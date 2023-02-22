# Assumptions
Assumes that you have gone through the [example-drawio-1](https://github.com/StateSmith/example-drawio-1) walk through already.


# Custom Triggers
This example shows how you can write user code to support custom triggers.

## Shorthands
The first user extension is simple: it just allows you to write `en` instead of `entry` and `ex` instead of `exit`.

## Aggregate Events and Triggers
The second part of the user script adds some more advanced features:

* `$any_t` expands to all triggers (enter, exit and all events used in state machine).
* `$any_e` expands to all events used in state machine.

![](./src/MySm.drawio.svg)
