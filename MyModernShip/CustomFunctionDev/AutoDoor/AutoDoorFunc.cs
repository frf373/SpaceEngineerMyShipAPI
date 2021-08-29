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
        /// 自动门禁和气阀功能,包括按钮门
        /// </summary>
        public partial class AutoDoorFunc:CustomFuncBase
        {   //airVents

            private List<IMyAirVent> AirVents=>ShipSystems.GasSystem.AirVents;

            public AutoDoorFunc(Program program,MyShip ship,string funcName,string uid):base(program,ship,funcName,uid)
            {
                AutoToggleDistance = 3;
                //ShipSystems.GasSystem.AirVents.First().
                Runtime.UpdateFrequency = UpdateFrequency.Once;
            }

            public void SetDoorsStatus()
            {
                 
            }

            public double AutoToggleDistance { get; set; }
            

            /// <summary>
            /// 门禁系统中气阀参数
            /// </summary>
            public class DoorAirVentArgs
            {
                /// <summary>
                /// 气阀
                /// </summary>
                public IMyAirVent airVent;

                /// <summary>
                /// 当门打开时，气阀需要执行的命令枚举
                /// </summary>
                public AirVentActionArg actionArg;

                /// <summary>
                /// 构造函数
                /// </summary>
                /// <param name="airVent">气阀</param>
                /// <param name="arg">当门打开时，气阀需要执行的命令枚举</param>
                public DoorAirVentArgs(IMyAirVent airVent,AirVentActionArg actionArg)
                {
                    this.airVent = airVent;
                    this.actionArg = actionArg;
                }
            }

            /// <summary>
            /// 当门打开时，气阀需要执行的命令枚举
            /// </summary>
            public enum AirVentActionArg
            {
                /// <summary>
                /// 门打开，不影响漏气
                /// </summary>
                None=0,
                /// <summary>
                /// 门打开前需要减压
                /// </summary>
                NeedDepress=1,
                /// <summary>
                /// 这是外部气阀
                /// </summary>
                Outside=2
            }
        }
    }
}
