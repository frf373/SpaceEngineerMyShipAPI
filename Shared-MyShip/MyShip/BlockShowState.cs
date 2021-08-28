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
        [Flags]
        public enum BlockShowState
        {
            /// <summary>
            /// 完全私人，不显示
            /// </summary>
            Private=0,

            /// <summary>
            /// 终端显示
            /// </summary>
            Terminal=1,

            /// <summary>
            /// 在库存中显示
            /// </summary>
            Inventory=2,

            /// <summary>
            /// 在工具栏中显示
            /// </summary>
            Toolbar=4,

            /// <summary>
            /// 完全公用，全部显示
            /// </summary>
            Public =7
        }
    }
}
