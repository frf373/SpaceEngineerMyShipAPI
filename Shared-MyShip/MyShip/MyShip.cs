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
        /// 飞船类
        /// </summary>
        public class MyShip
        {
            /// <summary>
            /// 自身网格
            /// </summary>
            public IMyCubeGrid CubeGrid => Me.CubeGrid;

            /// <summary>
            /// 网格终端系统
            /// </summary>
            public IMyGridTerminalSystem GridTerminalSystem=>Program.GridTerminalSystem;

            /// <summary>
            /// Echo
            /// </summary>
            public Action<string> Echo => Program.Echo;

            /// <summary>
            /// 本程序块
            /// </summary>
            public IMyProgrammableBlock Me => Program.Me;

            /// <summary>
            /// 程序接口
            /// </summary>
            public Program Program {  get; set; }

            /// <summary>
            /// 是否是静态网格
            /// </summary>
            public bool IsStatic => CubeGrid.IsStatic;

            /// <summary>
            /// 网格大小
            /// </summary>
            public MyCubeSize GridSize => CubeGrid.GridSizeEnum;

            /// <summary>
            /// 是否是大网格，判断依据是这个编程块是否是大方块
            /// </summary>
            public bool IsLargeGrid => GridSize == MyCubeSize.Large;

            /// <summary>
            /// 飞船网格名字
            /// </summary>
            public string ShipCustomName
            {
                get
                {
                    return CubeGrid.CustomName;
                }
                set
                {
                    CubeGrid.CustomName = value;
                }
            }

            /// <summary>
            /// 飞船功能集合
            /// </summary>
            public CustomFuncManager CustomFuncs {  get; set; }

            /// <summary>
            /// 飞船系统集合
            /// </summary>
            public ShipSystemCollection ShipSystems { get; set; }

            /// <summary>
            /// 飞船的构造函数
            /// </summary>
            /// <param name="program">需要一个program类，如果从Program中调用，一般来说用参数this就行了</param>
            public MyShip(Program program)
            {
                this.Program = program;
                this.Language = Language.Chinese;

                ShipSystems = new ShipSystemCollection(this);
                CustomFuncs = new CustomFuncManager(this);

                CycleCount = 0;
            }

            /// <summary>
            /// 对某一个函数，执行有参数的运行
            /// </summary>
            /// <param name="arg">带有转发标识符的参数,(16位字符串ID)+(#+-*/#)+（真正参数），如:ExampleFunctionH#+-*/#ShowExample</param>
            /// <param name="source">更新源</param>
            public void RunArgFunc(string arg,UpdateType source)
            {
                CustomFuncs.HandleArg(arg, source);
            }

            /// <summary>
            /// 循环次数，用于比如10次Update10=Update100
            /// </summary>
            private int CycleCount {  get; set; }

            /// <summary>
            /// 运行所有功能循环，并且更新功能的更新频率
            /// </summary>
            public void RunAllCircleFunc()
            {
                CustomFuncs.RunOnceFunc();

                CustomFuncs.RunCycle1Func();

                //10次1tick等于10tick
                if(CycleCount%10==0)
                {
                    CustomFuncs.RunCycle10Func();
                }

                //100次1tick等于100tick
                if(CycleCount%100==0)
                {
                    //重置计数
                    CycleCount = 0;
                    CustomFuncs.RunCycle100Func();
                }
                CycleCount++;

                CustomFuncs.UpdateFuncFrequency();
            }

            /// <summary>
            /// 运行保存功能
            /// </summary>
            public void RunSave()
            {
                CustomFuncs.RunSaveFunc();
            }

            /// <summary>
            /// 运行总是要运行的功能，比如计数函数，日志函数
            /// </summary>
            /// <param name="arg"></param>
            /// <param name="source"></param>
            public void RunAlwaysFunc(string arg,UpdateType source)
            {
                
            }

            /// <summary>
            /// 飞船系统语言
            /// </summary>
            public Language Language {  get; set; }

            /// <summary>
            /// 飞船语言翻译功能
            /// </summary>
            public string Translate(string chineseContent)
            {
                return LanguageSystem.Translate(chineseContent,Language);
            }

            /// <summary>
            /// 充电
            /// </summary>
            public void Recharge()
            {

            }

            /// <summary>
            /// 充气
            /// </summary>
            public void Refill()
            {

            }
        }
    }
}
