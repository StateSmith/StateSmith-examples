If your design is a lot larger than what is shown here, you may need to make certain arrows longer so that PlantUML has space to layout the drawing. It always seems to want to make the smallest diagram possible.

# Consider Not Using Any Hints
PlantUML does a pretty good job if you just use `-->` for all arrows. It isn't quite as clean, but it is still very readable.

![](./larger-no-hints.png)

```plantuml
@startuml SolarUiSm

' //////////////////////// STATE ORGANIZATION ///////////////////////
' Note: StateSmith treats state names and events as case insensitive.
' More info: https://github.com/StateSmith/StateSmith/wiki/PlantUML

state SPLASH
state HOME

state MAIN_MENU {
    [*] -> MAIN_MENU_1
    state MAIN_MENU_1
    state MAIN_MENU_2
}

state SOLAR_STATS {
    [*] -> SOLAR_STATS_1
    state SOLAR_STATS_1
    state SOLAR_STATS_2
    state SOLAR_STATS_3
}

state BATTERY_STATS {
    [*] -> BATTERY_STATS_1
    state BATTERY_STATS_1
    state BATTERY_STATS_2
    state BATTERY_STATS_3
}

state SS1 {
    [*] -> SS1_VOLTAGE
    state SS1_VOLTAGE
    state SS1_CURRENT
    state SS1_POWER
}

[*] -> SPLASH

' ///////////////////////// STATE HANDLERS /////////////////////////
' Syntax: https://github.com/StateSmith/StateSmith/wiki/Behaviors

SPLASH --> HOME: RIGHT

HOME --> MAIN_MENU: RIGHT
MAIN_MENU --> HOME: LEFT

MAIN_MENU_1 --> MAIN_MENU_2: DOWN
MAIN_MENU_2 --> MAIN_MENU_1: UP

MAIN_MENU_1 --> SOLAR_STATS: RIGHT
SOLAR_STATS --> MAIN_MENU_1: LEFT

MAIN_MENU_2 --> BATTERY_STATS: RIGHT
BATTERY_STATS --> MAIN_MENU_2: LEFT

SOLAR_STATS_1 --> SOLAR_STATS_2: DOWN
SOLAR_STATS_2 --> SOLAR_STATS_3: DOWN
SOLAR_STATS_3 --> SOLAR_STATS_2: UP
SOLAR_STATS_2 --> SOLAR_STATS_1: UP

SOLAR_STATS_1 --> SS1: RIGHT
SS1 --> SOLAR_STATS_1: LEFT

SS1_VOLTAGE --> SS1_CURRENT: DOWN
SS1_CURRENT --> SS1_POWER: DOWN
SS1_POWER --> SS1_CURRENT: UP
SS1_CURRENT --> SS1_VOLTAGE: UP

BATTERY_STATS_1 --> BATTERY_STATS_2: DOWN
BATTERY_STATS_2 --> BATTERY_STATS_3: DOWN
BATTERY_STATS_3 --> BATTERY_STATS_2: UP
BATTERY_STATS_2 --> BATTERY_STATS_1: UP

SPLASH: enter / show_splash();
HOME: enter / show_home();
MAIN_MENU_1: enter / show_main_menu1();
MAIN_MENU_2: enter / show_main_menu2();
SOLAR_STATS_1: enter / show_solar_stats1();
SOLAR_STATS_2: enter / show_solar_stats2();
SOLAR_STATS_3: enter / show_solar_stats3();
BATTERY_STATS_1: enter / show_battery_stats1();
BATTERY_STATS_2: enter / show_battery_stats2();
BATTERY_STATS_3: enter / show_battery_stats3();
@enduml
```

# Before Making Arrows Longer
In the below diagram, we can see that the MAIN_MENU state needs to be taller to make for a cleaner diagram.

![](./pre.png)

```plantuml
@startuml SolarUiSm

' //////////////////////// STATE ORGANIZATION ///////////////////////
' Note: StateSmith treats state names and events as case insensitive.
' More info: https://github.com/StateSmith/StateSmith/wiki/PlantUML

state SPLASH
state HOME

state MAIN_MENU {
    [*] -> MAIN_MENU_1
    state MAIN_MENU_1
    state MAIN_MENU_2
}

state SOLAR_STATS {
    [*] -> SOLAR_STATS_1
    state SOLAR_STATS_1
    state SOLAR_STATS_2
    state SOLAR_STATS_3
}

state BATTERY_STATS {
    [*] -> BATTERY_STATS_1
    state BATTERY_STATS_1
    state BATTERY_STATS_2
    state BATTERY_STATS_3
}

state SS1 {
    [*] -> SS1_VOLTAGE
    state SS1_VOLTAGE
    state SS1_CURRENT
    state SS1_POWER
}

[*] -> SPLASH

' ///////////////////////// STATE HANDLERS /////////////////////////
' Syntax: https://github.com/StateSmith/StateSmith/wiki/Behaviors

SPLASH -right-> HOME: RIGHT

HOME -right-> MAIN_MENU: RIGHT
MAIN_MENU -left-> HOME: LEFT

MAIN_MENU_1 --> MAIN_MENU_2: DOWN
MAIN_MENU_2 --> MAIN_MENU_1: UP

MAIN_MENU_1 -right-> SOLAR_STATS: RIGHT
SOLAR_STATS -left-> MAIN_MENU_1: LEFT

MAIN_MENU_2 -right-> BATTERY_STATS: RIGHT
BATTERY_STATS -left-> MAIN_MENU_2: LEFT

SOLAR_STATS_1 --> SOLAR_STATS_2: DOWN
SOLAR_STATS_2 --> SOLAR_STATS_3: DOWN
SOLAR_STATS_3 --> SOLAR_STATS_2: UP
SOLAR_STATS_2 --> SOLAR_STATS_1: UP

SOLAR_STATS_1 -right-> SS1: RIGHT
SS1 -left-> SOLAR_STATS_1: LEFT

SS1_VOLTAGE --> SS1_CURRENT: DOWN
SS1_CURRENT --> SS1_POWER: DOWN
SS1_POWER --> SS1_CURRENT: UP
SS1_CURRENT --> SS1_VOLTAGE: UP

BATTERY_STATS_1 --> BATTERY_STATS_2: DOWN
BATTERY_STATS_2 --> BATTERY_STATS_3: DOWN
BATTERY_STATS_3 --> BATTERY_STATS_2: UP
BATTERY_STATS_2 --> BATTERY_STATS_1: UP

SPLASH: enter / show_splash();
HOME: enter / show_home();
MAIN_MENU_1: enter / show_main_menu1();
MAIN_MENU_2: enter / show_main_menu2();
SOLAR_STATS_1: enter / show_solar_stats1();
SOLAR_STATS_2: enter / show_solar_stats2();
SOLAR_STATS_3: enter / show_solar_stats3();
BATTERY_STATS_1: enter / show_battery_stats1();
BATTERY_STATS_2: enter / show_battery_stats2();
BATTERY_STATS_3: enter / show_battery_stats3();
@enduml
```

# After Making Arrows Longer

> NOTE!!!
> Not yet supported in StateSmith PlantUML grammar. See issue TODO.

Making the below arrows longer makes the MAIN_MENU state taller and allows for a cleaner diagram.

```diff
-MAIN_MENU_1 --> MAIN_MENU_2: DOWN
-MAIN_MENU_2 --> MAIN_MENU_1: UP

+MAIN_MENU_1 ----> MAIN_MENU_2: DOWN
+MAIN_MENU_2 ----> MAIN_MENU_1: UP
```

![](./larger.png)

```plantuml
@startuml SolarUiSm

' //////////////////////// STATE ORGANIZATION ///////////////////////
' Note: StateSmith treats state names and events as case insensitive.
' More info: https://github.com/StateSmith/StateSmith/wiki/PlantUML

state SPLASH
state HOME

state MAIN_MENU {
    [*] -> MAIN_MENU_1
    state MAIN_MENU_1
    state MAIN_MENU_2
}

state SOLAR_STATS {
    [*] -> SOLAR_STATS_1
    state SOLAR_STATS_1
    state SOLAR_STATS_2
    state SOLAR_STATS_3
}

state BATTERY_STATS {
    [*] -> BATTERY_STATS_1
    state BATTERY_STATS_1
    state BATTERY_STATS_2
    state BATTERY_STATS_3
}

state SS1 {
    [*] -> SS1_VOLTAGE
    state SS1_VOLTAGE
    state SS1_CURRENT
    state SS1_POWER
}

[*] -> SPLASH

' ///////////////////////// STATE HANDLERS /////////////////////////
' Syntax: https://github.com/StateSmith/StateSmith/wiki/Behaviors

SPLASH -right-> HOME: RIGHT

HOME -right-> MAIN_MENU: RIGHT
MAIN_MENU -left-> HOME: LEFT

MAIN_MENU_1 ----> MAIN_MENU_2: DOWN
MAIN_MENU_2 ----> MAIN_MENU_1: UP

MAIN_MENU_1 -right-> SOLAR_STATS: RIGHT
SOLAR_STATS -left-> MAIN_MENU_1: LEFT

MAIN_MENU_2 -right-> BATTERY_STATS: RIGHT
BATTERY_STATS -left-> MAIN_MENU_2: LEFT

SOLAR_STATS_1 --> SOLAR_STATS_2: DOWN
SOLAR_STATS_2 --> SOLAR_STATS_3: DOWN
SOLAR_STATS_3 --> SOLAR_STATS_2: UP
SOLAR_STATS_2 --> SOLAR_STATS_1: UP

SOLAR_STATS_1 -right-> SS1: RIGHT
SS1 -left-> SOLAR_STATS_1: LEFT

SS1_VOLTAGE --> SS1_CURRENT: DOWN
SS1_CURRENT --> SS1_POWER: DOWN
SS1_POWER --> SS1_CURRENT: UP
SS1_CURRENT --> SS1_VOLTAGE: UP

BATTERY_STATS_1 --> BATTERY_STATS_2: DOWN
BATTERY_STATS_2 --> BATTERY_STATS_3: DOWN
BATTERY_STATS_3 --> BATTERY_STATS_2: UP
BATTERY_STATS_2 --> BATTERY_STATS_1: UP

SPLASH: enter / show_splash();
HOME: enter / show_home();
MAIN_MENU_1: enter / show_main_menu1();
MAIN_MENU_2: enter / show_main_menu2();
SOLAR_STATS_1: enter / show_solar_stats1();
SOLAR_STATS_2: enter / show_solar_stats2();
SOLAR_STATS_3: enter / show_solar_stats3();
BATTERY_STATS_1: enter / show_battery_stats1();
BATTERY_STATS_2: enter / show_battery_stats2();
BATTERY_STATS_3: enter / show_battery_stats3();
@enduml
```