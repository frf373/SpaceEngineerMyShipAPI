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
        public partial class CustomFuncManager
        {
            /// <summary>
            /// 不运行的功能
            /// </summary>
            List<Action<string, UpdateType>> RunNoneActionList { get; set; }

            /// <summary>
            /// 只运行一次功能表
            /// </summary>
            List<Action<string, UpdateType>> RunOnceActionList { get; set; }

            /// <summary>
            /// 1tick运行一次功能表
            /// </summary>
            List<Action<string, UpdateType>> Run1ActionList { get; set; }

            /// <summary>
            /// 10tick运行一次功能表
            /// </summary>
            List<Action<string, UpdateType>> Run10ActionList { get; set; }

            /// <summary>
            /// 100tick运行一次功能表
            /// </summary>
            List<Action<string, UpdateType>> Run100ActionList { get; set; }

            /// <summary>
            /// 保存函数表
            /// </summary>
            List<Action> RunSaveActionList { get; set; }

            /// <summary>
            /// 应对Once->Once的频率变化，RunOnceActionList中不允许清空的Action
            /// </summary>
            List<Action<string, UpdateType>> RunOnceDeletedExceptions { get; set; }

            /// <summary>
            /// 标识符ID到自定函数类的哈希表，防止一个函数注册多次
            /// </summary>
            Hashtable UIDToFunc { get; set; }

            /// <summary>
            /// 功能主函数到功能的哈希表
            /// </summary>
            Hashtable FuncMainToFunc {  get; set; }

            /// <summary>
            /// 功能类到功能运行权限
            /// </summary>
            Hashtable FuncToFuncRunPermission {  get; set; }

            public CustomFuncManager()
            {

                UIDToFunc = new Hashtable();
                FuncMainToFunc = new Hashtable();
                FuncToFuncRunPermission = new Hashtable();

                UpdateFrencyToActionList = new Hashtable();

                RunNoneActionList = new List<Action<string, UpdateType>>();
                RunOnceActionList = new List<Action<string, UpdateType>>();
                Run1ActionList = new List<Action<string, UpdateType>>();
                Run10ActionList = new List<Action<string, UpdateType>>();
                Run100ActionList = new List<Action<string, UpdateType>>();

                RunSaveActionList = new List<Action>();

                UpdateFrencyToActionList.Add(UpdateFrequency.None, RunNoneActionList);
                UpdateFrencyToActionList.Add(UpdateFrequency.Once, RunOnceActionList);
                UpdateFrencyToActionList.Add(UpdateFrequency.Update1, Run1ActionList);
                UpdateFrencyToActionList.Add(UpdateFrequency.Update10, Run10ActionList);
                UpdateFrencyToActionList.Add(UpdateFrequency.Update100, Run100ActionList);

                FrequencyChangedInfoCaches = new List<FrequencyChangedInfoCache>();
                RunOnceDeletedExceptions = new List<Action<string, UpdateType>> ();

                FuncsListeners = new List<IMyBroadcastListener>();
                ListenerToFunc = new Hashtable();
            }

            /// <summary>
            /// 把自定义功能加载到飞船功能中
            /// </summary>
            public void ManageFunc(CustomFuncBase func, FuncRunPermission permission)
            {
                //如果没有这个权限，自动生成一个未注册的权限
                if(!FuncToFuncRunPermission.ContainsKey(func))
                {
                    FuncToFuncRunPermission.Add(func,FuncRunPermission.None);
                }

                //取出权限值
                FuncRunPermission funcPermission = (FuncRunPermission)FuncToFuncRunPermission[func];
                
                //如果功能没有注册，进行功能注册
                if (funcPermission == FuncRunPermission.None && permission != FuncRunPermission.None)
                {
                    //Id到这个类
                    UIDToFunc.Add(func.UID, func);

                    Action<string, UpdateType> tempMain = func.Main;
                    //main函数到类
                    FuncMainToFunc.Add(tempMain, func);


                    //注册频率变化时间处理函数
                    func.Runtime.OnUpdateFrequencyChanged += FuncUpdateFrequencyChangedHandler;
                    //注册保存函数
                    RunSaveActionList.Add(func.Save);

                    //相当于仅注册
                    funcPermission = FuncRunPermission.ToggleAllOff;
                }
                //下面不能是else if 因为注册完后还要跳出，并执行真正的参数

                //如果注册过，取消注册
                if (permission == FuncRunPermission.None && funcPermission != FuncRunPermission.None)
                {
                    UIDToFunc.Remove(func.UID);

                    Action<string, UpdateType> tempMain = func.Main;
                    FuncMainToFunc.Remove(tempMain);

                    //删除循环函数，防止直接从循环状态跳到未注册的时候，函数没删。
                    //List中移除不存在的东西，没有问题
                    if (func.Runtime.UpdateFrequency != UpdateFrequency.None)
                    {
                        (UpdateFrencyToActionList[func.Runtime.UpdateFrequency] as List<Action<string, UpdateType>>).Remove(func.Main);
                    }

                    //删除频率变化时间处理函数
                    func.Runtime.OnUpdateFrequencyChanged -= FuncUpdateFrequencyChangedHandler;
                    //删除保存函数
                    RunSaveActionList.Remove(func.Save);

                    //相当于未注册
                    funcPermission = FuncRunPermission.None;
                }
                else
                {
                    //用else if 是为了 过滤同时设置Listening和UnListened，优先取Listening
                    if (Convert.ToBoolean(permission & FuncRunPermission.Listening))
                    {
                        //不保留这一位
                        funcPermission &= ~FuncRunPermission.Unlistened;
                        //增加这样一位
                        funcPermission |= FuncRunPermission.Listening;
                    }
                    else if (Convert.ToBoolean(permission & FuncRunPermission.Unlistened))
                    {
                        funcPermission &= ~FuncRunPermission.Listening;
                        funcPermission |= FuncRunPermission.Unlistened;
                    }
                    //else if 与上面同理
                    //注意，这个功能只是把原本会循环的功能开启循环，原本设置不是循环的函数，即None不会继续循环
                    //相当于截断循环和恢复截断
                    //当然经过arg运行后，比如更新频率变为Once,或者其他。打开循环后，会变成RunOnce等
                    if (Convert.ToBoolean(permission & FuncRunPermission.Cycling)&&Convert.ToBoolean(funcPermission & FuncRunPermission.Cycling))
                    {
                        //因为变频事件中改List会被Uncycled阻断，应该可以加
                        (UpdateFrencyToActionList[func.Runtime.UpdateFrequency] as List<Action<string, UpdateType>>).Add(func.Main);
                        funcPermission &= ~FuncRunPermission.Uncycled;
                        funcPermission |= FuncRunPermission.Cycling;

                    }
                    else if (Convert.ToBoolean(permission & FuncRunPermission.Uncycled)&&Convert.ToBoolean(funcPermission & FuncRunPermission.Uncycled))
                    {
                        //因为变频事件中改List会被Uncycled阻断，应该可以减
                        (UpdateFrencyToActionList[func.Runtime.UpdateFrequency] as List<Action<string, UpdateType>>).Remove(func.Main);
                        funcPermission &= ~FuncRunPermission.Cycling;
                        funcPermission |= FuncRunPermission.Uncycled;

                    }
                }
                //存回权限值
                FuncToFuncRunPermission[func] = funcPermission;
            }

            /// <summary>
            /// 运行1次的功能
            /// </summary>
            public void RunOnceFunc()
            {
                RunOnceActionList.ForEach(x => x("", UpdateType.Once));
            }

            /// <summary>
            /// 循环1tick的功能
            /// </summary>
            public void RunCycle1Func()
            {
                Run1ActionList.ForEach(x =>x("", UpdateType.Update1));
            }

            /// <summary>
            /// 循环10tick的功能
            /// </summary>
            public void RunCycle10Func()
            {
                Run10ActionList.ForEach(x => x("", UpdateType.Update10));
            }

            /// <summary>
            /// 循环100tick的功能
            /// </summary>
            public void RunCycle100Func()
            {
                Run100ActionList.ForEach(x => x("", UpdateType.Update100));
            }

            /// <summary>
            /// 运行保存的功能
            /// </summary>
            public void RunSaveFunc()
            {
                RunSaveActionList.ForEach(x => x());
            }

            /// <summary>
            /// 转发函数参数的处理函数
            /// </summary>
            /// <param name="arg">原始参数</param>
            /// <param name="updateSource">更新类型</param>
            public void HandleArg(string arg, UpdateType updateSource)
            {
                string[] handledArgs = arg.Split(new string[] { CustomFuncBase.custom_Separator_End }, StringSplitOptions.None);
                //Trim保证删掉字符串最前面和最后面的所有空格
                RunArgFunc(handledArgs[0].Trim(' '), handledArgs[1].TrimStart(' '), updateSource);
            }

            /// <summary>
            /// 对一个功能执行有参数的运行
            /// </summary>
            /// <param name="UID">功能16位字符串标识符(独一无二的)</param>
            /// <param name="arg">运行参数</param>
            /// <param name="updateSource">更新信息</param>
            public void RunArgFunc(string UID, string arg, UpdateType updateSource)
            {
                if (UID.Length != 16)
                {
                    throw new Exception("你的触发器发送的函数UID不是16位\n你发送的UID是:" + UID);
                }
                else
                {
                    if (UIDToFunc[UID] != null)
                    {
                        CustomFuncBase func = UIDToFunc[UID] as CustomFuncBase;
                        if (Convert.ToBoolean(
                            (FuncRunPermission)FuncToFuncRunPermission[func] 
                            & FuncRunPermission.Listening))
                        {
                            func?.Main(arg, updateSource);
                        }
                        else
                        {
                            throw new Exception("你的功能未开启参数监听\n你发送的UID是:" + UID);
                        }

                    }
                    else
                    {
                        throw new Exception("你的触发器发送的UID不正确\n你发送的UID是:" + UID);
                    }
                }
            }
        }
    }
}
