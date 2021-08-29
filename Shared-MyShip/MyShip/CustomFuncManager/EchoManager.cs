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
        public partial class CustomFuncManager
        {
            /// <summary>
            /// 获取而所有功能要Echo的内容缓存
            /// </summary>
            /// <returns></returns>
            public string GetAllEchoCache()
            {
                StringBuilder builder = new StringBuilder();
                foreach (CustomFuncBase func in UIDToFunc.Values)
                {
                    builder.Append(func.CustomEcho.GetContent());
                }
                return builder.ToString();
            }
        }
    }
}
