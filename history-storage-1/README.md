> Assumes that you have gone through basic StateSmith tutorials already.

This example shows how you can persist/restore a state machines state to/from some storage medium (database, file, ...).

To keep this example simple, we use a web browser's [local storage](https://developer.mozilla.org/en-US/docs/Web/API/Web_Storage_API). You can just open index.html in a browser.

![](docs/pizza.png)

# Things to notice
* Get to a `PIZZA_BUILD` substate (crust, size, toppings) and refresh the webpage. The state machine will resume where you left off.
* If you are in a `PURCHASING` substate, you can refresh the page and it will resume at the `REVIEW_ORDER` substate (it will intentionally **not** resume at `CONFIRM_ORDER`). This is an arbitrary design choice to show control options.
* Get to `ORDERED` state and refresh your browser. The state will resume at `ORDERED` state.
* There are two code generation scripts (basic and advanced). Read more about `code_gen_advanced.csx` below.

# How does this work?
Check out `index.js`. Essentially, the state machine's history field is set to the appropriate variable before the state machine is started.

This approach has a number of advantages over simply forcing the state machine into state X:
* The state machine behaves as expected (all ancestor state's entry handlers are called when resuming state X).
* The history behavior is clear in the diagram.
* The designer has more control over which states can and cannot be resumed.
* You have control over how to handle state machine design changes.


# Store State Name and Not Integer ID
In the generated code, you'll see an enumeration created for the states tracked by the History vertex:
```js
PizzaSm_HistoryId = 
{
    PIZZA_BUILD : 0, // default transition
    PURCHASING : 1,
    ORDERED : 2,
    CRUST : 3,
    TOPPINGS : 4,
    SIZE : 5,
}
```

These integer values may change during code generation runs or StateSmith versions. It's safer to store the name instead.

But what if someone changes a state name in the diagram like `SIZE` renamed to `PIZZA_SIZE`? See `code_gen_advanced.csx` for a solution. It detects changes, fails the code gen and prints a message like below:

```
Exception Exception : State machine root history changes: +PIZZA_SIZE, -SIZE,
```

# History State Info
* https://github.com/StateSmith/StateSmith/blob/main/docs/history-vertex.md

# Other Options
This isn't the only way to accomplish restoring a state machine from storage. You can also add custom state machine behavior from your code generation scripts.

# How to use
Open `index.html` in a browser.

# Run code gen
Run regular or advanced code gen with these commands in this directory:
```
dotnet-script code_gen.csx
dotnet-script code_gen_advanced.csx
```