using System.Collections.Concurrent;

namespace ConsoleApp1.Ui;

public class MyUi
{
    /// <summary>
    /// The StateSmith generated state machine is not thread safe.
    /// We buffer events to the state machine to ensure thread safety.
    /// </summary>
    private readonly UiSm stateMachine = new();

    /// <summary>
    /// The StateSmith generated state machine is not thread safe so we create a thread safe
    /// queue to buffer events that should be dispatched to the state machine.
    /// </summary>
    private readonly BlockingCollection<UiSm.EventId> smEventQueue = new();
    private readonly Display display;

    public bool IsDone => stateMachine.stateId == UiSm.StateId.DONE;

    public MyUi()
    {
        display = new(stateMachine);
        stateMachine.SetDisplay(display);
    }

    public void Start()
    {
        stateMachine.Start();

        StartKeyboardTask();
        StartDoEventIntervalTask();
        StartEventLoopTask();
    }

    private void StartEventLoopTask()
    {
        Task.Run(() =>
        {
            while (!IsDone)
            {
                UiSm.EventId smEventId = smEventQueue.Take();
                stateMachine.DispatchEvent(smEventId);
                display.UpdateSmInfo();
                display.RepaintIfNeeded();
            }
        });
    }

    private void StartKeyboardTask()
    {
        KeyboardReader keyboardReader = new(smEventQueue);

        Task.Run(() =>
        {
            while (!IsDone)
            {
                keyboardReader.ReadKeyboardMaybeAddEvent();
            }
        });
    }

    private void StartDoEventIntervalTask()
    {
        Task.Run(() =>
        {
            while (!IsDone)
            {
                smEventQueue.Add(UiSm.EventId.DO);
                Thread.Sleep(100);
            }
        });
    }
}
