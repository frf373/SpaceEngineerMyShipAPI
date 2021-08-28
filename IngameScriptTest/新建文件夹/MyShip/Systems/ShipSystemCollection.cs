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
        /// 飞船系统集合
        /// </summary>
        public class ShipSystemCollection
        {

            private MyShip ship;

            public ShipSystemCollection(MyShip ship)
            {
                this.ship = ship;

                ElectricSystem=new ElectricSystem(ship);
            }
            /// <summary>
            /// 辅助系统
            /// </summary>
            public AssistanceSystem AssistanceSystem { get; set; }
            /// <summary>
            /// 电力系统
            /// </summary>
            public ElectricSystem ElectricSystem { get; set; }


        }
    }
}
