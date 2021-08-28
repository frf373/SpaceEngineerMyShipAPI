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
        /// 生产系统
        /// </summary>
        public class ProductionSystem : ShipSystem
        {
            /// <summary>
            /// 基础装配机
            /// </summary>
            public List<IMyAssembler> BasicAssemblers {  get; set; }

            /// <summary>
            /// 装配机,包括工业装配机
            /// </summary>
            public List<IMyAssembler> Assemblers {  get; set; }

            /// <summary>
            /// 其他装配机
            /// </summary>
            public List<IMyAssembler> OtherAssemblers {  get; set; }

            /// <summary>
            /// 基础精炼厂
            /// </summary>
            public List<IMyRefinery> BasicRefineries {  get; set; }

            /// <summary>
            /// 精炼厂，包括工业精炼厂
            /// </summary>
            public List<IMyRefinery> Refineries {  get; set; }

            /// <summary>
            /// 其他精炼厂
            /// </summary>
            public List<IMyRefinery> OtherRefineries {  get; set; }

            /// <summary>
            /// 速度升级模块
            /// </summary>
            public List<IMyUpgradeModule> SpeedModules {  get; set; }

            /// <summary>
            /// 电力效率升级模块
            /// </summary>
            public List<IMyUpgradeModule> PowerModules {  get; set; }

            /// <summary>
            /// 精炼升级模块
            /// </summary>
            public List<IMyUpgradeModule> YieldModules {  get; set; }
            public ProductionSystem(MyShip ship):base(ship)
            {
                
            }

            public override void InitializationBlock()
            {
                BasicAssemblers=new List<IMyAssembler>();
                Assemblers=new List<IMyAssembler>();
                OtherAssemblers=new List<IMyAssembler>();

                BasicRefineries = new List<IMyRefinery>();
                Refineries=new List<IMyRefinery>();
                OtherRefineries=new List<IMyRefinery>();

                SpeedModules=new List<IMyUpgradeModule>();
                PowerModules=new List<IMyUpgradeModule>();
                YieldModules = new List<IMyUpgradeModule>();

                List<IMyAssembler> assemblers= new List<IMyAssembler>();
                GridTerminalSystem.GetBlocksOfType(assemblers);
                foreach(var block in assemblers)
                {
                    string typeId = block.BlockDefinition.SubtypeId;
                    if(typeId.Contains("LargeAssembler"))
                    {
                        Assemblers.Add(block);
                    }
                    else if(typeId.Contains("BasicAssembler"))
                    {
                        BasicAssemblers.Add(block);
                    }
                    else if(!typeId.Contains("SurvivalKit"))
                    {
                        OtherAssemblers.Add(block);
                    }
                }

                List<IMyRefinery> refineries=new List<IMyRefinery>();
                GridTerminalSystem.GetBlocksOfType(refineries);
                foreach(var block in refineries)
                {
                    string typeId = block.BlockDefinition.SubtypeId;
                    if(typeId.Contains("Refinery"))
                    {
                        Refineries.Add(block);
                    }
                    else if(typeId.Contains("Blast"))
                    {
                        BasicRefineries.Add(block);
                    }
                    else
                    {
                        OtherRefineries.Add(block);
                    }
                }

                GridTerminalSystem.GetBlocksOfType(SpeedModules, x => x.BlockDefinition.SubtypeId.Contains("Productivity"));
                GridTerminalSystem.GetBlocksOfType(PowerModules, x => x.BlockDefinition.SubtypeId.Contains("Energy"));
                GridTerminalSystem.GetBlocksOfType(YieldModules, x => x.BlockDefinition.SubtypeId.Contains("Effectiveness"));
            }
        }
    }
}
