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
            /// <summary>
            /// 删除自定义数据，相当于block.CustomData-""
            /// </summary>
            /// <param name="block">要删除自定义数据的方块</param>
            protected void Remove_CustomData(IMyTerminalBlock block)
            {
                Set_CustomData(block, "");
            }

            /// <summary>
            /// 增加自定义数据，相当于block.CustomData+=data
            /// </summary>
            /// <param name="block">要加的自定义数据的方块</param>
            /// <param name="data">自定义数据内容</param>
            protected void Add_CustomData(IMyTerminalBlock block, string data)
            {
                string temp = Get_CustomData(block);

                Set_CustomData(block, temp + data);
            }

            /// <summary>
            /// 设置自定义数据,相当于block.CustomData=data
            /// </summary>
            /// <param name="block">要设置自定义数据的方块</param>
            /// <param name="data">自定义数据内容</param>
            protected void Set_CustomData(IMyTerminalBlock block, string data)
            {
                if (data.Contains(custom_Separator_End))
                {
                    throw new Exception("你的传递字符中有专用分隔符");
                }
                else
                {
                    block.CustomData = SetbySplit(block.CustomData, data);
                }
            }

            /// <summary>
            /// 获得自定义数据,相当于=block.CustomData
            /// </summary>
            /// <param name="block">要获得自定义数据的方块</param>
            /// <returns>自定义数据</returns>
            protected string Get_CustomData(IMyTerminalBlock block)
            {
                return GetbySplit(block.CustomData);
            }
        }
    }
}
