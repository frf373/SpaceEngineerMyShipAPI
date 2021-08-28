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
        /// 电力系统
        /// </summary>
        public class ElectricSystem : ShipSystem
        {
            /// <summary>
            /// 所有电力元件
            /// </summary>
            public List<IMyPowerProducer> PowerProducers {  get; set; }

            /// <summary>
            /// 电池
            /// </summary>
            public List<IMyBatteryBlock> Batteries { get; set; }

            /// <summary>
            /// 反应堆
            /// </summary>
            public List<IMyReactor> Reactors { get; set; }

            /// <summary>
            /// 太阳能板
            /// </summary>
            public List<IMySolarPanel> SolarPanels { get; set; }

            /// <summary>
            /// 风力涡轮机
            /// </summary>
            public List<IMyPowerProducer> WindTurbines { get; set; }

            /// <summary>
            /// 氢气发电机
            /// </summary>
            public List<IMyPowerProducer> HydrogenEngines { get; set; }

            /// <summary>
            /// 其他电力元件，比如mod中的化石能发电机
            /// </summary>
            public List<IMyPowerProducer> OtherPowerProducers { get; set; }

            public override void InitializationBlock()
            {
                PowerProducers = new List<IMyPowerProducer>();
                Batteries = new List<IMyBatteryBlock>();
                Reactors = new List<IMyReactor>();
                SolarPanels = new List<IMySolarPanel>();
                WindTurbines = new List<IMyPowerProducer>();
                HydrogenEngines = new List<IMyPowerProducer>();
                OtherPowerProducers = new List<IMyPowerProducer>();

                 
                GridTerminalSystem.GetBlocksOfType<IMyPowerProducer>(PowerProducers);
                foreach (var block in PowerProducers)
                {
                    if (block is IMyBatteryBlock)
                    {
                        //SetBlockShowState(block,BlockShowState.Terminal|BlockShowState.Toolbar);
                        Batteries.Add(block as IMyBatteryBlock);
                    }
                    else if (block is IMyReactor)
                    {
                        //SetBlockShowState(block, BlockShowState.Public);
                        Reactors.Add(block as IMyReactor);
                    }
                    else if (block is IMySolarPanel)
                    {
                        //SetBlockShowState(block, BlockShowState.Terminal);
                        SolarPanels.Add(block as IMySolarPanel);
                    }
                    else if (block.BlockDefinition.SubtypeId.Contains("WindTurbine"))
                    {
                        //SetBlockShowState(block, BlockShowState.Terminal);
                        WindTurbines.Add(block as IMyPowerProducer);
                    }
                    else if (block.BlockDefinition.SubtypeId.Contains("HydrogenEngine"))
                    {
                        //SetBlockShowState(block, BlockShowState.Terminal | BlockShowState.Toolbar);
                        HydrogenEngines.Add(block as IMyPowerProducer);
                    }
                    //Tips:The following code is used for compatible with mod
                    else
                    {
                        OtherPowerProducers.Add(block as IMyPowerProducer);
                    }
                }
            }
            public ElectricSystem(MyShip ship) : base(ship)
            {
                
            }

            private string[] storedPowerUnit = new string[] { "MWh", "KWh", "Wh" };
            private string[] exchangePowerUnit = new string[] { "MW", "KW", "W" };
            /// <summary>
            /// 获得
            /// </summary>
            /// <param name="current">当前值</param>
            /// <param name="max">最大值</param>
            /// <param name="unit">计量单位字符串组，从大到小，如"MWh","KWh"</param>
            /// <returns>样例:[12]</returns>
            protected string DrawWithUnit(float current, float max, string[] unit)
            {
                int currentUnitNum = 0, maxUnitNum = 0;
                for (int i = 0; i < unit.Count(); i++)
                {
                    if (current < 1.0)
                    {
                        current *= 1000f;
                    }
                    else
                    {
                        currentUnitNum = i;
                        break;
                    }
                }
                for (int i = 0; i < unit.Count(); i++)
                {
                    if (max < 1.0)
                    {
                        max *= 1000f;
                    }
                    else
                    {
                        maxUnitNum = i;
                        break;
                    }
                }
                ////////////////////F2
                return "[" + current.ToString("F2") + unit[currentUnitNum] + "/" + max + unit[maxUnitNum] + "]";
            }
            public override string GetSystemInfo(InfoLevel level = InfoLevel.Overview)
            {
                StringBuilder infoBuilder = new StringBuilder();

                infoBuilder.AppendLine("<<" + "电力系统信息面板" + ">>");
                float currentInput = 0, currentOutput = 0, currentStoredPower = 0;
                float maxInput = 0, maxOutput = 0, maxStoredPower = 0;
                foreach (var block in Batteries)
                {
                    currentInput += block.CurrentInput;
                    currentOutput += block.CurrentOutput;
                    currentStoredPower += block.CurrentStoredPower;
                    maxInput += block.MaxInput;
                    maxOutput += block.MaxOutput;
                    maxStoredPower += block.MaxStoredPower;
                }
                infoBuilder.AppendLine("<" + "电池组信息" + ">");
                infoBuilder.AppendLine("[" + "电量储存" + "]----" + DrawWithUnit(currentStoredPower, maxStoredPower, storedPowerUnit));
                infoBuilder.AppendLine(DrawPercentPic(currentStoredPower, maxStoredPower));
                infoBuilder.AppendLine("[" + "放电功率" + "]----" + DrawWithUnit(currentOutput, maxOutput, exchangePowerUnit));
                infoBuilder.AppendLine(DrawPercentPic(currentOutput, maxOutput));
                infoBuilder.AppendLine("[" + "充电功率"+ "]----" + DrawWithUnit(currentInput, maxInput, exchangePowerUnit));
                infoBuilder.AppendLine(DrawPercentPic(currentInput, maxInput));

                /*foreach(var block in Reactors)
                {
                    
                }*/

                return infoBuilder.ToString();
            }
        }
    }
}
