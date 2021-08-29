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
        abstract public partial class CustomFuncBase
        {
            /// <summary>
            /// 定义好转发规则的IGC系统
            /// </summary>
            public class Custom_IGC:IMyIntergridCommunicationSystem
            {
                private readonly CustomFuncBase func;

                private IMyIntergridCommunicationSystem IGC => func.program.IGC;

                public Custom_IGC(CustomFuncBase func)
                {
                    this.func = func;
                }

                public long Me => IGC.Me;
                public IMyUnicastListener UnicastListener => IGC.UnicastListener;
                public bool IsEndpointReachable(long address, TransmissionDistance transmissionDistance = TransmissionDistance.AntennaRelay) => IGC.IsEndpointReachable(address, transmissionDistance);
                public void GetBroadcastListeners(List<IMyBroadcastListener> broadcastListeners, Func<IMyBroadcastListener, bool> collect = null)=>IGC.GetBroadcastListeners(broadcastListeners, collect);

                public bool SendUnicastMessage<TData>(long addressee, string tag, TData data) => IGC.SendUnicastMessage<TData>(addressee, tag, data);

                //改写部分
                public IMyBroadcastListener RegisterBroadcastListener(string tag)
                {
                    IMyBroadcastListener listener= IGC.RegisterBroadcastListener(tag);
                    func.CustomFuncs.RegisterBroadcastListener(func, listener);
                    return listener;
                }
                
                public void SendBroadcastMessage<TData>(string tag, TData data, TransmissionDistance transmissionDistance = TransmissionDistance.AntennaRelay)=>IGC.SendBroadcastMessage<TData>(tag, data, transmissionDistance);
                
                //改写部分
                public void DisableBroadcastListener(IMyBroadcastListener listener)
                {
                    func.CustomFuncs.DisableBroadcastListener(listener);
                    IGC.DisableBroadcastListener(listener);
                }

            }
        }
    }
}
