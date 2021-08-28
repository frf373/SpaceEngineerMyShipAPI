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
        MyShip ship;
        public Program()
        {

            ship = new MyShip(this);
            ship.CustomFuncs.AddFunc(new TestFunc(this, ship, "TestFunc", "TestFuncTestAlll"));
            //ship.CustomFuncs.AddFunc(new AutoDoorFunc(this, ship, "AutoDoor","CopyTestABCDEDGH"));/////////////here!
            
            Runtime.UpdateFrequency = UpdateFrequency.Update1;
        }

        public void Save()
        {
            ship.RunSave();
        }
        
        public void Main(string arg, UpdateType source)
        {
            ship.RunAlwaysFunc(arg, source);

            if (Convert.ToBoolean( source & (UpdateType.Once|UpdateType.Update1|UpdateType.Update10|UpdateType.Update100)))
            {
                ship.RunAllCircleFunc();
            }
            else
            {
                ship.RunArgFunc(arg, source);
            }
        }
    }
}
