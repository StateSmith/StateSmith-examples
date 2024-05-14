# EXPERIMENTAL!

Stuff in here will change. This directory name will also likely change. Don't link to it just yet.

# Purpose
Shows a simple way to get all states and state behaviors for a design.

This information could be used to generate test plans, test scaffolding, or documentation.

Sample output:
```
Root initial state behaviors:
    TransitionTo(OFF)

State ON_GROUP behaviors:
    OFF TransitionTo(OFF)

State ON1 behaviors:
    enter / { light_blue(); }
    INCREASE TransitionTo(ON2)
    DIM TransitionTo(OFF)

State ON_HOT behaviors:
    enter / { light_red(); }
    DIM TransitionTo(ON2)

State ON2 behaviors:
    1. INCREASE / { count++; }
    2. INCREASE [count >= 3] TransitionTo(ON_HOT)
    enter / { light_yellow(); }
    enter / { count = 0; }
    DIM TransitionTo(ON1)

State OFF behaviors:
    enter / { std::cout << "OFF\n"; }
    INCREASE TransitionTo(ON1)
```
