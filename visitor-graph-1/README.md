This example shows how to analyze a state machine graph using the [Visitor Pattern](https://en.wikipedia.org/wiki/Visitor_pattern).

Useful for when you want to output additional information about the state machine.

For example, you could output the state machine graph in mermaid.js, plantuml, or any other format.

Ignore the generated .c/.h files. They aren't interesting for this example.

See the .csx file for the visitor code.

Example output when .csx file is run:

```
Starting to visit the graph
        Visiting StateMachine: OrderingSm
                Visiting State: ORDER_MENU
                        Visiting State: BEVERAGE
                                Visiting InitialState
                                Visiting State: WATER
                                Finished Visiting State: WATER
                                Visiting State: COFFEE
                                Finished Visiting State: COFFEE
                                Visiting State: TEA
                                Finished Visiting State: TEA
                        Finished Visiting State: BEVERAGE
                        Visiting State: FOOD
                                Visiting InitialState
                                Visiting State: JUNK
                                Finished Visiting State: JUNK
                                Visiting State: POTATO
                                Finished Visiting State: POTATO
                                Visiting State: SUSHI
                                Finished Visiting State: SUSHI
                        Finished Visiting State: FOOD
                        Visiting EntryPoint with label: to_history
                        Visiting ExitPoint with label: 1
                        Visiting HistoryVertex
                        Visiting ChoicePoint with label: bev_choice
                Finished Visiting State: ORDER_MENU
                Visiting State: WAITING
                Finished Visiting State: WAITING
                Visiting InitialState
        Finished Visiting StateMachine: OrderingSm
Finished visiting the graph
```