#!/usr/bin/env dotnet-script
#r "nuget: StateSmith, 0.9.2-alpha" // this line specifies which version of StateSmith to use and download from c# nuget web service.

// spell-checker: ignore drawio

using System.Text.RegularExpressions;
using StateSmith.Common;
using StateSmith.Input.Expansions;
using StateSmith.Output.UserConfig;
using StateSmith.Runner;
using StateSmith.SmGraph;

SmRunner runner = new(diagramPath: "MySm.drawio.svg");
SupportEnterExitShorthands();
SupportAnyTriggerAndEvent();
runner.Run();

// --- functions follow ---------

void SupportEnterExitShorthands()
{
    // We add our custom transformation to run after unspecified events are converted to the `do` event.
    // This doesn't matter too much for this part, but our special $any_t and $any_e triggers search the state machine
    // for all used events. This part should happen after Standard_DefaultUnspecifiedEventsAsDoEvent.
    runner.SmTransformer.InsertAfterFirstMatch(StandardSmTransformer.TransformationId.Standard_DefaultUnspecifiedEventsAsDoEvent,
        new TransformationStep(id: nameof(SupportEnterExitShorthands), action: (sm) =>
        {
            // change "en" triggers to "enter", and "ex" triggers to "exit".
            sm.VisitTypeRecursively<Vertex>(vertex =>
            {
                foreach (var behavior in vertex.Behaviors)
                {
                    for (int i = 0; i < behavior.Triggers.Count; i++)
                    {
                        string trigger = TriggerHelper.SanitizeTriggerName(behavior.Triggers[i]);
                        switch (trigger)
                        {
                            case "en":
                                behavior._triggers[i] = TriggerHelper.TRIGGER_ENTER;
                                break;
                            case "ex":
                                behavior._triggers[i] = TriggerHelper.TRIGGER_EXIT;
                                break;
                        }
                    }
                }
            });
        })
    );
}

void SupportAnyTriggerAndEvent()
{
    HashSet<string> foundSmEvents = new();

    // This step needs to run after SupportEnterExitShorthands so we don't collect the "en","ex" shorthands.
    runner.SmTransformer.InsertAfterFirstMatch(nameof(SupportEnterExitShorthands),
        new TransformationStep(id: nameof(SupportAnyTriggerAndEvent), action: (sm) =>
        {
            // Step 1: visit all vertices to collect all events used in the state machine
            sm.VisitTypeRecursively<Vertex>(vertex =>
            {
                foreach (var behavior in vertex.Behaviors)
                {
                    foreach (var trigger in behavior.SanitizedTriggers)
                    {
                        // collect events (other than our special $any_t and $any_e)
                        if (TriggerHelper.IsEvent(trigger) && !trigger.StartsWith("$"))
                        {
                            foundSmEvents.Add(trigger);
                        }
                    }
                }
            });

            // Step 2: convert `$any_t` and `$any_e` to those found in step 1 above.
            sm.VisitTypeRecursively<Vertex>(vertex =>
            {
                foreach (var behavior in vertex.Behaviors)
                {
                    List<string> newTriggerList = new();
                    foreach (string trigger in behavior.SanitizedTriggers)
                    {
                        if (trigger == "$any_t")
                        {
                            newTriggerList.Add(TriggerHelper.TRIGGER_ENTER);
                            newTriggerList.Add(TriggerHelper.TRIGGER_EXIT);
                            newTriggerList.AddRange(foundSmEvents);
                        }
                        else if (trigger == "$any_e")
                        {
                            newTriggerList.AddRange(foundSmEvents);
                        }
                        else
                        {
                            newTriggerList.Add(trigger);
                        }
                    }

                    behavior._triggers = newTriggerList;
                }
            });
        })
    );
}
