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
        /// 灯光系统
        /// </summary>
        public class LightSystem : ShipSystem
        {
            /// <summary>
            /// 室内灯，包括单角灯和双角灯
            /// </summary>
            public List<IMyInteriorLight> InteriorLights {  get; set; }

            /// <summary>
            /// 偏移灯
            /// </summary>
            public List<IMyInteriorLight> OffsetLights {  get; set; }

            /// <summary>
            /// 通道灯
            /// </summary>
            public List<IMyInteriorLight> PassageLight {  get; set; }
            
            /// <summary>
            /// 聚光灯
            /// </summary>
            public List<IMyReflectorLight> SpotLights {  get; set; }

            /// <summary>
            /// 旋转灯，注意：旋转速度设置不了
            /// </summary>
            public List<IMyReflectorLight> RotatingLights {  get; set; }

            /// <summary>
            /// 偏移聚光灯
            /// </summary>
            public List<IMyReflectorLight> OffsetSpotLights {  get; set; }
            public LightSystem(MyShip ship):base(ship)
            {

            }

            public override void InitializationBlock()
            {
                InteriorLights = new List<IMyInteriorLight>();
                OffsetLights = new List<IMyInteriorLight>();
                PassageLight= new List<IMyInteriorLight>();

                SpotLights=new List<IMyReflectorLight>();
                RotatingLights = new List<IMyReflectorLight>();
                OffsetSpotLights = new List<IMyReflectorLight>();

                List<IMyInteriorLight> interiorLights = new List<IMyInteriorLight>();
                GridTerminalSystem.GetBlocksOfType(interiorLights);
                foreach(var block in  interiorLights)
                {
                    string subtypeId = block.BlockDefinition.SubtypeId;
                    if (subtypeId.Contains("OffsetLight"))
                    {
                        OffsetLights.Add(block);
                    }
                    else if(subtypeId.Contains("Passage"))
                    {
                        PassageLight.Add(block);
                    }
                    else
                    {
                        InteriorLights.Add(block);
                    }
                }

                GridTerminalSystem.GetBlocksOfType(SpotLights, x => x.BlockDefinition.SubtypeId.Contains("FrontLight"));
                GridTerminalSystem.GetBlocksOfType(RotatingLights, x => x.BlockDefinition.SubtypeId.Contains("Rotating"));
                GridTerminalSystem.GetBlocksOfType(OffsetSpotLights, x => x.BlockDefinition.SubtypeId.Contains("OffsetSpot"));
            }
        }
    }
}
