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
        /// 门禁系统
        /// </summary>
        public class DoorSystem : ShipSystem
        {
            /// <summary>
            /// 普通门，包括偏移门，滑动门
            /// </summary>
            public List<IMyDoor> Doors { get; set; }

            /// <summary>
            /// 大门
            /// </summary>
            public List<IMyDoor> Gates { get; set; }

            /// <summary>
            /// 吊门
            /// </summary>
            public List<IMyAirtightHangarDoor> AirtightHangarDoors { get; set; }
            //public List<>


            public DoorSystem(MyShip ship) : base(ship)
            {

            }

            public override void InitializationBlock()
            {
                Doors=new List<IMyDoor>();
                Gates=new List<IMyDoor>();
                AirtightHangarDoors = new List<IMyAirtightHangarDoor>();

                List<IMyDoor> doors=new List<IMyDoor>();
                GridTerminalSystem.GetBlocksOfType(doors);

                foreach(var block in doors)
                {
                    //这里不能用subtypeID，因为有两个subtypeID都是空的
                    string typeId = block.BlockDefinition.ToString();
                    
                    if(typeId.Contains("Gate"))
                    {
                        Gates.Add(block);
                    }
                    else if(typeId.Contains("AirtightHangarDoor"))
                    {
                        AirtightHangarDoors.Add(block as IMyAirtightHangarDoor);
                    }
                    else
                    {
                        Doors.Add(block);
                    }
                }
            }
        }
    }
}
