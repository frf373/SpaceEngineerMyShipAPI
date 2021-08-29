﻿using Sandbox.Game.EntityComponents;
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
        abstract public partial class CustomFuncBase
        {
            /// <summary>
            /// 定义好转发规则的IGC系统
            /// </summary>
            public class Custom_IGC
            {
                private readonly CustomFuncBase func;

                private IMyIntergridCommunicationSystem OriginalIGC => func.program.IGC;

                public Custom_IGC(CustomFuncBase func)
                {
                    this.func = func;
                }

                public long Me => OriginalIGC.Me;
                public IMyUnicastListener UnicastListener => OriginalIGC.UnicastListener;


                public void GetBroadcastListeners(List<IMyBroadcastListener> broadcastListeners, Func<IMyBroadcastListener, bool> collect = null)=>OriginalIGC.GetBroadcastListeners(broadcastListeners, collect);

                public bool SendUnicastMessage<TData>(long addressee, string tag, TData data) => OriginalIGC.SendUnicastMessage<TData>(addressee, tag, data);

                //改写部分
                public IMyBroadcastListener RegisterBroadcastListener(string tag)
                {
                    IMyBroadcastListener listener= OriginalIGC.RegisterBroadcastListener(tag);
                    func.CustomFuncs.RegisterBroadcastListener(func, listener);
                    return listener;
                }
                
                //改写部分
                public void SendBroadcastMessage<TData>(string tag, TData data, TransmissionDistance transmissionDistance = TransmissionDistance.AntennaRelay)=>OriginalIGC.SendBroadcastMessage<TData>(tag, data, transmissionDistance);
                
                //改写部分
                public void DisableBroadcastListener(IMyBroadcastListener broadcastListener)
                {
                    OriginalIGC.DisableBroadcastListener(broadcastListener);
                }


            }
        }
    }
}
