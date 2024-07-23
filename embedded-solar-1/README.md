> Assumes that you have gone through basic StateSmith tutorials already.

# Menu UI Layout
```
STATUS: charging
BATTERY: 80%

MAIN MENU 1/2
> solar stats

	SOLAR STATS 1/3
	voltage: 14.5

	SOLAR STATS 2/3
	amperage: 1.3

	SOLAR STATS 3/3
	time: 3h 16min

MAIN MENU 2/2
> battery stats

	BATTERY STAT 1/3
	voltage: 13.1

	BATTERY STAT 2/3
	amperage: -1.3

	BATTERY STAT 3/3
	amp hours: 35.1
```

# Finished Design
![](docs/finished-design.png)


# Simulation
https://wokwi.com/projects/404121058111410177


# Taking It Further
* create a few idle screens instead of "HOME" state
* have the idle screens cycle between each other every 5 seconds
* have main menu timeout to idle screen after 30 seconds
