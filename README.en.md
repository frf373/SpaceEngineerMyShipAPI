# SpaceEngineerMyShipAPI

If you are a space engineer player: this script will automatically help you build a modern intelligent spacecraft, and contains various functions

If you are a Mengxin script developer: this script will provide you with a fully functional myship API for you to use and help you develop

If you are a big script developer: you have several scripts in your hand. This myship API supports merging multiple scripts into one script without worrying about naming. It is compatible and safe to operate (such as setting the runtime, using customdata data, echo output, etc.). In just a few minutes and a few lines of code, you can merge multiple scripts and make them compatible with each other.

#### General introduction

Game: space engineer content: script development and API development

An open source script, support: direct use, script merging, API secondary development

The folder [shared myship] is a myshipapi

The folder [mymodernship] is a complete script I built according to this myshipapi. Can run directly. Please refer to the introduction of mymodernship for specific usage

#### Installation tutorial

1. Download all codes

2. Use visual studio to load

3. Start your journey as a space programmer

#### explain

1. [shared myship] is a sharedproject, which can be directly referenced by a new project

2. [mymodernship] is a script prototype built using myship API, with some functions attached

#### Introduction to myshipapi

1. You can merge your own scripts in a few minutes without worrying about naming, runtime, etc

2. The unified function base class customfuncbase includes various script running prototypes and supports self-development

3. The multi-function manager, customfuncmanager, loads and deletes functions, runs permissions management functions, etc

4. The unified system base class shipsystem supports self-development

5. Ship system collection

6. Obtain the complete spacecraft terminal block. The terminal block is included in each spacecraft system, such as batteryblock in the electric system and sensorblock in the detection system

See wiki for specific usage

#### Introduction to mymodernship

Carrying function: automatic door opening and closing

See wiki for specific usage

#### Version history

The small version number represents function optimization and experience repair (such as 1.1.5 and 1.1.6), the large version number represents function increase (such as 1.0 and 1.1 and 1.2), and the main version number represents architecture rewriting (such as 1.0 and 2.0)

////////////////////////////////////////

[version: 1.1] new

[MyShipAPI]

1.Set_ CustomData,Get_ CustomData,Add_ Customdata specifies operations on custom data. Accurately extract the content related to your own function. A block of custom data stores the data of various functions

2.Custom_ Echo class, so that multiple functions can echo at the same time and separated by function identifiers. It has the function of caching echo content, such as staying in 50tick. In 50tick, the content will be displayed

////////////////////////////////////////

[version: 1.0] New

[MyModernShip]

1. Add automatic door opening and closing function

[MyShipAPI]

1. Add a new spacecraft system set, which can obtain all terminal blocks on the spacecraft

2. Add a collection of spacecraft functional systems, which can be quickly compatible with all self-developed scripts and multi-functional runtime compatible

3. Add the basic class of spacecraft function for development and use

4. New spacecraft function prototype

5. New function: runtime rewrite class