using System;
using System.Collections.Generic;

namespace ConsoleAppSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aloha");
            while (true)
            {
                var input = System.Console.ReadLine().Trim();
                var arguments = new List<string>(input.Split(' '));
                var commandName = arguments[0];

                //var command = SimpleCommandFactory.CreateCommand(commandName);

                //command.Execute(arguments);
            }
        }
    }

    #region COMMANDS REGION
    public interface ICommand
    {
        void Execute(List<string> arguments);
    }

    public class SimpleCommandFactory
    {
        //create command instance 
    }

    //Concrete Commands
    public class CreateFileCommand : ICommand
    {
        public void Execute(List<string> arguments)
        {
            Console.WriteLine($"Hello World! CreateFileCommand");
        }
    }

    public class PrintDirectoryCommand : ICommand
    {
        public void Execute(List<string> arguments)
        {
            Console.WriteLine($"Hello World! PrintDirectoryCommand");
        }
    }

    public class ListCommand : ICommand
    {
        public void Execute(List<string> arguments)
        {
            Console.WriteLine($"Hello World! ListCommand");
        }
    }

    public class ChangeDirectoryCommand : ICommand
    {
        public void Execute(List<string> arguments)
        {
            Console.WriteLine($"Hello World! ChangeDirectoryCommand");
        }
    }

    public class CreateDirectoryCommand : ICommand
    {
        public void Execute(List<string> arguments)
        {
            Console.WriteLine($"Hello World! CreateDirectoryCommand");
        }
    }

    public class QuitCommand : ICommand
    {
        public void Execute(List<string> arguments)
        {
            Console.WriteLine($"Shutting down!");
            Environment.Exit(0);
        }
    }
    public class NotFoundCommand : ICommand
    {
        public void Execute(List<string> arguments)
        {
            Console.WriteLine($"Hello World! NotFoundCommand");
        }
    }
    #endregion

    #region FILES REGION

    #endregion
}
