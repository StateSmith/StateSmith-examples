# Assumptions
Assumes that you have gone through basic StateSmith tutorials already.

# Intro
This example outputs a [MySm.md](./MySm.md) file that can be useful for:
* git/svn diffs
* inspecting hierarchical designs
* understanding StateSmith transformations

Lots more detail here: https://github.com/StateSmith/StateSmith/issues/200

# How To Use
The generated code isn't meant to be compiled. We are just here to see the output .md file.

Play with the diagram, the .csx settings, then run code gen.

# Other Notable Stuff
Look inside `RUNNING` state. It helps explain a potential problem with "transition spamming" and safety checks.
