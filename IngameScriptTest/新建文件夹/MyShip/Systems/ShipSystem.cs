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
        /// 飞船系统基类
        /// </summary>
        public class ShipSystem
        {
            /// <summary>
            /// 飞船接口
            /// </summary>
            private MyShip ship;
            /// <summary>
            /// 此系统的所有方块集合
            /// </summary>
            public List<IMyTerminalBlock> SystemBlocks { get; set; }

            public void WriteSystemInfo()
            {
                if (InfoPanelSystem.Panel != null)
                {
                    InfoPanelSystem.Panel.WriteText(GetSystemInfo());
                }
            }

            /// <summary>
            /// 获得系统基本信息
            /// </summary>
            public virtual string GetSystemInfo(InfoLevel level = InfoLevel.Overview)
            {
                return "这是飞船系统基类,无系统信息";
            }

            public ShipSystem(MyShip ship)
            {
                this.ship = ship;
                SystemBlocks = new List<IMyTerminalBlock>();
                InfoPanelSystem = new InfoPanelSystem(ship);
            }
            /// <summary>
            /// 面板信息系统
            /// </summary>
            public InfoPanelSystem InfoPanelSystem { get; set; }

            /// <summary>
            /// 输出一个方块的定义、类型和名字到控制台和自定义数据中
            /// </summary>
            public void DebugToBoth(IMyTerminalBlock block)
            {
                string debugContent = "[BlockDefinition:" + block.BlockDefinition +
                    "]-[SubtypeId:" + block.BlockDefinition.SubtypeId +
                    "]-[CustomName:" + block.CustomName + "]";
                ship.Echo(debugContent);
                ship.Me.CustomData += debugContent;
            }
            /// <summary>
            /// 返回百分比的图
            /// </summary>
            /// <param name="current">现有数字</param>
            /// <param name="max">最大数字</param>
            /// <returns>百分比图，样例[--|__|]--[12.34%]</returns>
            protected string DrawPercentPic(float current, float max)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("[");
                int count = Convert.ToInt32(current * 50 / max);
                for (int i = 1; i <= count; i++)
                {
                    builder.Append("|");
                }
                for (int i = count + 1; i <= 50; i++)
                {
                    builder.Append("'");
                }
                builder.Append("]--");
                builder.Append("[" + (current / max).ToString("P") + "]");
                return builder.ToString();
            }
        }
    }
}
