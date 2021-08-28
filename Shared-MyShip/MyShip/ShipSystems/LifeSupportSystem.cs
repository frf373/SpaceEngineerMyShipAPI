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
        /// 生存系统
        /// </summary>
        public class LifeSupportSystem:ShipSystem
        {
            /// <summary>
            /// 休眠仓
            /// </summary>
            public List<IMyCryoChamber> CryoChambers {  get; set; }
            /// <summary>
            /// 床
            /// </summary>
            public List<IMyCryoChamber> Beds {  get; set; }

            /// <summary>
            /// 医疗站
            /// </summary>
            public List<IMyMedicalRoom> MedicalRooms {  get; set; }

            /// <summary>
            /// 求生装置
            /// </summary>
            public List<IMyAssembler> SurvivalKits {  get; set; }

            public LifeSupportSystem(MyShip ship):base(ship)
            {
                
            }

            public override void InitializationBlock()
            {
                CryoChambers=new List<IMyCryoChamber>();
                Beds=new List<IMyCryoChamber>();
                MedicalRooms=new List<IMyMedicalRoom>();
                SurvivalKits=new List<IMyAssembler>();

                GridTerminalSystem.GetBlocksOfType(CryoChambers, x => x.BlockDefinition.SubtypeId.Contains("CryoChamber"));
                GridTerminalSystem.GetBlocksOfType(Beds, x => x.BlockDefinition.SubtypeId.Contains("Bed"));
                GridTerminalSystem.GetBlocksOfType(MedicalRooms);
                GridTerminalSystem.GetBlocksOfType(SurvivalKits, x => x.BlockDefinition.SubtypeId.Contains("SurvivalKit"));
            }
        }
    }
}
