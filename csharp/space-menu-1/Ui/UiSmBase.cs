using System.Diagnostics;

namespace ConsoleApp1.Ui;

public class UiSmBase
{
    public int BurritoCount => burritoCount;
    public int Count => count;

    protected int count;
    protected int burritoCount = 42;
    protected readonly Stopwatch t1 = new();
    protected readonly Stopwatch selfDestructStopWatch = new();
    protected Display display;

    public UiSmBase()
    {
        // One area of improvement for StateSmith is generating a state machine that inherits from
        // a base class that takes constructor arguments. Open to suggestions on how to specify this.
        display = null!; // to ignore nullable warning.
    }

    public void SetDisplay(Display display)
    {
        this.display = display;
    }

    protected void MenuHeader(string header) => display.MenuHeader(header);
    protected void MenuOption(string option) => display.MenuOption(option);
    protected void ShowSplashScreen() => display.ShowSplashScreen();
    protected void ShowDoneScreen() => display.ShowDoneScreen();

    protected static void SelfDestruct()
    {
        Environment.Exit(1000);
    }

    protected static void Beep()
    {
        Console.Beep();
    }

    protected void StartSelfDestructTimer()
    {
        selfDestructStopWatch.Restart();
    }

    protected int SelfDestructSeconds => 10 - selfDestructStopWatch.Elapsed.Seconds;


}

