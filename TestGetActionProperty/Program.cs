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
using System.Xml.Serialization;
namespace IngameScript
{
    partial class Program : MyGridProgram
    {
        
        /*List<ITerminalAction> actions=new List<ITerminalAction>();
        List<ITerminalProperty> properties = new List<ITerminalProperty>();
        IMyTerminalBlock terminalBlock;*/

        public Program()
        {
            /*Me.CustomData = "";
            terminalBlock=GridTerminalSystem.GetBlockWithName("Test");
            terminalBlock.GetActions(actions);
            terminalBlock.GetProperties(properties);
            StringBuilder builder=new StringBuilder();
            builder.AppendLine("Actions");
            foreach(var action in actions)
            {
                builder.AppendLine("[ID:"+action.Id+"][Name:"+action.Name+"]");
            }
            builder.AppendLine("Properties");
            foreach(var property in properties)
            {
                builder.AppendLine("[ID:" + property.Id + "][TypeName:" + property.TypeName + "]");
            }
            string content=builder.ToString();
            Echo(content);
            Me.CustomData += content;*/

            Me.CustomData = "";
            
            Runtime.UpdateFrequency = UpdateFrequency.Once;
            
        }
        public MyIni ini = new MyIni();
        public void Main(string argument, UpdateType updateSource)
        {
            ini.TryParse(GridTerminalSystem.GetBlockWithName("TestIni").CustomData);
            Echo(ini.Get("Test", "Good").Key.Section);
            Echo(ini.Get("Test", "Good").Key.Name);
            Echo(ini.Get("Test", "Good").Key.ToString());
            Echo(ini.Get("Test", "Good").ToString());
            
            /*if(argument==null)
            {
                Me.CustomData += "null" + updateSource;
            }
            else if(argument=="")
            {
                Me.CustomData += "no" + updateSource;
            }
            else
            {
                Me.CustomData += argument + updateSource;
            }
            Me.CustomData += "\n";
            if(argument=="100")
            {
                Runtime.UpdateFrequency = UpdateFrequency.Update100;
            }*/
        }
    }
}
