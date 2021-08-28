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
            public class Custom_Echo
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
                private int Echo_CacheCounter { get; set; }

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
                private bool HasContent => contentBuilder.Length != 0;

                /// <summary>
                /// Echo内容缓存内容
                /// </summary>
                private string Echo_Cache {  get; set; }

                /// <summary>
                /// 输出Echo缓存并刷新缓存计数器
                /// </summary>
                private void Echo_FlushToCache()
                {
                    //定义转发规则，并且向缓存中输入
                    Echo_Cache = "[" + func.FuncName + "]" + "\n" + contentBuilder.ToString();
                    if(!Echo_Cache.EndsWith("\n"))Echo_Cache+="\n";
                    Echo_CacheCounter = 0;
                    contentBuilder.Clear();
                }

                /// <summary>
                /// 根据刷新计数器数字，获得Echo缓存
                /// </summary>
                /// <returns>Echo缓存</returns>
                private string GetEcho_Cache()
                {
                    if (Echo_CacheCounter>0&&Echo_CacheCounter<50)
                    {
                        Echo_CacheCounter++;
                    }
                    else
                    {
                        Echo_CacheCounter = -1;
                        Echo_Cache = "";
                    }
                    return Echo_Cache;
                }

                /// <summary>
                /// 得到此功能的Echo全功能，并且加上了函数标识符
                /// </summary>
                /// <returns></returns>
                public string GetContent()
                {
                    if (HasContent) Echo_FlushToCache();
                    return GetEcho_Cache();
                }
            }
            
        }
    }
}
