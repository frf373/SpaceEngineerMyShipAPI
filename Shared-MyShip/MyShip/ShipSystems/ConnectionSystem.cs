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
        /// 连接系统
        /// </summary>
        public class ConnectionSystem:ShipSystem
        {
            /// <summary>
            /// 连接器
            /// </summary>
            public List<IMyShipConnector> Connectors {  get; set; }

            /// <summary>
            /// 合并块
            /// </summary>
            public List<IMyShipMergeBlock> MergeBlocks { get; set; }
            public ConnectionSystem(MyShip ship):base(ship)
            {

            }

            public override void InitializationBlock()
            {
                Connectors = new List<IMyShipConnector>();
                MergeBlocks=new List<IMyShipMergeBlock>();

                GridTerminalSystem.GetBlocksOfType(Connectors);
                GridTerminalSystem.GetBlocksOfType(MergeBlocks);
            }
        }
    }
}
