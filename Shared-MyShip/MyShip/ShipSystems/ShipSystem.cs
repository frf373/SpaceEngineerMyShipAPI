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
        abstract public class ShipSystem
        {
            /// <summary>
            /// 飞船接口
            /// </summary>
            private MyShip ship;

            /// <summary>
            /// 飞船语言翻译功能接口
            /// </summary>
            public string Translate(string chineseContent)
            {
                return ship.Translate(chineseContent);
            }

            /// <summary>
            /// 本程序块接口
            /// </summary>
            public IMyTerminalBlock Me => ship.Me;

            /// <summary>
            /// 网格终端系统接口
            /// </summary>
            public IMyGridTerminalSystem GridTerminalSystem => ship.GridTerminalSystem;

            /// <summary>
            /// 调试输出接口
            /// </summary>
            public Action<string> Echo => ship.Echo;

            /// <summary>
            /// 输出信息到面板上
            /// </summary>
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
                return Translate("这是飞船系统基类,无系统信息");
            }

            /// <summary>
            /// 初始化方块,在基类中为abstract，并且在基类构造函数中调用过，无需在派生类构造函数中再调用
            /// </summary>
            abstract public void InitializationBlock();

            /// <summary>
            /// 设置方块展示状态
            /// </summary>
            /// <param name="block">要设置的方块</param>
            /// <param name="state">展示状态,按照位运算,1终端显示，2库存显示，4工具栏显示</param>
            public void SetBlockShowState(IMyTerminalBlock block,BlockShowState state=BlockShowState.Private)
            {
                block.ShowInTerminal = Convert.ToBoolean((Convert.ToInt32(state) & 1));
                block.ShowInInventory = Convert.ToBoolean((Convert.ToInt32(state) & 2));
                block.ShowInToolbarConfig = Convert.ToBoolean((Convert.ToInt32(state) & 4));
            }
            /// <summary>
            /// 初始化:ship接口，InfoPanelSystem,系统内所有方块
            /// </summary>
            /// <param name="ship"></param>
            public ShipSystem(MyShip ship)
            {
                this.ship = ship;
                InfoPanelSystem = new InfoPanelSystem(ship);
                InitializationBlock();
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
                Echo(debugContent);
                Me.CustomData += debugContent;
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
