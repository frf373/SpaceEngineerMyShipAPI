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
        /// 机械系统
        /// </summary>
        public class MechanicalSystem : ShipSystem
        {
            /// <summary>
            /// 普通转子
            /// </summary>
            public List<IMyMotorStator> MotorStators { get; set;  }

            /// <summary>
            /// 高级转子
            /// </summary>
            public List<IMyMotorAdvancedStator> MotorAdvancedStators {  get; set; }

            /// <summary>
            /// 铰链
            /// </summary>
            public List<IMyMotorAdvancedStator> Hinges {  get; set; }

            /// <summary>
            /// 活塞
            /// </summary>
            public List<IMyExtendedPistonBase> Pistons { get; set; }
            public MechanicalSystem(MyShip ship):base(ship)
            {
                
            }

            public override void InitializationBlock()
            {
                MotorStators = new List<IMyMotorStator>();
                MotorAdvancedStators = new List<IMyMotorAdvancedStator>();
                Hinges = new List<IMyMotorAdvancedStator>();
                Pistons = new List<IMyExtendedPistonBase>();

                List<IMyMotorStator> motorStators = new List<IMyMotorStator>();
                GridTerminalSystem.GetBlocksOfType(motorStators);
                foreach (var block in motorStators)
                {
                    string subtypeId = block.BlockDefinition.SubtypeId;
                    if (subtypeId.Contains("AdvancedStator"))
                    {
                        MotorAdvancedStators.Add(block as IMyMotorAdvancedStator);
                    }
                    else if (subtypeId.Contains("Hinge"))
                    {
                        Hinges.Add(block as IMyMotorAdvancedStator);
                    }
                    else if (subtypeId.Contains("Stator"))
                    {
                        MotorStators.Add(block as IMyMotorStator);
                    }
                }
                GridTerminalSystem.GetBlocksOfType(Pistons);
            }
        }
    }
}
