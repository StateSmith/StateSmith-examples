# A simpler example
Is available in [c-include-sm-basic-1](../c-include-sm-basic-1/README.md). Maybe start there first.

# Works For C/C++
Utilizes the preprocessor instead of StateSmith features.

This example shows an fsm directly `#included` into a user written c/cpp file. To avoid compilation concerns, when we generate the state machine ".c" file, we rename it to a ".inc" file instead. So the generated files are:
* `LightSm.h` 
* `LightSm.inc`

Why `.inc`? Somewhat common to mean "code fragment" and not a normal header. See https://stackoverflow.com/a/48474197/7331858 

# Benefits
* fsm can access private variables/functions inside a user c/cpp file.
* no additional wiring or data structures need to be created by user.
* no expansions are needed at all.
* user private variables/functions remain private.
    - they don't need to be put into a header for fsm to access.
    - private var/function pointers don't need to be put into struct for fsm to access.

# The Design
The actual user state machine diagram and user code is just a trivial example. The key point is that the state machine is directly included into the user code and how they can interact.

# Single Or Multi Instance
This particular example assumes you only have a single instance of the target fsm,
but the same approach can be applied to multiple instances of a fsm (like for multiple buttons).

# Bare .CSX File
Note that the .csx file is almost empty (no render config, no expansions).

```cs
using StateSmith.Runner;
SmRunner runner = new(diagramPath: "LightSm.drawio", transpilerId: TranspilerId.C99);
runner.Run();
```

* This gets us closer to not needing to use .csx files. 
* .csx files allow powerful customization, but many fsm designs don't need that power.
* It would be nice to be simpler.
