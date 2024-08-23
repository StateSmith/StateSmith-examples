# Intro
A very nice and clean way to add a state machine into a larger "controller".

# NO EXPANSIONS NEEDED!
This is really nice. It feels more natural and is more developer friendly.

This pattern is inspired by hand coded state machines in C. Typically, a hand coded state machine in C includes a number of things beyond just the state machine itself. There is a public API, some utility functions, and then the hand code state machine at the bottom of the .c file.

This is what we are recreating here, but with a StateSmith generated state machine.

Essentially the user `#include`s the generated state machine at the bottom of their handwritten `.c` file.

NOTE!!! To avoid weird issues with a `.c` file being included inside of another `.c` file, we generate a `.inc` file instead.

# Why a `.inc` file?
Because it is a common convention to use `.inc` files for files that are included in other `.c` files. See https://stackoverflow.com/a/48474197/7331858 

It also makes it clear that this file is not meant to be compiled on its own and allows simple build rules like `gcc *.c` to work.

# Note for Arduino
Arduino IDE doesn't like `.inc` files. You can use `.inc.h` instead. See [here for details](https://github.com/StateSmith/StateSmith/issues/361).

# This might seem strange at first...
but take a look at [LightController.c](LightController.c). It's pretty straightforward.

This is essentially what we are doing:

![image](https://github.com/StateSmith/StateSmith-examples/assets/274012/a2d74059-e44a-415e-ba9c-6dcf98ce1160)

Technically it works like below. The C preprocessor is used to essentially bring the generated state machine into the user's `.c` file as if they had hand written the state machine.

![image](https://github.com/StateSmith/StateSmith-examples/assets/274012/8229cfab-8fa5-4702-bbf2-818c6b853dfa)

# If you don't like it
That's OK. Check out [README.drawio.svg](README.drawio.svg) for a comparison of different patterns.

Pattern works best when only a single instance of the state machine is needed, but can be adapted to multiple instances.




# Requirements
* If on Windows, use WSL/Cygwin/MinGW/etc.
* Assumes you have StateSmith cli `ss.cli` installed
* Assumes you have gcc installed

# Build & run
```sh
ss.cli run -h --lang C99
gcc *.c && ./a.out
```

# More details
See [c-include-sm example](../c-include-sm/README.md) for a more advanced example and additional details.
