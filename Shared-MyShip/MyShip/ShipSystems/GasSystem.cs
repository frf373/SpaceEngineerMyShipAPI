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
        /// 气体系统
        /// </summary>
        public class GasSystem:ShipSystem
        {
            /// <summary>
            /// 气阀
            /// </summary>
            public List<IMyAirVent> AirVents { get; set; }
            /// <summary>
            /// 氧气农场
            /// </summary>
            public List<IMyOxygenFarm> OxygenFarms {  get; set; }
            /// <summary>
            /// 氧氢生成器
            /// </summary>
            public List<IMyGasGenerator> OxygenGenerators {  get; set; }
            /// <summary>
            /// 其他气体生成器，如甲烷
            /// </summary>
            public List<IMyGasGenerator> OtherGasGenerators { get;set;}

            /// <summary>
            /// 氢气罐
            /// </summary>
            public List<IMyGasTank> HydrogenTanks {  get; set; }

            /// <summary>
            /// 氧气罐
            /// </summary>
            public List<IMyGasTank> OxygenTanks {  get; set; }

            /// <summary>
            /// 其他气罐，如甲烷
            /// </summary>
            public List<IMyGasTank> OtherGasTanks { get; set;  }
            public GasSystem(MyShip ship):base(ship)
            {


            }

            public override void InitializationBlock()
            {
                AirVents = new List<IMyAirVent>();
                OxygenFarms = new List<IMyOxygenFarm>();
                OxygenGenerators = new List<IMyGasGenerator>();
                OtherGasGenerators = new List<IMyGasGenerator>();
                HydrogenTanks = new List<IMyGasTank>();
                OxygenTanks = new List<IMyGasTank>();
                OtherGasTanks = new List<IMyGasTank>();

                GridTerminalSystem.GetBlocksOfType(AirVents);
                GridTerminalSystem.GetBlocksOfType(OxygenFarms);

                List<IMyGasGenerator> gasGenerators = new List<IMyGasGenerator>();
                GridTerminalSystem.GetBlocksOfType(gasGenerators);
                foreach (var block in gasGenerators)
                {
                    string subtypeId = block.BlockDefinition.SubtypeId;
                    if (subtypeId == "" || subtypeId.Contains("OxygenGenerator"))
                    {
                        OxygenGenerators.Add(block);
                    }
                    else
                    {
                        OtherGasGenerators.Add(block);
                    }
                }

                List<IMyGasTank> gasTanks = new List<IMyGasTank>();
                GridTerminalSystem.GetBlocksOfType(gasTanks);
                foreach (var block in gasTanks)
                {
                    string subtypeId = block.BlockDefinition.SubtypeId;
                    if (subtypeId == "" || subtypeId.Contains("OxygenTank"))
                    {
                        OxygenTanks.Add(block);
                    }
                    else if (subtypeId.Contains("HydrogenTank"))
                    {
                        HydrogenTanks.Add(block);
                    }
                    else
                    {
                        OtherGasTanks.Add(block);
                    }
                }
            }
        }
    }
}
