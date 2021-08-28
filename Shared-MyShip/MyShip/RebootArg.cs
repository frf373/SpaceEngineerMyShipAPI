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
        [Flags]
        public enum RebootArg
        {
            None=0,
            Assistance=1,
            Communication=2,
            Connection=4,
            Control=8,
            Detection=16,
            Door=32,
            Dynamic=64,
            Electric=128,
            Gas=256,
            Gravitational=512,
            Landing=1024,
            LCD=2048,
            LifeSupport=4096,
            Light=8192,
            Mechanical=16384,
            Production=32768,
            Storage=65536,
            Tool=131072,
            Trade=262144,
            Weapon=524288,
            All=1048575
        }
    }
}
