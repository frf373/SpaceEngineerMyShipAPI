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
            /// 自定义Runtime类
            /// </summary>
            public Custom_Runtime Runtime { get; protected set; }

            /// <summary>
            /// 重写的Runtime,必须和主程序隔开的类
            /// </summary>
            public class Custom_Runtime : IMyGridProgramRuntimeInfo
            {
                public Custom_Runtime(CustomFuncBase customFunc)
                {
                    this.customFunc = customFunc;
                    updateFrequency = UpdateFrequency.None;
                }
                private IMyGridProgramRuntimeInfo Runtime => customFunc.program.Runtime;

                private readonly CustomFuncBase customFunc;
                public TimeSpan TimeSinceLastRun => Runtime.TimeSinceLastRun;

                public double LastRunTimeMs => Runtime.LastRunTimeMs;

                public int MaxInstructionCount => Runtime.MaxInstructionCount;

                public int CurrentInstructionCount => Runtime.CurrentInstructionCount;

                public int MaxCallChainDepth => Runtime.MaxCallChainDepth;

                public int CurrentCallChainDepth => Runtime.CurrentCallChainDepth;

                /// <summary>
                /// 与主程序隔开的更新频率
                /// </summary>
                private UpdateFrequency updateFrequency;

                /// <summary>
                /// 其他地方暂时不用修改。这是关键！需要和主程序的更新频率隔开的地方！
                /// </summary>
                public UpdateFrequency UpdateFrequency
                {
                    get
                    {
                        return updateFrequency;
                    }
                    set
                    {
                        UpdateFrequency previous = updateFrequency;
                        //先变更后通知事件发生，防止卡死
                        updateFrequency = value;
                        //通知外界更新频率变了
                        OnUpdateFrequencyChanged?.Invoke(customFunc, new UpdateFrequencyChangedEventArgs(previous, value));
                    }
                }

                /// <summary>
                /// 当更新频率发生改变的事件
                /// </summary>
                public event UpdateFrequencyChangedHandler OnUpdateFrequencyChanged;

            }

            /// <summary>
            /// 更新频率改变事件参数
            /// </summary>
            public class UpdateFrequencyChangedEventArgs : EventArgs
            {
                /// <summary>
                /// 以前的频率
                /// </summary>
                public UpdateFrequency previous;

                /// <summary>
                /// 现在的频率
                /// </summary>
                public UpdateFrequency now;

                /// <summary>
                /// 频率改变事件参数构造函数
                /// </summary>
                /// <param name="previous">以前的频率</param>
                /// <param name="now">现在的频率</param>
                public UpdateFrequencyChangedEventArgs(UpdateFrequency previous, UpdateFrequency now)
                {
                    this.previous = previous;
                    this.now = now;
                }
            }

            /// <summary>
            /// 更新频率改变处理委托
            /// </summary>
            /// <param name="sender">哪个功能函数更新频率发生改变</param>
            /// <param name="e">改变后的参数</param>
            public delegate void UpdateFrequencyChangedHandler(object sender, UpdateFrequencyChangedEventArgs e);
        }
    }
}
