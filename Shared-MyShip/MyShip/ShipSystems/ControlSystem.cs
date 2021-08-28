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
        /// 控制系统
        /// </summary>
        public class ControlSystem : ShipSystem
        {
            /// <summary>
            /// 驾驶舱集合
            /// </summary>
            public List<IMyCockpit> Cockpits { get; set; }

            /*
             /// <summary>
            /// 普通驾驶舱
            /// </summary>
            public List<IMyCockpit> NormalCockpits {  get; set; }

            /// <summary>
            /// 工业驾驶舱
            /// </summary>
            public List<IMyCockpit> IndustrialCockpits {  get; set; }

            /// <summary>
            /// 歼击机驾驶舱
            /// </summary>
            public List<IMyCockpit> FighterCockpits {  get; set; }

            /// <summary>
            /// 漫游车驾驶舱
            /// </summary>
            public List<IMyCockpit> RoverCockpits {  get; set; }

            //控制站，飞行座椅，另一个小车
            */

            /// <summary>
            /// 远程控制
            /// </summary>
            public List<IMyRemoteControl> RemoteControls { get; set; }

            /// <summary>
            /// 编程块
            /// </summary>
            public List<IMyProgrammableBlock> ProgrammableBlocks { get; set; }

            /// <summary>
            /// 定时方块
            /// </summary>
            public List<IMyTimerBlock> TimerBlocks { get; set; }

            /// <summary>
            /// 按钮面板
            /// </summary>
            public List<IMyButtonPanel> ButtonPanels { get; set; }

            /// <summary>
            /// 主驾驶舱
            /// </summary>
            public IMyCockpit MainCockpit { get; set; }

            /// <summary>
            /// 主远程控制
            /// </summary>
            public IMyRemoteControl MainRemoteControl { get; set; }

            /// <summary>
            /// 是否有主驾驶舱
            /// </summary>
            public bool HasMainCockpit => MainCockpit != null;

            /// <summary>
            /// 是否有主远程控制
            /// </summary>
            public bool HasMainRemoteControl => MainRemoteControl != null;
            public ControlSystem(MyShip ship) : base(ship)
            {

            }

            public override void InitializationBlock()
            {
                Cockpits = new List<IMyCockpit>();
                RemoteControls = new List<IMyRemoteControl>();

                ProgrammableBlocks = new List<IMyProgrammableBlock>();
                TimerBlocks = new List<IMyTimerBlock>();

                ButtonPanels = new List<IMyButtonPanel>();

                MainCockpit = null;
                MainRemoteControl = null;

                GridTerminalSystem.GetBlocksOfType(Cockpits, x => x.CanControlShip);
                GridTerminalSystem.GetBlocksOfType(RemoteControls);

                GridTerminalSystem.GetBlocksOfType(ProgrammableBlocks);
                GridTerminalSystem.GetBlocksOfType(TimerBlocks);

                GridTerminalSystem.GetBlocksOfType(ButtonPanels);

                foreach (var block in Cockpits)
                {
                    if (block.IsMainCockpit)
                    {
                        MainCockpit = block;
                        break;
                    }
                }
                foreach(var block in RemoteControls)
                {
                    if(block.IsMainCockpit)
                    {
                        MainRemoteControl = block;
                        break;
                    }
                }
            }
        }
    }
}
