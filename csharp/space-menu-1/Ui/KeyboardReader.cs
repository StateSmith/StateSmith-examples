using System.Collections.Concurrent;

namespace ConsoleApp1.Ui;

public class KeyboardReader
{
    private readonly BlockingCollection<UiSm.EventId> smEventQueue;

    public KeyboardReader(BlockingCollection<UiSm.EventId> smEventQueue)
    {
        this.smEventQueue = smEventQueue;
    }

    public void ReadKeyboardMaybeAddEvent()
    {
        // note: this is a blocking call
        var key = Console.ReadKey(intercept: true);

        UiSm.EventId? eventId = null;

        switch (key.Key)
        {
            case ConsoleKey.Escape: eventId = UiSm.EventId.ESC; break;
            case ConsoleKey.DownArrow: eventId = UiSm.EventId.DOWN; break;
            case ConsoleKey.UpArrow: eventId = UiSm.EventId.UP; break;
            case ConsoleKey.RightArrow: eventId = UiSm.EventId.RIGHT; break;
            case ConsoleKey.LeftArrow: eventId = UiSm.EventId.LEFT; break;
            case ConsoleKey.PageDown: eventId = UiSm.EventId.PG_DOWN; break;
            case ConsoleKey.PageUp: eventId = UiSm.EventId.PG_UP; break;
        }

        if (eventId != null)
            smEventQueue.Add(eventId.Value);
    }
}
