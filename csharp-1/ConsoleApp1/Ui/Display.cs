using System.Reflection.PortableExecutable;

namespace ConsoleApp1.Ui;

public class Display
{
    private bool menuMode = false;

    private string menuHeader = "";
    private string menuOption = "";
    private string smInfo = "";

    private readonly UiSm uiSm;

    private bool needsRepaint;

    public Display(UiSm uiSm)
    {
        this.uiSm = uiSm;
    }

    public void SetMenuMode(bool menuMode) => this.menuMode = menuMode;

    public void MenuHeader(string header)
    {
        if (menuHeader != header)
            needsRepaint = true;

        menuHeader = header;
    }

    public void MenuOption(string option)
    {
        if (menuOption != option)
            needsRepaint = true;

        menuOption = option;
    }

    public void UpdateSmInfo()
    {
        var newSmInfo = $"Debug Info - State: {uiSm.stateId}, BurritoCount: {uiSm.BurritoCount}, Count: {uiSm.Count}";

        if (smInfo != newSmInfo)
            needsRepaint = true;

        smInfo = newSmInfo;
    }

    public void ShowSplashScreen()
    {
        menuMode = false;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(DisplayStrings.splashScreen);
    }

    public void ShowDoneScreen()
    {
        menuMode = false;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(DisplayStrings.doneScreen);
    }

    public void RepaintIfNeeded()
    {
        if (!menuMode)
            return;

        if (!needsRepaint)
            return;

        needsRepaint = false;

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(smInfo);
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(menuHeader);

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(menuOption);
    }
}
