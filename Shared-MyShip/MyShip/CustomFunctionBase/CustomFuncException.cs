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
            /// 相当于throw new Exception 但会自动帮你加上功能名称和UID
            /// </summary>
            /// <param name="exceptionContent">例外内容</param>
            protected void ReportException(string exceptionContent)
            {
                throw new CustomFuncException(this, $"[FuncName:{FuncName}][UID:{UID}]Exception:" + exceptionContent);
            }

            /// <summary>
            /// 自定义功能自定义异常类
            /// </summary>
            public class CustomFuncException:Exception
            {
                /// <summary>
                /// 出现异常的功能类
                /// </summary>
                public CustomFuncBase func;

                /// <summary>
                /// 异常内容
                /// </summary>
                public string exceptionConent;

                /// <summary>
                /// 自定义功能自定义异常类构造函数
                /// </summary>
                /// <param name="func">出现异常的功能类</param>
                /// <param name="exceptionContent">异常内容</param>
                public CustomFuncException(CustomFuncBase func,string exceptionContent=""):base(exceptionContent)
                {
                    this.func = func;
                }
            }
        }
    }
}
