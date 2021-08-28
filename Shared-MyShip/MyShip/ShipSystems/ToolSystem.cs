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
        /// 工具系统
        /// </summary>
        public class ToolSystem : ShipSystem
        {
            /// <summary>
            /// 钻头
            /// </summary>
            public List<IMyShipDrill> Drills {  get; set; }

            /// <summary>
            /// 拆卸机
            /// </summary>
            public List<IMyShipGrinder> Grinders {  get; set; }

            /// <summary>
            /// 焊接机
            /// </summary>
            public List<IMyShipWelder> Welders {  get; set; }


            public ToolSystem(MyShip ship):base(ship)
            {
                
            }

            public override void InitializationBlock()
            {
                Drills = new List<IMyShipDrill>();
                Grinders = new List<IMyShipGrinder>();
                Welders = new List<IMyShipWelder>();

                GridTerminalSystem.GetBlocksOfType(Drills);
                GridTerminalSystem.GetBlocksOfType(Grinders);
                GridTerminalSystem.GetBlocksOfType(Welders);
            }
        }
    }
}
