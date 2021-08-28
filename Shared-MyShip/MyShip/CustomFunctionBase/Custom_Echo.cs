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
        abstract public partial class CustomFuncBase
        {
            protected class Custom_Echo
            {
                private CustomFuncBase func;
                public Custom_Echo(CustomFuncBase func)
                {
                    this.func = func;
                    Echo_CacheCounter = -1;
                    contentBuilder=new StringBuilder();
                }

                /// <summary>
                /// 输出信息缓存计数器
                /// </summary>
                public int Echo_CacheCounter { get; set; }

                private StringBuilder contentBuilder;
                public void Echo_Builder(string content)
                {
                    Echo_CacheCounter = 0;
                    contentBuilder.Append(content);
                }

                /// <summary>
                /// 是否有内容需要刷新出去
                /// </summary>
                public bool HasContent => contentBuilder.Length != 0;
                public string Echo_Flush()
                {
                    string content = "[" + func.FuncName + "]" + "\n" + contentBuilder.ToString();
                    contentBuilder.Clear();
                    return content;
                }
            }
            
        }
    }
}
