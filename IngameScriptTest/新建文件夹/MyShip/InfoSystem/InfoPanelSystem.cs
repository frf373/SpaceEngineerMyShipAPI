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
        /// 面板信息系统
        /// </summary>
        public class InfoPanelSystem
        {
            private MyShip ship;
            public InfoPanelSystem(MyShip ship)
            {
                this.ship = ship;
            }
            public bool WriteText(string content)
            {
                if (HasInfoPanel)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            /// <summary>
            /// 尝试用包含的字符串绑定系统信息面板
            /// </summary>
            /// <param name="containedName">包含的字符串</param>
            public void TryBindContainName(string containedName)
            {
                foreach(var block in ship.ShipSystems.AssistanceSystem.TextPanels)
                {
                    if (block.CustomName.ToUpper().Contains(containedName.ToUpper()))
                    {
                        BoundName = containedName;
                        Panel = block;
                        Panel.ContentType = ContentType.TEXT_AND_IMAGE;
                        break;
                    }
                }
            }
            /// <summary>
            /// 绑定的名字
            /// </summary>
            public string BoundName { get; set; }
            /// <summary>
            /// 绑定的系统信息面板
            /// </summary>
            public IMyTextSurface Panel { get; set; }

            /// <summary>
            /// 是否有系统信息面板
            /// </summary>
            public bool HasInfoPanel => Panel != null ? true : false;

        }
    }
}
