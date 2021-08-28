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
            /*public class Custom_IGC : IMyIntergridCommunicationSystem
            {
                public long Me
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                }

                public IMyUnicastListener UnicastListener
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                }

                public void DisableBroadcastListener(IMyBroadcastListener broadcastListener)
                {
                    throw new NotImplementedException();
                }

                public void GetBroadcastListeners(List<IMyBroadcastListener> broadcastListeners, Func<IMyBroadcastListener, bool> collect = null)
                {
                    throw new NotImplementedException();
                }

                public bool IsEndpointReachable(long address, TransmissionDistance transmissionDistance = TransmissionDistance.AntennaRelay)
                {
                    throw new NotImplementedException();
                }

                public IMyBroadcastListener RegisterBroadcastListener(string tag)
                {
                    throw new NotImplementedException();
                }

                public void SendBroadcastMessage<TData>(string tag, TData data, TransmissionDistance transmissionDistance = TransmissionDistance.AntennaRelay)
                {
                    throw new NotImplementedException();
                }

                public bool SendUnicastMessage<TData>(long addressee, string tag, TData data)
                {
                    throw new NotImplementedException();
                }
            }*/
        }
    }
}
