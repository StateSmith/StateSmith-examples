> Assumes that you have gone through basic StateSmith tutorials already.

This example stores the history state in a web browser's local storage (you don't need to run a local server).

# Things to notice
* Get to a `PIZZA_BUILD` substate (crust, size, toppings) and refresh the webpage. The state machine will resume where you left off.
* If you are in a `PURCHASING` substate, you can refresh the page and it will resume at the `REVIEW_ORDER` substate (it will intentionally **not** resume at `CONFIRM_ORDER`). This is an arbitrary design choice to show control options.
* Get to `ORDERED` state and refresh. The state will resume at `ORDERED` state.

# History State Info
* https://github.com/StateSmith/StateSmith/blob/main/docs/history-vertex.md

# Other Options
This isn't the only way to accomplish restoring a state machine from storage. You can also add custom state machine behavior from your code generation scripts.

# How to use
Open `index.html` in a browser.

# Run code gen
Run command `dotnet-script code_gen.csx` in this directory.

