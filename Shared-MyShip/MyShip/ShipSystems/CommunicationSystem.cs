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
        /// 通讯系统
        /// </summary>
        public class CommunicationSystem : ShipSystem
        {
            /// <summary>
            /// 信标
            /// </summary>
            public List<IMyBeacon> Beacons { get; set; }

            /// <summary>
            /// 天线,包括天线锅
            /// </summary>
            public List<IMyRadioAntenna> Antennas { get; set; }

            /// <summary>
            /// 激光天线
            /// </summary>
            public List<IMyLaserAntenna> LaserAntennas {  get; set; }
            public CommunicationSystem(MyShip ship):base(ship)
            {

            }

            public override void InitializationBlock()
            {
                Beacons = new List<IMyBeacon>();
                Antennas = new List<IMyRadioAntenna>();
                LaserAntennas= new List<IMyLaserAntenna>();

                GridTerminalSystem.GetBlocksOfType(Beacons);
                GridTerminalSystem.GetBlocksOfType(Antennas);
                GridTerminalSystem.GetBlocksOfType(LaserAntennas);
            }
        }
    }
}
