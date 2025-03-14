# Hello!
Make sure you've already gone [through a tutorial](https://github.com/StateSmith/StateSmith/wiki/Learning-Resources) already as these example projects aren't tutorials. They generally highlight a few interesting features.

## New vs Old
Most of these examples use the older (but still good) .csx method of running StateSmith. You can search for `ss.cli` for newer examples. More will come.

You can generally learn lots from these older examples even if you aren't setup to run the .csx files. There's good stuff in the diagram files and also how the generated files are connected to user code.

Not all projects are listed below. Feel free to explore this directory. A number of the projects in here are straight up experimental. See each project's readme.

## How to Use .csx Files?
In 2024, I only recommend using .csx files if you need very advanced features of StateSmith like custom modding the state machine graph just before code gen.

See [tutorial 2](https://github.com/StateSmith/tutorial-2/)


<br>

# StateSmith
## 🌟 Features
* [trigger-map](./trigger-map/README.md) - short hands for events/triggers.
* [parent-alias-1](./parent-alias-1/README.md) - useful strategy for commanding a specific state. `ss.cli`
* [multiple state machines in a single diagram](./buttons-lights-1/README.md) - connect state machines together through variables & events.
* [sm-design-describer-1](./sm-design-describer-1/README.md) good for git diffs, understanding

## ✨ Advanced Features (require .csx)
Not recommended for beginners.
* [multi-lang-1](./multi-lang-1/README.md). Generate state machines for multiple programming languages from a single diagram.
* [modding-logging](./modding-logging/README.md)
* [custom-triggers](./custom-triggers/README.md)
* [user-post-process](./user-post-process/README.md)
* [visitor-graph-1](./visitor-graph-1/README.md). Analyze a state machine graph using the [Visitor Pattern](https://en.wikipedia.org/wiki/Visitor_pattern).

<br>

# By Topic

## 📝 Logging
* [logging-simple-1](./logging-simple-1/README.md). Basic user runtime logging.
* [logging-simple-script-2](./logging-simple-script-2/README.md). Easy logging added at code generation phase.
* [modding-logging](./modding-logging/README.md). Advanced code gen logging visible in diagram.

## 🔀 Concurrency
* [rtos-event-queue-c](./rtos-event-queue-c/README.md)
* [csharp-space-menu-1](csharp-space-menu-1/README.md) - Advanced menu system with C#. Concurrency. Deep nesting, entry/exit points, parent alias and more.

## 👾 Video Game
* [mario-sm-3](./mario-sm-3/README.md) - Adds invincible star state, history, thwomp. Uses new 0.12.0+ lib features. `ss.cli` `drawio` `new`
* [mario-sm-2](./mario-sm-2/README.md) - Adds invincible star state and history. `ss.cli` `plantuml`
* [mario-sm-1](./mario-sm-1/README.md)

## 🗄️ Web/Database
* [history-storage-1](./history-storage-1/README.md) Resumes a state machine from a clean start and some storage (file/database/...).

## 📱 Menu / User Interface
* [embedded-solar-1](./embedded-solar-1/README.md) - Shows how to make a nested menu state machine with PlantUML. `ss.cli`
* [csharp-space-menu-1](csharp-space-menu-1/README.md) - Advanced menu system with C#. Concurrency. Deep nesting, entry/exit points, parent alias and more.
* todo - migrate [laser tag example](https://www.youtube.com/watch?v=9czSDothuzM) [simulation](https://wokwi.com/projects/351165738904453719).

## 🕹️ Buttons/Input
* [button-simple-1](./button-simple-1/README.md) - Cross platform design. Easy to generate for any desired language. A simple button state machine that can be easily customized to your needs. `ss.cli` `plantuml`
* [buttons-lights-1](./buttons-lights-1/README.md) - Multiple button state machines sending events to a light state machine. Arduino simulation. `drawio` `csx`
* [plantuml-arduino-buttons](./plantuml-arduino-buttons/README.md)
* todo - migrate [laser tag example](https://www.youtube.com/watch?v=9czSDothuzM) [simulation](https://wokwi.com/projects/351165738904453719).

## ⏚ Embedded
* [embedded-solar-1](./embedded-solar-1/README.md) - `ss.cli`
* [button-simple-1](./button-simple-1/README.md) - `ss.cli`
* [rtos-event-queue-c](./rtos-event-queue-c/README.md)
* todo - migrate [laser tag example](https://www.youtube.com/watch?v=9czSDothuzM) [simulation](https://wokwi.com/projects/351165738904453719).

## 🌱 PlantUML
* [embedded-solar-1](./embedded-solar-1/README.md) - Shows how to make a nested menu state machine with PlantUML. `ss.cli`
* [mario-sm-2](./mario-sm-2/README.md) - `ss.cli`
* [button-simple-1](./button-simple-1/README.md) - A simple button state machine that can be easily customized to your needs. `ss.cli`
* [c-include-sm-basic-2-plantuml-tutorial](./c-include-sm-basic-2-plantuml-tutorial/README.md) - `ss.cli`
* [parent-alias-1](./parent-alias-1/README.md) - `ss.cli`
* [plantuml-basic-1](./plantuml-basic-1/README.md)
* [plantuml-arduino-buttons](./plantuml-arduino-buttons/README.md)

<br>

# By Language

## C
* [c-include-sm-basic-2-plantuml-tutorial](./c-include-sm-basic-2-plantuml-tutorial/README.md) - easy connection between user code and fsm. No expansions needed. `ss.cli`
* [c-include-sm-basic-2-drawio](./c-include-sm-basic-2-drawio/README.md) - easy connection between user code and fsm. No expansions needed. `ss.cli`
* [rtos-event-queue-c](./rtos-event-queue-c/README.md)
* [example-drawio-1 repo](https://github.com/StateSmith/example-drawio-1)
* [logging-simple-1](./logging-simple-1/README.md)
* [logging-simple-script-2](./logging-simple-script-2/README.md)

## C#
* [csharp-space-menu-1](csharp-space-menu-1/README.md) - Advanced menu system with C#. Concurrency. Deep nesting, entry/exit points, parent alias and more.

## C++
* [cpp-inheritance-1](./cpp-inheritance-1/README.md) - Shows easy state machine testing. Uses a base class. `ss.cli` `plantuml`.
* [cpp-inheritance-2](./cpp-inheritance-2/README.md) - Shows easy state machine testing. Uses custom `ClassCode`. `ss.cli` `plantuml`.

## C++ / Arduino
* [button-simple-1](./button-simple-1/README.md) - `ss.cli`
* [embedded-solar-1](./embedded-solar-1/README.md) - `ss.cli`
* [buttons-lights-1](./buttons-lights-1/README.md) - Multiple button state machines sending events to a light state machine. Arduino simulation. `drawio` `csx`
* [plantuml-arduino-buttons](./plantuml-arduino-buttons/README.md)
* TODO: migrate [laser tag example](https://www.youtube.com/watch?v=9czSDothuzM) [simulation](https://wokwi.com/projects/351165738904453719).

## JavaScript
* [mario-sm-3](./mario-sm-3/README.md) - Adds invincible star state, history, thwomp. Uses new 0.12.0+ lib features. `ss.cli` `drawio` `new`
* [mario-sm-2](./mario-sm-2/README.md) - Adds invincible star state and history. `ss.cli`
* [mario-sm-1](./mario-sm-1/README.md)
* [history-storage-1](./history-storage-1/README.md)

<br>

# 🏘️ Community Examples 🎁
* Be the first to create an example and send us the link! It can be as simple or complicated as you like.
