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
        /// 飞船系统集合
        /// </summary>
        public class ShipSystemCollection
        {

            private MyShip ship;

            public ShipSystemCollection(MyShip ship)
            {
                this.ship = ship;
                //重新获得所有系统
                Reboot();
                
            }
            /// <summary>
            /// 辅助系统
            /// </summary>
            public AssistanceSystem AssistanceSystem { get; set; }

            /// <summary>
            /// 通讯系统
            /// </summary>
            public CommunicationSystem CommunicationSystem { get; set; }

            /// <summary>
            /// 连接系统
            /// </summary>
            public ConnectionSystem ConnectionSystem { get; set; }

            /// <summary>
            /// 控制系统
            /// </summary>
            public ControlSystem ControlSystem { get; set; }

            /// <summary>
            /// 探测系统
            /// </summary>
            public DetectionSystem DetectionSystem { get; set; }

            /// <summary>
            /// 门禁系统
            /// </summary>
            public DoorSystem DoorSystem { get; set; }

            /// <summary>
            /// 动力系统
            /// </summary>
            public DynamicSystem DynamicSystem { get; set; }

            /// <summary>
            /// 电力系统
            /// </summary>
            public ElectricSystem ElectricSystem { get; set; }

            /// <summary>
            /// 气体系统
            /// </summary>
            public GasSystem GasSystem { get; set; }

            /// <summary>
            /// 引力系统
            /// </summary>
            public GravitationalSystem GravitationalSystem { get; set; }

            /// <summary>
            /// 降落系统
            /// </summary>
            public LandingSystem LandingSystem { get; set; }

            /// <summary>
            /// 屏幕系统
            /// </summary>
            public LCDSystem LCDSystem { get; set; }

            /// <summary>
            /// 生存系统
            /// </summary>
            public LifeSupportSystem LifeSupportSystem { get; set; }

            /// <summary>
            /// 灯光系统
            /// </summary>
            public LightSystem LightSystem { get; set; }

            /// <summary>
            /// 机械系统
            /// </summary>
            public MechanicalSystem MechanicalSystem { get; set; }

            /// <summary>
            /// 生产系统
            /// </summary>
            public ProductionSystem ProductionSystem { get; set; }

            /// <summary>
            /// 仓储系统
            /// </summary>
            public StorageSystem StorageSystem { get; set; }

            /// <summary>
            /// 工具系统
            /// </summary>
            public ToolSystem ToolSystem { get; set; }

            /// <summary>
            /// 交易系统
            /// </summary>
            public TradeSystem TradeSystem { get; set; }

            /// <summary>
            /// 武器系统
            /// </summary>
            public WeaponSystem WeaponSystem { get; set; }

            /// <summary>
            /// 重新加载飞船系统
            /// </summary>
            /// <param name="arg">要加载的系统枚举参数，支持位运算</param>
            public void Reboot(RebootArg arg = RebootArg.All)
            {
                if (Convert.ToBoolean(arg & RebootArg.Assistance)) AssistanceSystem = new AssistanceSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Communication)) CommunicationSystem = new CommunicationSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Connection)) ConnectionSystem = new ConnectionSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Control)) ControlSystem = new ControlSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Detection)) DetectionSystem = new DetectionSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Door)) DoorSystem = new DoorSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Dynamic)) DynamicSystem = new DynamicSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Electric)) ElectricSystem = new ElectricSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Gas)) GasSystem = new GasSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Gravitational)) GravitationalSystem = new GravitationalSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Landing)) LandingSystem = new LandingSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.LCD)) LCDSystem = new LCDSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.LifeSupport)) LifeSupportSystem = new LifeSupportSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Light)) LightSystem = new LightSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Mechanical)) MechanicalSystem = new MechanicalSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Production)) ProductionSystem = new ProductionSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Storage)) StorageSystem = new StorageSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Tool)) ToolSystem = new ToolSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Trade)) TradeSystem = new TradeSystem(ship);
                if (Convert.ToBoolean(arg & RebootArg.Weapon)) WeaponSystem = new WeaponSystem(ship);
            }
        }
    }
}
