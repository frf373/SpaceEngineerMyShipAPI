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
        /// 自动门禁和气阀功能
        /// </summary>
        public class AutoDoorFunc:CustomFuncBase
        {   //airVents

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
            public class DoorSensorSystem
            {
                public IMySensorBlock sensor;
                public List<IMyDoor> doors = new List<IMyDoor>();
                AutoDoorFunc func;
                /// <summary>
                /// 检查门是否需要改变状态
                /// </summary>
                public void CheckDoor()
                {
                    Vector3D playerPos = sensor.LastDetectedEntity.Position;

                    foreach (var door in doors)
                    {
                        if ((door.GetPosition() - playerPos).Length() > func.AutoToggleDistance)
                        {
                            if (door.Status != DoorStatus.Closed)
                            {
                                door.CloseDoor();
                            }
                        }
                        else
                        {
                            if (door.Status != DoorStatus.Open)
                            {
                                door.OpenDoor();
                            }
                        }
                    }
                }
                public DoorSensorSystem(IMySensorBlock sensor, List<IMyDoor> ungroupedDoors, AutoDoorFunc func)
                {
                    this.sensor = sensor;
                    this.func = func;
                    Vector3D sensorPosition = this.sensor.GetPosition();
                    foreach (var door in ungroupedDoors)
                    {
                        if ((door.GetPosition() - sensorPosition).Length() < 50)
                        {
                            doors.Add(door);
                        }
                    }
                    foreach (var door in doors)
                    {
                        //删除门，为了保证一个门只能被一个探测器控制
                        //不可以在上一个循环中移除，不然会造成枚举器出问题
                        ungroupedDoors.Remove(door);
                    }
                }
            }

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
