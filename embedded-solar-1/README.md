# Prerequisites
Assumes that you have gone through StateSmith [tutorial 3 already](https://github.com/StateSmith/tutorial-3).

# Intro
The main purpose of this project & tutorial is to help you understand PlantUML layout options.

It will also guide you through the process of easily creating a simple nested/hierarchical menu state machine. This isn't necessarily the best way to do it, but it's a nice and simple starting point. More menu designs will be covered in future tutorials.

Although the simulation uses C++/Arduino, the same concepts can be applied to any language.

![](docs/sim.png)

# Finished Design
With just a bit of additional hints provided to PlantUML, we can get it to create a beautiful diagram.

Without the type hints, it can still do a decent job ([see here](./docs/alternate.md)).

> NOTE! You should generally avoid using type hints unless you need to. It's often better to let PlantUML figure it out on its own. Just use `-->` for transitions and see how it looks.

![](docs/finished-design.png)

# Tutorial
Follow along at [tutorial.md](./tutorial.md).

# Simulation
https://wokwi.com/projects/404121058111410177

# Taking It Further
This just shows the basics. There are many things we could do to take it further.

* create a few idle screens instead of "HOME" state
* have the idle screens cycle between each other every 5 seconds
* have main menu timeout to idle screen after 30 seconds

I'll cover more advanced menu designs in the future. When you start wanting to edit settings from a nested menu, it often makes sense to have a separate "Editor" state machine that works along side the main UI state machine.


<br>

# Auto Generating PlantUML Code
I'm tinkering with idea of a small online tool that will take a simple text format like this:

```
MAIN_MENU
	SOLAR_STATS
		SOLAR_STATS_1
		SOLAR_STATS_2
		SOLAR_STATS_3
	BATTERY_STATS
		BATTERY_STATS_1
		BATTERY_STATS_2
		BATTERY_STATS_3
```

and then generate the majority of the PlantUML code we need.
