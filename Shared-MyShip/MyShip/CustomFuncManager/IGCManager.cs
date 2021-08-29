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
            /// 包含所有功能类中的所有广播监听的列表
            /// </summary>
            List<IMyBroadcastListener> FuncsListeners { get; set; }

            /// <summary>
            /// 广播监听到自定义功能类
            /// </summary>
            private Hashtable ListenerToFunc { get; set; }

            /// <summary>
            /// 注册有转发规则的广播监听
            /// </summary>
            /// <param name="func">广播监听所属功能类</param>
            /// <param name="listener">广播监听</param>
            public void RegisterBroadcastListener(CustomFuncBase func, IMyBroadcastListener listener)
            {
                FuncsListeners.Add(listener);
                ListenerToFunc.Add(listener, func);
            }

            /// <summary>
            /// 删除广播监听
            /// </summary>
            /// <param name="listener">要删除的广播监听</param>
            public void DisableBroadcastListener(IMyBroadcastListener listener)
            {
                FuncsListeners.Remove(listener);
                ListenerToFunc.Remove(listener);
            }

            /// <summary>
            /// 处理IGC转发，找到转发对象
            /// </summary>
            /// <param name="arg">原参数</param>
            public void HandleIGC(string arg)
            {
                string uid = "";
                foreach (var listener in FuncsListeners)
                {
                    if (listener.HasPendingMessage)
                    {
                        uid = ListenerToFunc[listener] as string;
                        break;
                    }
                }
                if (uid.Length != 0)
                {
                    RunArgFunc(uid, arg, UpdateType.IGC);
                }

            }


        }
    }
}
