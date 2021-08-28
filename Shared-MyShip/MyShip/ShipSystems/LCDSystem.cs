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
        /// 屏幕系统
        /// </summary>
        public class LCDSystem:ShipSystem
        {

            /// <summary>
            /// 所有屏幕面板
            /// </summary>
            public List<IMyTextPanel> TextPanels {  get; set; }

            /// <summary>
            /// 方形屏幕面板
            /// </summary>
            public List<IMyTextPanel> SquarePanels {  get; set; }

            /// <summary>
            /// 矩形屏幕面板
            /// </summary>

            public List<IMyTextPanel> RectanglePanels { get; set; }

            /// <summary>
            /// 角落屏幕面板
            /// </summary>
            public List<IMyTextPanel> CornerPanels { get; set; }

            public LCDSystem(MyShip ship):base(ship)
            {

            }

            public override void InitializationBlock()
            {
                
            }
        }
    }
}
