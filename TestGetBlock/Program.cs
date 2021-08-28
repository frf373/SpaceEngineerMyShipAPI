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
    partial class Program : MyGridProgram
    {
        public List<IMyTerminalBlock> terminalBlocks = new List<IMyTerminalBlock>();
        /*public List<IMySlimBlock> slimBlocks=new List<IMySlimBlock>();
        public List<IMyCubeBlock> cubeBlocks=new List<IMyCubeBlock>();
        public IMyCubeGrid CubeGrid => Me.CubeGrid;

        Vector3I min3I;
        Vector3I max3I;*/
        public Program()
        {
            Me.CustomData = "";
            StringBuilder builder = new StringBuilder();
            int count = 1;

            /*min3I = CubeGrid.Min;
            max3I = CubeGrid.Max;

            for(int i=min3I.X;i<=max3I.X;i++)
            {
                for(int j=min3I.Y;j<=max3I.Y;j++)
                {
                    for(int k=min3I.Z;k<=max3I.Z;k++)
                    {
                        Vector3I temp=new Vector3I(i,j,k);
                        builder.AppendLine(temp+CubeGrid.CubeExists(temp).ToString());
                        if(CubeGrid.GetCubeBlock(temp) !=null)
                        {
                            slimBlocks.Add(CubeGrid.GetCubeBlock(temp));
                        }
                    }
                }
            }
            foreach(var slim in slimBlocks)
            {
                builder.Append($"[{count}]-");
                builder.AppendLine($"[Slim]{slim.BlockDefinition}");
                builder.AppendLine($"[BuildIntegrity][{slim.BuildIntegrity}]");
                builder.AppendLine($"[BuildLevelRation][{slim.BuildLevelRatio}]");
                builder.AppendLine($"[CurrentDamage][{slim.CurrentDamage}]");
                builder.AppendLine($"[DamageRatio][{slim.DamageRatio}]");
                builder.AppendLine($"[HasDeformation][{slim.HasDeformation}]");
                builder.AppendLine($"[IsDestroyed][{slim.IsDestroyed}]");
                builder.AppendLine($"[IsFullIntegrity][{slim.IsFullIntegrity}]");
                builder.AppendLine($"[MaxDeformation][{slim.MaxDeformation}]");
                builder.AppendLine($"[MaxIntergrity][{slim.MaxIntegrity}]");
                if(slim.FatBlock!=null)
                {
                    cubeBlocks.Add(slim.FatBlock);
                    builder.Append($"[{count}]-");
                    builder.Append($"[Cube]{slim.FatBlock.BlockDefinition}");
                    builder.AppendLine($"[DsRt]{slim.FatBlock.DisassembleRatio}");
                }
                count++;
            }
            */

            GridTerminalSystem.GetBlocksOfType(terminalBlocks);
            
            
            
            foreach(var block in terminalBlocks)
            {
                
                builder.Append("["+count+++"]");
                builder.Append("[" + block.BlockDefinition + "]");
                builder.Append("[" + block.CustomName + "]\n");
            }
            

            string info = builder.ToString();

            Echo(info);
            Me.CustomData += info;
            
        }

        public void Save()
        {
        }

        public void Main(string argument, UpdateType updateSource)
        {
        }
    }
}
