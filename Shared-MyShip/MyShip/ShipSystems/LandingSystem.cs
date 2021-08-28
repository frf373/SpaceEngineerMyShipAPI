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
        /// 降落系统
        /// </summary>
        public class LandingSystem : ShipSystem
        {
            /// <summary>
            /// 起落架包括大小磁性板
            /// </summary>
            public List<IMyLandingGear> LandingGears {  get; set; }

            /// <summary>
            /// 降落伞
            /// </summary>
            public List<IMyParachute> Parachutes {  get; set; }
            public LandingSystem(MyShip ship):base(ship)
            {

            }

            public override void InitializationBlock()
            {
                LandingGears= new List<IMyLandingGear>();
                Parachutes= new List<IMyParachute>();
                GridTerminalSystem.GetBlocksOfType(LandingGears);
                GridTerminalSystem.GetBlocksOfType(Parachutes);

            }
        }
    }
}
