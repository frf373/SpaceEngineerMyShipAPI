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
        public class TestFunc:CustomFuncBase
        {   

            public TestFunc(Program program,MyShip ship,string funcName,string uid):base(program,ship,funcName,uid)
            {
                /*MyIni ini=new MyIni();
                ini.TryParse(GridTerminalSystem.GetBlockWithName("TestIni").CustomData);
                Echo(ini.Get("Test", "Good").Key.Section);
                Echo(ini.Get("Test", "Good").Key.Name);
                Echo(ini.Get("Test", "Good").Key.ToString());
                Echo(ini.Get("Test", "Good").ToString());*/
            }

        }
    }
}
