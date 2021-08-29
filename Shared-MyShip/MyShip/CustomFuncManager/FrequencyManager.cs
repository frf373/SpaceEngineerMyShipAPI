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
            /// 函数更新频率更改缓存List
            /// </summary>
            List<FrequencyChangedInfoCache> FrequencyChangedInfoCaches { get; set; }

            /// <summary>
            /// 更新频率到5种功能表的哈希表
            /// </summary>
            Hashtable UpdateFrencyToActionList { get; set; }

            /// <summary>
            /// 从缓存信息中更新功能运行频率，防止直接更新后破坏List的枚举器
            /// </summary>
            public void UpdateFuncFrequency()
            {
                if (FrequencyChangedInfoCaches.Count != 0)
                {
                    foreach (var cache in FrequencyChangedInfoCaches)
                    {
                        //如果是不允许循环，则直接跳过，因为RunActionList中都没有
                        if (Convert.ToBoolean(
                            (FuncRunPermission)FuncToFuncRunPermission[(cache.sender as CustomFuncBase)]
                            & FuncRunPermission.Uncycled))
                        {
                            //如果是Once->Once，加入删除例外列表中
                            if (cache.e.previous == UpdateFrequency.Once && cache.e.now == UpdateFrequency.Once)
                            {
                                RunOnceDeletedExceptions.Add((cache.sender as CustomFuncBase).Main);
                            }
                            //如果以前和现在一样，则跳过，节省时间
                            if (cache.e.previous != cache.e.now)
                            {////////////////////////////////////////////////Uncycle，不要剪
                                CustomFuncBase customFunc = cache.sender as CustomFuncBase;
                                Action<string, UpdateType> MainAction = (UIDToFunc[customFunc.UID] as CustomFuncBase).Main;
                                (UpdateFrencyToActionList[cache.e.previous] as List<Action<string, UpdateType>>).Remove(MainAction);
                                (UpdateFrencyToActionList[cache.e.now] as List<Action<string, UpdateType>>).Add(MainAction);
                            }
                        }
                    }

                    //清空缓存列表
                    FrequencyChangedInfoCaches.Clear();
                }

                //如果运行一次没调整完全，手动调整，比如：Once->Update10会减少，但是如果Once没变化就要删除，要考虑Once->Once会放在例外列表中，不变化频率
                if (RunOnceActionList.Count != 0)
                {
                    foreach (var action in RunOnceActionList)
                    {
                        //如果例外列表中为0，直接更新频率，加快速度
                        if (RunOnceDeletedExceptions.Count == 0 || !RunOnceDeletedExceptions.Contains(action))
                        {
                            //手动调为None，此时会有更新频率变化的事件产生
                            (FuncMainToFunc[action] as CustomFuncBase).Runtime.UpdateFrequency = UpdateFrequency.None;
                        }
                    }
                    //清空例外
                    RunOnceDeletedExceptions.Clear();
                    //重新加载
                    UpdateFuncFrequency();
                }
            }

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

        }
    }
}
