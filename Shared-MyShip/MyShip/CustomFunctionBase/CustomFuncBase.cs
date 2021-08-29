using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;
namespace IngameScript
{
    partial class Program
    {
        /// <summary>
        /// 自定义函数基类，注意：不可以继承MyGridProgram,需要的东西需要重写！
        /// </summary>
        public partial class CustomFuncBase
        {
            /// <summary>
            /// Ini配置读取类
            /// </summary>
            MyIni Ini {  get; set; }

            /// <summary>
            /// 数据分隔符,处于每条数据的结尾处
            /// </summary>
            public static readonly string custom_Separator_End = "#+-*/#";

            private readonly Program program;

            private readonly MyShip ship;

            /// <summary>
            /// 飞船系统接口
            /// </summary>
            protected ShipSystemCollection ShipSystems => ship.ShipSystems;

            protected CustomFuncManager CustomFuncs => ship.CustomFuncs;
            /// <summary>
            /// 直接使用，无需定义转发规则
            /// </summary>
            protected IMyGridTerminalSystem GridTerminalSystem => program.GridTerminalSystem;

            /// <summary>
            /// 程序块接口，可能考虑需要转发
            /// </summary>
            protected IMyProgrammableBlock Me=>program.Me;

            /// <summary>
            /// 总是要运行的函数,比如计数函数，日志函数
            /// </summary>
            protected Action<string, UpdateType> RunAlwaysAction;

            /// <summary>
            /// 有参数的Action
            /// </summary>
            protected Action<string, UpdateType> RunArgAction;

            /// <summary>
            /// 无参数的循环Action
            /// </summary>
            protected Action RunCycleAction;

            /// <summary>
            /// 删除自定义数据，相当于block.CustomData-""
            /// </summary>
            /// <param name="block">要删除自定义数据的方块</param>
            protected void Remove_CustomData(IMyTerminalBlock block)
            {
                Set_CustomData(block, "");
            }

            /// <summary>
            /// 增加自定义数据，相当于block.CustomData+=data
            /// </summary>
            /// <param name="block">要加的自定义数据的方块</param>
            /// <param name="data">自定义数据内容</param>
            protected void Add_CustomData(IMyTerminalBlock block, string data)
            {
                string temp = Get_CustomData(block);

                Set_CustomData(block,temp+data);
            }

            /// <summary>
            /// 设置自定义数据,相当于block.CustomData=data
            /// </summary>
            /// <param name="block">要设置自定义数据的方块</param>
            /// <param name="data">自定义数据内容</param>
            protected void Set_CustomData(IMyTerminalBlock block, string data)
            {
                if (data.Contains(custom_Separator_End))
                {
                    throw new Exception("你的传递字符中有专用分隔符");
                }
                else
                {
                    block.CustomData = SetbySplit(block.CustomData, data);
                }
            }

            /// <summary>
            /// 获得自定义数据,相当于=block.CustomData
            /// </summary>
            /// <param name="block">要获得自定义数据的方块</param>
            /// <returns>自定义数据</returns>
            protected string Get_CustomData(IMyTerminalBlock block)
            {
                return GetbySplit(block.CustomData);
            }

            /// <summary>
            /// 用分隔符获得数据，会去掉最前面的换行符
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            private string GetbySplit(string data)
            {
                //消除开头和结尾的换行符，空格等无效字符
                string formedData = data.Trim();
                string[] entries = formedData.Split(new string[] { custom_Separator_End }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var a in entries)
                {
                    //去掉开头换行符等无效字符
                    string temp = a.TrimStart();
                    if (temp.StartsWith(UID))
                    {
                        int startIndex = UID.Length;
                        //防止误跳过有用字符
                        if (temp[UID.Length]=='\n') startIndex++;
                        string temp2=temp.Substring(startIndex);

                        //防止误删有用字符
                        return temp2.EndsWith("\n") ? temp2.Remove(temp2.Length - 1) : temp2;
                    }
                }
                return "";
            }

            /// <summary>
            /// 用分隔符设置数据，如果数据没有和函数标识符隔开，会自动给数据最前面增加换行符。如果数据没有和分隔符隔开，会自动给数据最后面增加换行符
            /// </summary>
            /// <param name="target">目标数据</param>
            /// <param name="value">要设置的值</param>
            /// <returns></returns>
            private string SetbySplit(string target,string value)
            {
                //消除开头和结尾的换行符，空格等无效字符
                string formedTarget = target.Trim();
                string[] entries = formedTarget.Split(new string[] { custom_Separator_End }, StringSplitOptions.RemoveEmptyEntries);
                StringBuilder builder = new StringBuilder();
                //是否找到函数标识符下的条目
                bool found = false;
                foreach (var a in entries)
                {
                    //消除开头的换行符，空格等无效字符
                    string temp =a.TrimStart();
                    if (!found&&temp.StartsWith(uid))
                    {
                        //空为删除条目，即为不增加
                        if (value != "")
                        {
                            //数据头
                            builder.AppendLine(UID);
                            //不论源数据有没有换行符，都加换行符，相当于直接隔开
                            builder.AppendLine(value);
                            //数据尾
                            builder.AppendLine(custom_Separator_End);

                            found = true;
                        }
                        //""代表删除，设置为找到等于直接不用再找了，也不会新增条目
                        else
                        {
                            found = true;
                        }
                    }
                    else
                    {
                        builder.Append(temp);
                        //自动补上换行符
                        if(!temp.EndsWith("\n")) builder.AppendLine();
                        builder.AppendLine(custom_Separator_End);
                    }
                }
                //没有条目则新增条目
                if (!found)
                {
                    builder.Append(UID);
                    //如果数据前面没有换行符，则手动增加换行符
                    if (!value.StartsWith("\n")) builder.AppendLine();
                    builder.Append(value);
                    //如果后面没有换行符，则手动增加换行符
                    if (!value.EndsWith("\n")) builder.AppendLine();
                    builder.AppendLine(custom_Separator_End);
                }
               return builder.ToString();
            }

            /// <summary>
            /// 定义好转发规则的Storage
            /// </summary>
            protected string Storage
            {
                get
                {
                    return GetbySplit(program.Storage);
                }
                set
                {
                    program.Storage= SetbySplit(program.Storage, value);
                }
            }

            /// <summary>
            /// 自定义Echo流
            /// </summary>
            public Custom_Echo CustomEcho { get; set; }

            /// <summary>
            /// 定义好的转发规则和缓存功能的Echo
            /// </summary>
            protected Action<string> Echo => CustomEcho.Echo_Builder;

            /// <summary>
            /// 自定义Runtime类
            /// </summary>
            public Custom_Runtime Runtime { get; protected set; }

            /// <summary>
            /// 自定义功能名称
            /// </summary>
            public string FuncName { get; set; }

            /// <summary>
            /// 必须设置的16位函数ID，请为自己函数定义好
            /// </summary>
            private string uid;

            /// <summary>
            /// 必须设置的16位函数ID，用于识别函数和转发规则等功能。请给自己函数定义一个唯一的ID。
            /// 不论多少次重新编写的你的函数代码，这个ID最好和第一次定义一样。
            /// 因为在你使用任何保存的数据时，只能读取到和这个ID标识符相匹配的内容。
            /// 比如在使用CustomData,Storage的时候
            /// 最好是16位字母，字符"["和字符"]"是不允许被使用的
            /// </summary>
            public string UID
            {
                get
                {
                    if(uid == null)
                    {
                        ReportException("未设置16位转发ID，不能使用转发功能");
                    }
                    return uid;
                }
                set
                {
                    if (value.Length != 16)
                    {
                        ReportException("转发规则ID应该是长度为16位的字符串");
                    }
                    else
                    {
                        if(value.Contains("[")||value.Contains("]"))
                        {
                            ReportException("转发规则ID中[和]符号是不被允许的");
                        }
                        else
                        {
                            uid = value;
                        }
                    }
                }
            }

            /// <summary>
            /// 隶属于哪个飞船系统的功能
            /// </summary>
            public ShipSystem AttachedTo { get; set; }

            /// <summary>
            /// 自定函数基类构造函数
            /// </summary>
            /// <param name="program">程序块接口</param>
            /// <param name="ship">使用的飞船类的接口，参数必须填，但是接口里面的功能可以不用</param>
            /// <param name="funcName">自定义函数名称</param>
            /// <param name="uid">自定义函数标识16位字符串ID。（最好是16位英文字母）
            ///必须设置的16位函数ID，用于识别函数和转发规则等功能。请给自己函数定义一个唯一的ID。
            /// 不论多少次重新编写的你的函数代码，这个ID最好和第一次定义一样。
            /// 因为在你使用任何保存的数据时，只能读取到和这个ID标识符相匹配的内容。
            /// 比如在使用CustomData,Storage的时候
            /// 最好是16位字母，字符"["和字符"]"是不允许被使用的
            /// </param>
            public CustomFuncBase(Program program, MyShip ship, string funcName,string uid)
            {
                this.program = program;
                this.ship = ship;
                FuncName = funcName;
                this.UID = uid;

                Runtime = new Custom_Runtime(this);
                CustomEcho = new Custom_Echo(this);
                Ini = new MyIni();
            }

            /// <summary>
            /// 功能主函数，如果你用了以下Action,就可以不用重写
            /// </summary>
            /// <param name="arg">参数</param>
            /// <param name="source">更新类型</param>
            public virtual void Main(string arg, UpdateType source)
            {
                if (Convert.ToBoolean(source & (UpdateType.Once | UpdateType.Update1 | UpdateType.Update10 | UpdateType.Update100)))
                {
                    RunCycleAction?.Invoke();
                }
                else
                {
                    RunArgAction?.Invoke(arg, source);
                }
                RunAlwaysAction?.Invoke(arg, source);
            }

            /// <summary>
            /// 保存函数
            /// </summary>
            public virtual void Save()
            {

            }

            /// <summary>
            /// 相当于throw new Exception 但会自动帮你加上功能名称和UID
            /// </summary>
            /// <param name="exceptionContent">例外内容</param>
            protected void ReportException(string exceptionContent)
            {
                throw new Exception($"[FuncName:{FuncName}][UID:{UID}]Exception:"+exceptionContent);
            }

            /// <summary>
            /// 注意除，CustomBase功能运行权限
            /// </summary>
            //public FuncRunPermission FuncState {  get; set; }
        }
    }
}
