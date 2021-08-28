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
        /// 飞船
        /// </summary>
        public class MyShip
        {
            /// <summary>
            /// 网格终端系统
            /// </summary>
            public IMyGridTerminalSystem GridTerminalSystem=>Program.GridTerminalSystem;

            /// <summary>
            /// Echo
            /// </summary>
            public Action<string> Echo => Program.Echo;

            /// <summary>
            /// 本程序块
            /// </summary>
            public IMyProgrammableBlock Me => Program.Me;

            /// <summary>
            /// 程序接口
            /// </summary>
            public Program Program {  get; set; }

            /// <summary>
            /// 是否是静态网格
            /// </summary>
            public bool IsStatic => Program.Me.CubeGrid.IsStatic;

            /// <summary>
            /// 是否是大网格，判断依据是这个编程块是否是大的
            /// </summary>
            public bool IsLargeGrid => Program.Me.BlockDefinition.SubtypeId.Contains("Large");

            public string ShipName
            {
                get
                {
                    return Program.Me.CubeGrid.CustomName;
                }
                set
                {
                    Program.Me.CubeGrid.CustomName = value;
                }
            }
            /// <summary>
            /// 飞船功能系统集合
            /// </summary>
            public ShipSystemCollection ShipSystems { get; set; }

            public MyShip(Program program)
            {
                this.Program = program;
                ShipSystems = new ShipSystemCollection(this);
            }
        }
    }
}
