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
        public partial class CustomFuncBase
        {
            public class Custom_Ini: MyIni
            {
                private CustomFuncBase func;
                public Custom_Ini(CustomFuncBase func):base()
                {
                    this.func=func;
                }
                
                public void ReadBlockIni(IMyTerminalBlock block,string section)
                {
                    TryParse(func.Get_CustomData(block));
                    func.Get_CustomData(block);
                    List<MyIniKey> keys = new List<MyIniKey>();
                    foreach(var key in keys)
                    {
                        //key.
                    }
                }

                /*public void AddBlockIni(IMyTerminalBlock block,string section)
                {

                }*/
            }
        }
    }
}
