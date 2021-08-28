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
        /// 辅助系统
        /// </summary>
        public class AssistanceSystem : ShipSystem
        {
            /// <summary>
            /// 音效方块
            /// </summary>
            public List<IMySoundBlock> SoundBlocks { get; set; }

            /// <summary>
            /// 音乐盒
            /// </summary>
            public List<IMySoundBlock> Jukeboxes { get; set; }

            /// <summary>
            /// 投影仪
            /// </summary>
            public List<IMyProjector> Projectors { get; set; }

            /// <summary>
            /// 中控方块
            /// </summary>
            public List<IMyProjector> ConsoleBlocks { get; set; }

            /// <summary>
            /// 安全区
            /// </summary>
            public List<IMySafeZoneBlock> SafeZone { get; set; }

            /// <summary>
            /// 控制面板方块,包括科幻
            /// </summary>
            public List<IMyControlPanel> ControlPanels { get; set; }

            /// <summary>
            /// 排气管
            /// </summary>
            public List<IMyTerminalBlock> ExhaustPipes { get; set; }

            /// <summary>
            /// 假人
            /// </summary>
            public List<IMyTargetDummyBlock> TargetDummies { get; set; }

            /// <summary>
            /// 座位，包括乘客座椅，普通座椅，厕所
            /// </summary>
            public List<IMyCockpit> Seats {  get; set; }

            /// <summary>
            /// 医疗站
            /// </summary>
            public List<IMyTerminalBlock> MedicalStations {  get; set; }//大类是LCDPanelsBlock

            /// <summary>
            /// 实验室设备
            /// </summary>
            public List<IMyTerminalBlock> LabEquipments {  get; set; }//大类是LCDPanelsBlock


            public AssistanceSystem(MyShip ship) : base(ship)
            {

            }


            public override void InitializationBlock()
            {
                SoundBlocks = new List<IMySoundBlock>();
                Jukeboxes=new List<IMySoundBlock>();

                Projectors=new List<IMyProjector>();
                ConsoleBlocks = new List<IMyProjector>();

                SafeZone=new List<IMySafeZoneBlock>();
                ControlPanels=new List<IMyControlPanel>();
                ExhaustPipes = new List<IMyTerminalBlock>();
                TargetDummies=new List<IMyTargetDummyBlock>();

                Seats = new List<IMyCockpit>();

                MedicalStations=new List<IMyTerminalBlock>();
                LabEquipments=new List<IMyTerminalBlock>();

                GridTerminalSystem.GetBlocksOfType(SoundBlocks, x => x.BlockDefinition.SubtypeId.Contains("SoundBlock"));
                GridTerminalSystem.GetBlocksOfType(Jukeboxes, x => x.BlockDefinition.SubtypeId.Contains("Jukebox"));

                GridTerminalSystem.GetBlocksOfType(Projectors, x => x.BlockDefinition.SubtypeId.Contains("Projector"));
                GridTerminalSystem.GetBlocksOfType(ConsoleBlocks, x => x.BlockDefinition.SubtypeId.Contains("Console"));

                GridTerminalSystem.GetBlocksOfType(SafeZone);
                GridTerminalSystem.GetBlocksOfType(ControlPanels);
                GridTerminalSystem.GetBlocksOfType(ExhaustPipes, x => x.BlockDefinition.SubtypeId.Contains("Exhaustpipe"));
                GridTerminalSystem.GetBlocksOfType(TargetDummies);

                GridTerminalSystem.GetBlocksOfType(Seats, x => !x.CanControlShip);

                GridTerminalSystem.GetBlocksOfType(MedicalStations, x => x.BlockDefinition.SubtypeId.Contains("MedicalStation"));
                GridTerminalSystem.GetBlocksOfType(LabEquipments, x => x.BlockDefinition.SubtypeId.Contains("LabEquipment"));
            }
        }
    }
}
