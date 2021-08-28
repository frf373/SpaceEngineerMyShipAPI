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
        /// 交易系统
        /// </summary>
        public class TradeSystem : ShipSystem
        {
            /// <summary>
            /// 商店
            /// </summary>
            public List<IMyStoreBlock> Stores {  get; set; }

            /// <summary>
            /// 售货机
            /// </summary>
            public List<IMyStoreBlock> VendingMachines { get; set; }

            /// <summary>
            /// 食物贩卖机
            /// </summary>
            public List<IMyStoreBlock> FoodDispensers { get; set; }

            /// <summary>
            /// Atm机
            /// </summary>
            public List<IMyStoreBlock> Atms { get; set; }

            /// <summary>
            /// 合约机
            /// </summary>
            public List<IMyTerminalBlock> Contracts {  get; set; }

            public override void InitializationBlock()
            {
                Stores = new List<IMyStoreBlock>();
                VendingMachines = new List<IMyStoreBlock>();
                FoodDispensers = new List<IMyStoreBlock>();
                Atms = new List<IMyStoreBlock>();
                Contracts = new List<IMyTerminalBlock>();

                List<IMyStoreBlock> storeBlocks = new List<IMyStoreBlock>();
                GridTerminalSystem.GetBlocksOfType(storeBlocks);
                foreach (var block in storeBlocks)
                {
                    string typeId = block.BlockDefinition.SubtypeId;
                    if (typeId.Contains("StoreBlock"))
                    {
                        Stores.Add(block);
                    }
                    else if (typeId.Contains("AtmBlock"))
                    {
                        Atms.Add(block);
                    }
                    else if (typeId.Contains("FoodDispenser"))
                    {
                        FoodDispensers.Add(block);
                    }
                    else if (typeId.Contains("VendingMachine"))
                    {
                        VendingMachines.Add(block);
                    }
                }
                GridTerminalSystem.GetBlocksOfType(Contracts, x => x.BlockDefinition.SubtypeId.Contains("ContractBlock"));
            }
            public TradeSystem(MyShip ship):base(ship)
            {
                

            }
        }
    }
}
