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
        /// 仓储系统
        /// </summary>
        public class StorageSystem : ShipSystem
        {
            /// <summary>
            /// 储存箱
            /// </summary>
            public List<IMyCargoContainer> CargoContainers {  get; set; }

            /// <summary>
            /// 武器架
            /// </summary>
            public List<IMyCargoContainer> WeaponRack { get; set; }

            /// <summary>
            /// 军工厂
            /// </summary>
            public List<IMyCargoContainer> Armories {  get; set; }

            /// <summary>
            /// 武器柜
            /// </summary>
            public List<IMyCargoContainer> ArmoryLockers {  get; set; }

            /// <summary>
            /// 衣柜
            /// </summary>
            public List<IMyCargoContainer> Lockers {  get; set; }

            /// <summary>
            /// 其他容器
            /// </summary>
            public List<IMyCargoContainer> OtherContainers {  get; set; }

            //Freight 1 Large Grid: MyObjectBuilder_CubeBlock/Freight1

            public StorageSystem(MyShip ship):base(ship)
            {
                
            }

            public override void InitializationBlock()
            {
                CargoContainers=new List<IMyCargoContainer>();
                WeaponRack=new List<IMyCargoContainer>();
                Armories=new List<IMyCargoContainer>();
                ArmoryLockers=new List<IMyCargoContainer>();
                Lockers=new List<IMyCargoContainer>();
                OtherContainers=new List<IMyCargoContainer>();

                List<IMyCargoContainer> cargoContainers= new List<IMyCargoContainer>();
                GridTerminalSystem.GetBlocksOfType(cargoContainers);
                foreach(var block in  cargoContainers)
                {
                    string typeId = block.BlockDefinition.SubtypeId;
                    if(typeId.Contains("Container"))
                    {
                        //SetBlockShowState(block,BlockShowState.Inventory);
                        CargoContainers.Add(block);
                    }
                    else if(typeId.Contains("Lockers"))
                    {
                        //SetBlockShowState(block);
                        Lockers.Add(block);
                    }
                    else if(typeId.Contains("WeaponRack"))
                    {
                        //SetBlockShowState(block,BlockShowState.Inventory);
                        WeaponRack.Add(block);
                    }
                    else if(typeId.Contains("LockerRoomCorner"))
                    {
                        //SetBlockShowState(block);
                        ArmoryLockers.Add(block);
                    }
                    else if(typeId.Contains("LockerRoom"))
                    {
                        //SetBlockShowState(block);
                        Armories.Add(block);
                    }
                    else
                    {
                        OtherContainers.Add(block);
                    }
                }    
            }
        }
    }
}
