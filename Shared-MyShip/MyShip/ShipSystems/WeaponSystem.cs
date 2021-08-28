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
        /// 武器系统
        /// </summary>
        public class WeaponSystem : ShipSystem
        {
            /// <summary>
            /// 诱饵
            /// </summary>
            public List<IMyDecoy> Decoys {  get; set; }

            /// <summary>
            /// 弹头
            /// </summary>
            public List<IMyWarhead> Warheads { get; set; }

            /// <summary>
            /// 加特林防御塔，包括大小
            /// </summary>
            public List<IMyLargeGatlingTurret> GatlingTurrets { get; set; }

            /// <summary>
            /// 导弹防御塔，包括大小
            /// </summary>
            public List<IMyLargeMissileTurret> MissileTurrets { get; set; }

            /// <summary>
            /// 室内机枪
            /// </summary>
            public List<IMyLargeInteriorTurret> InteriorTurrets { get; set; }

            /// <summary>
            /// 加特林机枪
            /// </summary>
            public List<IMySmallGatlingGun> GatlingGuns { get; set; }

            /// <summary>
            /// 导弹发射器，包括大小,可装填
            /// </summary>
            public List<IMySmallMissileLauncher> MissileLaunchers { get; set; }

            public WeaponSystem(MyShip ship):base(ship)
            {

            }

            public override void InitializationBlock()
            {
                Decoys = new List<IMyDecoy>();
                Warheads = new List<IMyWarhead>();

                GatlingTurrets = new List<IMyLargeGatlingTurret>();
                MissileTurrets = new List<IMyLargeMissileTurret>();
                InteriorTurrets = new List<IMyLargeInteriorTurret>();

                GatlingGuns = new List<IMySmallGatlingGun>();
                MissileLaunchers=new List<IMySmallMissileLauncher>();

                GridTerminalSystem.GetBlocksOfType(Decoys);
                GridTerminalSystem.GetBlocksOfType(Warheads);

                GridTerminalSystem.GetBlocksOfType(GatlingTurrets);
                GridTerminalSystem.GetBlocksOfType(MissileTurrets);
                GridTerminalSystem.GetBlocksOfType(InteriorTurrets);
                GridTerminalSystem.GetBlocksOfType(GatlingGuns);
                GridTerminalSystem.GetBlocksOfType(MissileLaunchers);
            }
        }
    }
}
