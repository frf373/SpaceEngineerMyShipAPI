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
    partial class Program : MyGridProgram
    {
        /*public int antiJamCount = 0;
        public readonly int antiJamNum = 1;
        public double autoToggleDistance = 3;
        
        public List<IMySensorBlock> sensorBlocks = new List<IMySensorBlock>();
        public List<DoorSensorSystem> doorSensorSystems = new List<DoorSensorSystem>();
        public List<IMyDoor> ungroupedDoors = new List<IMyDoor>();
        public List<IMyAirVent> airVents=new List<IMyAirVent>();
        public Hashtable IDtoDSSystem = new Hashtable();
        Action _CheckdDoor;
        public int checkDoorCount = 0;
        public Program()
        {

            Me.CustomData = "";
            GridTerminalSystem.GetBlocksOfType(sensorBlocks);
            GridTerminalSystem.GetBlocksOfType(ungroupedDoors, x => { string ID = x.BlockDefinition.SubtypeId; return ID == "" || ID.Contains("Offset") || ID.Contains("Slide"); });
            foreach (var sensor in sensorBlocks)
            {
                if (sensor.CustomData.Contains("[SensorType]:[DoorSensor]"))
                {
                    sensor.LeftExtend = sensor.MaxRange;
                    sensor.RightExtend = sensor.MaxRange;
                    sensor.TopExtend = sensor.MaxRange;
                    sensor.BottomExtend = sensor.MaxRange;
                    sensor.FrontExtend = sensor.MaxRange;
                    sensor.BackExtend = sensor.MaxRange;
                    
                    sensor.DetectAsteroids = false;
                    sensor.DetectFloatingObjects = false;
                    sensor.DetectSmallShips = false;
                    sensor.DetectLargeShips = false;
                    sensor.DetectStations = false;
                    sensor.DetectSubgrids = false;
                    sensor.DetectPlayers = true;

                    int index = sensor.CustomData.LastIndexOf("[");
                    int indexEnd = sensor.CustomData.LastIndexOf("]");

                    DoorSensorSystem sensorSystem = new DoorSensorSystem(sensor, ungroupedDoors, this);
                    doorSensorSystems.Add(sensorSystem);
                    IDtoDSSystem.Add(sensor.CustomData.Substring(index + 1, indexEnd - index - 1), sensorSystem);
                }
            }

            GridTerminalSystem.GetBlocksOfType(airVents);
            

            Runtime.UpdateFrequency = UpdateFrequency.Once;
        }

        public void Main(string argument, UpdateType updateSource)
        {
            if (updateSource == UpdateType.Trigger)
            {
                string[] args = argument.Split(Convert.ToChar(" "));
                if (args[0] == "DSF")
                {
                    _CheckdDoor += (IDtoDSSystem[args[1]] as DoorSensorSystem).CheckDoor;
                    checkDoorCount = checkDoorCount < 0 ? 0 : checkDoorCount;
                    if (checkDoorCount++ == 0)
                    {
                        Runtime.UpdateFrequency = UpdateFrequency.Update10;
                    }
                }
                else if (args[0] == "DSN")
                {
                    _CheckdDoor -= (IDtoDSSystem[args[1]] as DoorSensorSystem).CheckDoor;
                    checkDoorCount = checkDoorCount < 1 ? 1 : checkDoorCount;
                    if (--checkDoorCount == 0)
                    {
                        Runtime.UpdateFrequency = UpdateFrequency.None;
                    }
                    
                }
            }
            else if (updateSource == UpdateType.Update10)
            {
                if (antiJamCount != antiJamNum - 1)
                {
                    antiJamCount++;
                }
                else
                {
                    antiJamCount = 0;
                    _CheckdDoor();
                }
            }
            else if (updateSource == UpdateType.Once)
            {
                checkDoorCount = checkDoorCount < 0 ? 0 : checkDoorCount;
                foreach (var dss in doorSensorSystems)
                {
                    if (dss.sensor.LastDetectedEntity.Type == MyDetectedEntityType.CharacterHuman)
                    {
                        if (checkDoorCount++ == 0)
                        {
                            Runtime.UpdateFrequency = UpdateFrequency.Update10;
                        }
                        int index = dss.sensor.CustomData.LastIndexOf("[");
                        int indexEnd = dss.sensor.CustomData.LastIndexOf("]");
                        _CheckdDoor += (IDtoDSSystem[dss.sensor.CustomData.Substring(index + 1, indexEnd - index - 1)] as DoorSensorSystem).CheckDoor;
                    }
                }
            }
        }

        public class DoorAirlockSystem
        {
            public bool NeedDepress {  get; set; }

            public IMyDoor Door {  get; set; }

            public DoorAirlockSystem(bool needDepress,IMyDoor door)
            {
                this.NeedDepress = needDepress;
                this.Door = door;
            }
        }
        public class DoorSensorSystem
        {
            public IMySensorBlock sensor;
            public List<IMyDoor> doors = new List<IMyDoor>();
            Program program;
            /// <summary>
            /// 检查门是否需要改变状态
            /// </summary>
            public void CheckDoor()
            {
                Vector3D playerPos = sensor.LastDetectedEntity.Position;

                foreach (var door in doors)
                {
                    if ((door.GetPosition() - playerPos).Length() > program.autoToggleDistance)
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
            public DoorSensorSystem(IMySensorBlock sensor, List<IMyDoor> ungroupedDoors, Program program)
            {
                this.sensor = sensor;
                this.program = program;
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
        }*/
    }
}
