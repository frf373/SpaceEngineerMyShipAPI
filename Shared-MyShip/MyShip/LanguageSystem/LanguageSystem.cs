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
        /// 语言系统
        /// </summary>
        public class LanguageSystem
        {
            /// <summary>
            /// 语言哈希表
            /// </summary>
            static private Hashtable entries=new Hashtable();

            /// <summary>
            /// 初始化，词条在这里添加
            /// </summary>
            static LanguageSystem()
            {
                //如何添加词条 How to add an word entry
                //例子：Example:
                //wordEntries.Add("其他语言", new string[] { "Other Languages","Autres langues","Andere Sprachen" });
                //                  中文                          English           Français          Deutsch
                //Enum Language{    Chinese,                      English,          French,           German        }
                //词条顺序必须和语言枚举顺序一样 The order of entries must be the same as that of language enumeration
                //如果没有你所要的语言 If you don't have the language you want
                //你可以在语言枚举中定义 You can define it in the language enumeration

                //entries.Add("",new string[] { });

                entries.Add("无法翻译，没有找到对应词条", new string[] { "Unable to translate, no corresponding entry found" });

                //飞船系统基类
                entries.Add("这是飞船系统基类, 无系统信息", new string[] { "This is the base class of spacecraft system.There is no system information"});

                //飞船电力系统
                entries.Add("电力系统信息面板", new string[] { "PowerSystemInfoPanel" });
                entries.Add("电池组信息", new string[] { "BatteryPackInfo"});
                entries.Add("电量储存",new string[] { "ElectricityStorage" });
                entries.Add("放电功率", new string[] { "DischargePower" });
                entries.Add("充电功率",new string[] {"ChargingPower"});

            }

            /// <summary>
            /// 翻译功能
            /// </summary>
            /// <param name="chineseContent">中文内容</param>
            /// <param name="targetLanguage">目标语言</param>
            /// <returns></returns>
            static public string Translate(string chineseContent,Language targetLanguage=Language.Chinese)
            {
                switch(targetLanguage)
                {
                    case Language.Chinese:return chineseContent;
                    case Language.English:
                        return entries[chineseContent] != null 
                            ? (entries[chineseContent] as string[])[(int)targetLanguage]
                            :(entries["无法翻译，没有找到对应词条"] as string[])[(int)targetLanguage];
                    default: return "<fail>";
                }
            }
        }
    }
}
