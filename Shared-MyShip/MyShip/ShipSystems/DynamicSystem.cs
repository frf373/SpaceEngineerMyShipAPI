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
        /// 动力系统
        /// </summary>
        public class DynamicSystem : ShipSystem
        {
            /// <summary>
            /// 全部推进器
            /// </summary>
            public List<IMyThrust> Thrusts { get; set; }

            /// <summary>
            /// 离子推进器,包括科幻离子推进器
            /// </summary>
            public List<IMyThrust> IonThrusts { get; set; }

            /// <summary>
            /// 氢动力推进器，包括工业氢动力推进器
            /// </summary>
            public List<IMyThrust> HydrogenThrusts { get; set; }

            /// <summary>
            /// 空气动力推进器，包括科幻空气推进器
            /// </summary>
            public List<IMyThrust> AtmosphericThrusts { get; set; }

            /// <summary>
            /// 其他推进器
            /// </summary>
            public List<IMyThrust> OtherThrusts { get; set; }

            /// <summary>
            /// 车轮悬挂
            /// </summary>
            public List<IMyMotorSuspension> MotorSuspensions { get; set; }

            /// <summary>
            /// 陀螺仪
            /// </summary>
            public List<IMyGyro> Gyros { get; set; }

            /// <summary>
            /// 跃迁引擎
            /// </summary>
            public List<IMyJumpDrive> JumpDrives { get; set; }
            public DynamicSystem(MyShip ship):base(ship)
            {

            }

            public override void InitializationBlock()
            {
                Thrusts = new List<IMyThrust>();
                IonThrusts = new List<IMyThrust>();
                HydrogenThrusts = new List<IMyThrust>();
                AtmosphericThrusts = new List<IMyThrust>();
                OtherThrusts= new List<IMyThrust>();

                MotorSuspensions= new List<IMyMotorSuspension>();
                Gyros = new List<IMyGyro>();
                JumpDrives = new List<IMyJumpDrive>();

                GridTerminalSystem.GetBlocksOfType(Thrusts);
                foreach(var block in Thrusts)
                {
                    string subtypeId = block.BlockDefinition.SubtypeId;
                    if(subtypeId.Contains("Hydrogen"))
                    {
                        HydrogenThrusts.Add(block);
                    }
                    else if(subtypeId.Contains("Atmospheric"))
                    {
                        AtmosphericThrusts.Add(block);
                    }
                    else if(subtypeId.Contains("SmallThrust")||subtypeId.Contains("LargeThrust"))
                    {
                        IonThrusts.Add(block);
                    }
                    else
                    {
                        OtherThrusts.Add(block);
                    }
                }

                GridTerminalSystem.GetBlocksOfType(MotorSuspensions);
                GridTerminalSystem.GetBlocksOfType(Gyros);
                GridTerminalSystem.GetBlocksOfType(JumpDrives);
            }
        }
    }
}
