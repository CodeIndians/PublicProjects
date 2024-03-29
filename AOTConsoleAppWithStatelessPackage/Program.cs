﻿using System;
using Stateless;
using System.Data.SQLite;

class Program
{
    enum State { Green, Yellow, Red }

    static void Main()
    {
        TestStateless();
        TestSQLite();
        Console.Write("Press any key to close the application");
        Console.ReadLine();
    }

    static void TestStateless()
    {
        var machine = new StateMachine<State, string>(State.Green);
        Console.WriteLine("Now testing Staless \n");
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

        Console.WriteLine("\n Test Complete \n");
    }

    static void TestSQLite()
    {
        try
        {

            Console.WriteLine("\n SQLite AOT Console App Testing");

            using (var connection = new SQLiteConnection("Data Source=sample.db"))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "CREATE TABLE IF NOT EXISTS SampleTable (Id INT, Name TEXT)";
                    command.ExecuteNonQuery();
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO SampleTable (Id, Name) VALUES (1, 'John Doe')";
                    command.ExecuteNonQuery();
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM SampleTable";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id: {reader.GetInt32(0)}, Name: {reader.GetString(1)}");
                        }
                    }
                }

                Console.WriteLine("\n Test Complete \n");
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

}
