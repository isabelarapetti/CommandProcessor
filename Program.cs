using System;
using System.Collections.Generic;

namespace ConsoleAppSimple
{
    class Program
    {
        static void Main(string[] args)
        {
            //initialize filesystem
            FileSystemElement fs = new FileSystemElement("Root", null);

            while (true)
            {
                var input = System.Console.ReadLine().Trim();

                ParseInput(input, out string argument, out string commandName);

                var command = SimpleCommandFactory.CreateCommand(commandName);
                command.Execute(argument, fs);
            }
        }

        private static void ParseInput(string input, out string argument, out string commandName)
        {
            commandName = null;
            argument = null;

            if (string.IsNullOrWhiteSpace(input)) {
                Console.WriteLine($"Invalid command");
            }
            else
            {
                var words = new List<string>(input.Split(' '));
                commandName = words[0];
                argument = words.Count > 1 ? words[1] : null;
            }

        }
    }

    #region COMMANDS REGION
    public interface ICommand
    {
        void Execute(string argument, FileSystemElement currentNode);
    }

    public class SimpleCommandFactory
    {
        public static ICommand CreateCommand(String commandName)
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
        //TODO switch to class instead of Int to avoid unused arg implementation
        public void Execute(string argument, FileSystemElement currentNode)
        {
            Console.WriteLine($"Shutting down!");
            Environment.Exit(0);
        }
    }
    public class UnrecognizedCommand : ICommand
    {
        public void Execute(string argument, FileSystemElement currentNode)
        {
            Console.WriteLine($"Command not recognized");
        }
    }
    public class PrintDirectoryCommand : ICommand //pwd
    {
        public void Execute(string argument, FileSystemElement currentNode)
        {
            currentNode.GetCurrentFullPath();
        }
    }
    public class CreateDirectoryCommand : ICommand //mkdir
    {
        public void Execute(string argument, FileSystemElement currentNode)
        {
            if (argument == null || argument.Length > 100)
            {
                Console.WriteLine($"Invalid command:  {argument}");
            }
            else
            {
                new FileSystemElement(argument, currentNode, true);
                Console.WriteLine($"Create Directory {argument}");
            }

        }
    }
    public class CreateFileCommand : ICommand //touch
    {
        public void Execute(string argument, FileSystemElement currentNode)
        {
            if (argument == null || argument.Length > 100)
            {
                Console.WriteLine($"Invalid command:  {argument}");
            }
            else
            {
                new FileSystemElement(argument, currentNode, false);
                Console.WriteLine($"Create File {argument}");
            }
        }
    }

    public class ListCommand : ICommand //ls
    {
        public void Execute(string argument, FileSystemElement currentNode)
        {
            Console.WriteLine($"ListCommand");
        }
    }

    public class ChangeDirectoryCommand : ICommand  //cd
    {
        public void Execute(string argument, FileSystemElement currentNode)
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
