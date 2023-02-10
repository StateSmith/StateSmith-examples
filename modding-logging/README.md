# Intro
Open this directory with `vscode`.

This example shows how to use a custom user script to modify the state machine before code generation runs to add custom logging.

This is achieved with a few modding behavior lines in the diagram and [MySm.csx](./src/MySm.csx)
* `$mod / log` - logs state name on enter and exit.
* `$mod / log_r` - same as above, but it also acts recursively on all nested states.

![](./src/MySm.drawio.svg)

# How Does This Work?
https://github.com/StateSmith/StateSmith/wiki/How-StateSmith-Works

# Experimental API
This is an experimental feature. User modding scripts will supported going forward,
but some of the internal StateSmith API that the modding scripts access may change.

