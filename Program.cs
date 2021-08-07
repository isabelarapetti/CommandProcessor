using System;
using System.Collections.Generic;

namespace ConsoleAppSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Aloha");

            //initialize filesystem
            FileSystemElement fs = new FileSystemElement("Root", null);

            while (true)
            {
                var input = System.Console.ReadLine().Trim();
                var arguments = new List<string>(input.Split(' '));
                var commandName = arguments[0];

                var command = SimpleCommandFactory.CreateCommand(commandName, fs); //TODO remove fs
                command.Execute(arguments,fs);
            }
        }
    }

    #region COMMANDS REGION
    public interface ICommand
    {
        void Execute(List<string> arguments, FileSystemElement fs);
    }

    public class SimpleCommandFactory
    {
        public static ICommand CreateCommand(String commandName, FileSystemElement fs)
        {
            switch (commandName)
            {
                case "pwd":
                    return new PrintDirectoryCommand();
                case "mkdir":
                    return new CreateDirectoryCommand();
                case "ls":
                    return new ListCommand();
                case "cd":
                    return new ChangeDirectoryCommand();
                case "touch":
                    return new CreateFileCommand();
                case "quit":
                    return new QuitCommand();
                default:
                    return new UnrecognizedCommand();
            }
        }
    }

    //Concrete Commands
    public class QuitCommand : ICommand
    {
        public void Execute(List<string> arguments)
        {
            Console.WriteLine($"Shutting down!");
            Environment.Exit(0);
        }
    }
    public class UnrecognizedCommand : ICommand
    {
        public void Execute(List<string> arguments)
        {
            Console.WriteLine($"Command not recognized");
        }
    }
    public class PrintDirectoryCommand : ICommand //pwd
    {
        public void Execute(List<string> arguments, FileSystemElement currentNode)
        {
            Console.WriteLine($"PrintDirectoryCommand");
        }
    }
    public class CreateDirectoryCommand : ICommand //mkdir
    {
        public void Execute(List<string> arguments, FileSystemElement currentNode)
        {
            new FileSystemElement(arguments[0], currentNode, true);
            Console.WriteLine($"Create Directory {arguments[0]}");
        }
    }
    public class CreateFileCommand : ICommand //touch
    {
        public void Execute(List<string> arguments, FileSystemElement currentNode)
        {
            new FileSystemElement(arguments[0], currentNode, false);
            Console.WriteLine($"Create Directory {arguments[0]}");
        }
    }

    public class ListCommand : ICommand //ls
    {
        public void Execute(List<string> arguments, FileSystemElement currentNode)
        {
            Console.WriteLine($"ListCommand");
        }
    }

    public class ChangeDirectoryCommand : ICommand  //cd
    {
        public void Execute(List<string> arguments, FileSystemElement currentNode)
        {
            Console.WriteLine($"ChangeDirectoryCommand");
        }
    }
    #endregion

    #region FILES REGION

    //Directories and Files
    public class FileSystemElement
    {
        public string Name { get; set; }
        public FileSystemElement Parent { get; set; }
        public bool IsDirectory { get; set; }
        public List<FileSystemElement> Children { get; set; }

        public FileSystemElement(string name, FileSystemElement parent, bool isDirectory = false)
        {
            Name = name;
            Parent = parent;
            IsDirectory = IsDirectory;
        }

        public string GetCurrentFullPath()
        {
            if (Parent == null)
            {
                return Name;
            }
            else
            {
                return Parent.GetCurrentFullPath() + "/" + Name;
            }
        }

        //public void AddChildren(FileSystemElement node)
        //{
        //    Children.Add(node);
        //}

    }
    #endregion
}
