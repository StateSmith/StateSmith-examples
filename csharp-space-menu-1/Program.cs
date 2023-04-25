using ConsoleApp1.Ui;

namespace ConsoleApp1;

internal class Program
{
    static void Main()
    {
        MyUi ui = new();

        ui.Start();

        while (!ui.IsDone)
        {
            Thread.Sleep(100);
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Thank you for visiting space command!");
        Console.ResetColor();
    }
}
