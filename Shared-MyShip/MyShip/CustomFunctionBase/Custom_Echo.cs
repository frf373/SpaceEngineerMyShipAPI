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
            /// <summary>
            /// 定义好转发规则的Echo,并且有流功能和缓存功能
            /// </summary>
            protected class Custom_Echo
            {
                private readonly CustomFuncBase func;
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

                /// <summary>
                /// 内容构造器
                /// </summary>
                private StringBuilder contentBuilder;

                /// <summary>
                /// 向Echo流中写入信息
                /// </summary>
                /// <param name="content"></param>
                public void Echo_Builder(string content)
                {
                    //重置缓存计数器，并且代表有缓存内容
                    Echo_CacheCounter = 0;
                    contentBuilder.Append(content);
                }

                /// <summary>
                /// 是否有内容需要刷新出去
                /// </summary>
                public bool HasContent => contentBuilder.Length != 0;

                /// <summary>
                /// Echo内容缓存内容
                /// </summary>
                public string Echo_Cache {  get; set; }

                /// <summary>
                /// 是否有缓存信息
                /// </summary>
                public bool HasEcho_Cache => Echo_CacheCounter != -1;

                /// <summary>
                /// 刷新并输出Echo流
                /// </summary>
                /// <returns>要Echo的消息</returns>
                public void Echo_Flush()
                {
                    //定义转发规则，并且向缓存中输入
                    Echo_Cache = "[" + func.FuncName + "]" + "\n" + contentBuilder.ToString();
                    contentBuilder.Clear();
                }

                /// <summary>
                /// 重置缓存，比如计数器达到指定数字后，Echo不在需要这个信息，常用于某个信息停留在屏幕上一会
                /// </summary>
                public void Reset_Cache()
                {
                    //-1代表无缓存信息
                    Echo_CacheCounter = -1;
                }
            }
            
        }
    }
}
