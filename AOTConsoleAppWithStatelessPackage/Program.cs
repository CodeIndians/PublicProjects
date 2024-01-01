using System;
using Stateless;

class Program
{
    enum State { Green, Yellow, Red }

    static void Main()
    {
        var machine = new StateMachine<State, string>(State.Green);

        machine.Configure(State.Green)
            .Permit("Change", State.Yellow);

        machine.Configure(State.Yellow)
            .Permit("Change", State.Red);

        machine.Configure(State.Red)
            .Permit("Change", State.Green);

        Console.WriteLine($"Current state: {machine.State}");

        Console.Write("Press 'Enter' to transition to the next state...");
        Console.ReadLine();

        machine.Fire("Change");

        Console.WriteLine($"Current state: {machine.State}");

        Console.Write("Press any key to close the application");
        Console.ReadLine();
    }
}
