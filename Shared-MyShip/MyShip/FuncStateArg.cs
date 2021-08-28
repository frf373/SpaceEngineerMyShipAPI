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
        /// 功能运行状态参数
        /// </summary>
        [Flags]
        public enum FuncStateArg
        {
            /// <summary>
            /// 代表未注册的功能
            /// </summary>
            None = 0,

            /// <summary>
            /// 开启此功能的参数监听，即向此功能传参数是有效的
            /// </summary>
            Listening = 1,

            /// <summary>
            /// 关闭此功能的参数监听，即向此功能传参数是无效的
            /// </summary>
            Unlistened = 2,

            /// <summary>
            /// 开始循环此功能
            /// </summary>
            Cycling = 4,

            /// <summary>
            /// 停止循环此功能
            /// </summary>
            Uncycled = 8,

            /// <summary>
            /// 一般来说选这个，功能全开
            /// </summary>
            ToggleAllOn = Listening | Cycling,

            /// <summary>
            /// 全部关闭,仅相当于注册过
            /// </summary>
            ToggleAllOff = Unlistened | Uncycled
        }
    }
}
