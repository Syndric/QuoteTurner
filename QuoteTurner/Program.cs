using Func;

namespace MTQuoteTools
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("> Startup. use help");
            FileFolder fileFolder = new FileFolder();
            
            while(true)
            {
                string x = Console.ReadLine();
                string[] parts = x.Split(' ');
                string cmd = parts[0];
                string[] options = parts.Skip(1).ToArray();

                if(cmd == "help")
                {
                    Cmd.Help();
                } else if(cmd == "load")
                {
                    fileFolder = Cmd.Load(fileFolder, options);
                } else if(cmd == "write")
                {
                    Cmd.Write(fileFolder, options);
                } else if(cmd == "disp")
                {
                    Cmd.Disp(fileFolder, options);
                } else if(cmd == "clear")
                {
                    fileFolder = Cmd.Clear(fileFolder, options);
                } else if(cmd == "unload")
                {
                    fileFolder = Cmd.Unload(fileFolder, options);
                } else if(cmd == "verify")
                {
                    fileFolder = Cmd.Verify(fileFolder, options);
                } else if(cmd == "langset")
                {
                    fileFolder = Cmd.Langset(fileFolder, options);
                }
            }
        }
    }
}
