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
        public class CustomFuncManager
        {
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
            List<Action> RunSaveActionList {  get; set; }
            /// <summary>
            /// 标识符ID到自定函数类的哈希表，防止一个函数注册多次
            /// </summary>
            Hashtable UIDToFunc { get; set; }

            /// <summary>
            /// 更新频率到4钟功能表的哈希表
            /// </summary>
            Hashtable UpdateFrencyToActionList { get; set; }

            /// <summary>
            /// 飞船功能接口
            /// </summary>
            //private MyShip ship;
            public CustomFuncManager(MyShip ship)
            {
                //this.ship = ship;

                UIDToFunc = new Hashtable();

                UpdateFrencyToActionList = new Hashtable();
                RunOnceActionList = new List<Action<string, UpdateType>>();
                Run1ActionList = new List<Action<string, UpdateType>>();
                Run10ActionList = new List<Action<string, UpdateType>>();
                Run100ActionList = new List<Action<string, UpdateType>>();

                RunSaveActionList = new List<Action>();

                UpdateFrencyToActionList.Add(UpdateFrequency.Once, RunOnceActionList);
                UpdateFrencyToActionList.Add(UpdateFrequency.Update1, Run1ActionList);
                UpdateFrencyToActionList.Add(UpdateFrequency.Update10, Run10ActionList);
                UpdateFrencyToActionList.Add(UpdateFrequency.Update100, Run100ActionList);

                FrequencyChangedInfoCaches = new List<FrequencyChangedInfoCache>();
            }


            /// <summary>
            /// 把自定义功能加载到飞船功能中
            /// </summary>
            public void ManageFunc(CustomFuncBase customFunc,FuncStateArg managerArg)
            {


                //Id到这个类
                UIDToFunc.Add(customFunc.UID, customFunc);
                //None不考虑
                if (customFunc.Runtime.UpdateFrequency != UpdateFrequency.None)
                {
                    (UpdateFrencyToActionList[customFunc.Runtime.UpdateFrequency] as List<Action<string, UpdateType>>).Add(customFunc.Main);
                }

                customFunc.Runtime.OnUpdateFrequencyChanged += FuncUpdateFrequencyChangedHandler;

                RunSaveActionList.Add(customFunc.Save);
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
                Run1ActionList.ForEach(x => x("", UpdateType.Update1));
            }

            /// <summary>
            /// 循环10tick的功能
            /// </summary>
            public void RunCycle10Func()
            {
                Run10ActionList.ForEach(x => x("",UpdateType.Update10));
            }

            /// <summary>
            /// 循环100tick的功能
            /// </summary>
            public void RunCycle100Func()
            {
                Run100ActionList.ForEach(x=> x("",UpdateType.Update100));
            }

            /// <summary>
            /// 运行保存的功能
            /// </summary>
            public void RunSaveFunc()
            {
                RunSaveActionList.ForEach(x=>x());
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
                if(UID.Length!=16)
                {
                    throw new Exception("你的触发器发送的函数UID不是16位\n你发送的UID是:"+UID);
                }
                else
                {
                    if(UIDToFunc[UID]!=null)
                    {
                        (UIDToFunc[UID] as CustomFuncBase)?.Main(arg, updateSource);
                    }
                    else
                    {
                        throw new Exception("你的触发器发送的UID不正确\n你发送的UID是:"+UID);
                    }
                }
            }

            /// <summary>
            /// 从缓存信息中更新功能运行频率，防止直接更新后破坏List的枚举器
            /// </summary>
            public void UpdateFuncFrequency()
            {
                //自动清空Once列表
                RunOnceActionList.Clear();

                if (FrequencyChangedInfoCaches.Count != 0)
                {
                    foreach (var cache in FrequencyChangedInfoCaches)
                    {
                        CustomFuncBase customFunc = cache.sender as CustomFuncBase;
                        Action<string, UpdateType> MainAction = (UIDToFunc[customFunc.UID] as CustomFuncBase).Main;

                        //None不用减，Once已经自动清空
                        if (cache.e.previous != UpdateFrequency.Once && cache.e.previous != UpdateFrequency.None)
                        {
                            (UpdateFrencyToActionList[cache.e.previous] as List<Action<string, UpdateType>>).Remove(MainAction);
                        }
                        //None不能加
                        if (cache.e.now != UpdateFrequency.None)
                        {
                            (UpdateFrencyToActionList[cache.e.now] as List<Action<string, UpdateType>>).Add(MainAction);
                        }
                    }
                }

                //清空缓存列表
                FrequencyChangedInfoCaches.Clear();
            }

            /// <summary>
            /// 函数更新频率信息缓存类。这是嵌套类，外界最好不要自己构造
            /// </summary>
            public class FrequencyChangedInfoCache
            {
                /// <summary>
                /// 函数集合接口
                /// </summary>
                public CustomFuncManager customFuncs;

                /// <summary>
                /// 更新频率的功能函数
                /// </summary>
                public object sender;

                /// <summary>
                /// 更新后的频率事件参数
                /// </summary>
                public CustomFuncBase.UpdateFrequencyChangedEventArgs e;

                /// <summary>
                /// 函数更新频率信息缓存类构造函数
                /// </summary>
                /// <param name="customFuncs">函数集合接口，本类中调用填this就行</param>
                /// <param name="sender">更新频率的功能函数</param>
                /// <param name="e">更新后的频率事件参数</param>
                public FrequencyChangedInfoCache(CustomFuncManager customFuncs, object sender, CustomFuncBase.UpdateFrequencyChangedEventArgs e)
                {
                    this.customFuncs = customFuncs;
                    this.sender = sender;
                    this.e = e;
                }
            }

            /// <summary>
            /// 函数更新频率更改缓存List
            /// </summary>
            private List<FrequencyChangedInfoCache> FrequencyChangedInfoCaches { get; set; }

            /// <summary>
            /// 功能函数更新频率改变处理函数
            /// </summary>
            /// <param name="sender">更新频率的功能函数</param>
            /// <param name="e">更新后的频率事件参数</param>
            public void FuncUpdateFrequencyChangedHandler(object sender, CustomFuncBase.UpdateFrequencyChangedEventArgs e)
            {
                //记录为缓存，不能直接更改函数频率，防止破坏List的枚举器
                FrequencyChangedInfoCaches.Add(new FrequencyChangedInfoCache(this, sender, e));
            }

            public string GetEchoCache()
            {
                return "";
            }
            public void EchoAllCache()
            {
                
            }

            
        }
    }
}
