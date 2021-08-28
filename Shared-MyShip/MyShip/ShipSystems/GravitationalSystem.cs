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
        /// 引力系统
        /// </summary>
        public class GravitationalSystem:ShipSystem
        {
            /// <summary>
            /// 平面重力发生器
            /// </summary>
            public List<IMyGravityGenerator> GravityGenerators {  get; set; }

            /// <summary>
            /// 球形重力发生器
            /// </summary>
            public List<IMyGravityGeneratorSphere> GravityGeneratorSpheres {  get; set; }

            /// <summary>
            /// 人工虚拟质量
            /// </summary>
            public List<IMyArtificialMassBlock> ArtificialMassBlocks {  get; set; }

            /// <summary>
            /// 空间球
            /// </summary>
            public List<IMySpaceBall> SpaceBalls {  get; set; }
            public GravitationalSystem(MyShip ship):base(ship)
            {

            }

            public override void InitializationBlock()
            {
                GravityGenerators= new List<IMyGravityGenerator>();
                GravityGeneratorSpheres= new List<IMyGravityGeneratorSphere>();
                ArtificialMassBlocks=new List<IMyArtificialMassBlock>();
                SpaceBalls= new List<IMySpaceBall>();

                GridTerminalSystem.GetBlocksOfType(GravityGenerators);
                GridTerminalSystem.GetBlocksOfType(GravityGeneratorSpheres);
                GridTerminalSystem.GetBlocksOfType(ArtificialMassBlocks);
                GridTerminalSystem.GetBlocksOfType(SpaceBalls);
            }
        }
    }
}
