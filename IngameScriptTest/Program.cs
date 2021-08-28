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
        public MyShip myShip;
         
        public Program()
        {
            myShip = new MyShip(this);
            myShip.ShipSystems.ElectricSystem.InfoPanelSystem.Panel = GridTerminalSystem.GetBlockWithName("BatteryPanel") as IMyTextSurface;
            Runtime.UpdateFrequency = UpdateFrequency.Update100;
        }
        
        public void Save()
        {
            
        }

        public void Main(string argument, UpdateType updateSource)
        {
            myShip.ShipSystems.ElectricSystem.WriteSystemInfo();
        }
    }
}
