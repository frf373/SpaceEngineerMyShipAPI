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
        /// 探测系统
        /// </summary>
        public class DetectionSystem : ShipSystem
        {
            /// <summary>
            /// 摄像头
            /// </summary>
            public List<IMyCameraBlock> Cameras { get; set; }

            /// <summary>
            /// 矿物探测器
            /// </summary>
            public List<IMyOreDetector> OreDetectors {  get; set; }

            /// <summary>
            /// 探测器
            /// </summary>
            public List<IMySensorBlock> Sensors {  get; set; }

            public DetectionSystem(MyShip ship):base(ship)
            {

            }

            public override void InitializationBlock()
            {
                Cameras=new List<IMyCameraBlock>();
                OreDetectors=new List<IMyOreDetector>();
                Sensors=new List<IMySensorBlock>();

                GridTerminalSystem.GetBlocksOfType(Cameras);
                GridTerminalSystem.GetBlocksOfType(OreDetectors);
                GridTerminalSystem.GetBlocksOfType(Sensors);
            }
        }
    }
}
