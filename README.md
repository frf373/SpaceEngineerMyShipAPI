# SpaceEngineerMyShipAPI

如果你是一名太空工程师玩家：这个脚本会自动帮你搭建一个 **现代化智能飞船** ，并且包含各种功能

如果你是一名萌新脚本开发者：这个脚本会给你提供一个功能全面的MyShipAPI，供你使用，帮你开发

如果你是一名大佬脚本开发者：手里有好几个脚本。这个MyShipAPI，支持多脚本合并为一个脚本，不用担心命名等问题，并且是各项兼容且操作安全的（比如设置Runtime，使用CustomData数据，Echo输出等等）。只需 **几分钟** ， **修改几行代码**， 你就可以让自己多个脚本互相合并且兼容。

#### 总体介绍

游戏：太空工程师  内容：脚本开发和API开发

一款开源的脚本，支持： **直接使用** ， **脚本合并** ， **API二次开发** 

文件夹[Shared-Myship]是MyShipAPI

文件夹[MyModernShip]是我根据这个MyShipAPI搭建的完整script。可以直接运行。具体使用方法请看MyModernShip介绍

#### 安装教程

1.下载所有代码

2.使用visual studio 2019加载

3.开始你的太空编程师之旅吧

#### 说明

1.[Shared-Myship]是一个SharedProject，新建项目可以直接引用这个项目

2.[MyModernShip]是一个利用MyShipAPI搭建的脚本原型，里面附带了一些功能

#### MyShipAPI介绍

1.只需几分钟，就可以合并自己开发的各个脚本，不用担心命名，Runtime等问题

2.统一的功能基类CustomFuncBase，包含各种脚本运行原型等，支持自开发

3.多功能管理器CustomFuncManager，加载删除功能，功能运行权限管理功能等

4.统一的系统基类ShipSystem，支持自开发

5.飞船系统管理ShipSystemCollection

6.完整的飞船终端方块获取，终端块包含在各个飞船系统中，如BatteryBlock在ElectricSystem中，SensorBlock在DetectionSystem中

具体使用方法请见WIKI

#### MyModernShip介绍

搭载功能：自动开关门

具体使用方法请见WIKI

#### 版本历史

小版本号代表功能优化，体验修复（如1.1.5和1.1.6），大版本号代表功能增加（如1.0和1.1和1.2），主体版本号代表构架改写（如1.0和2.0）

////////////////////////////////////////

[Version:1.0]版本新增

[MyModernShip] 

1.新增自动开关门功能

[MyShipAPI] 

1.新增飞船系统集合，此系统能获得飞船上所有终端块

2.新增飞船功能系统集合，此集合可以快速兼容所有自研脚本，并且多功能Runtime兼容的

3.新增飞船功能基类，开发使用

4.新增飞船功能原型

5.新增功能Runtime改写类


