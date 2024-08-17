using Func;

namespace MTQuoteTools
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("> startup. use help");
            QuoteFile file = new QuoteFile();
            
            while(true)
            {
                string x = Console.ReadLine();
                string[] parts = x.Split(' ');
                if (parts[0] == "help")
                {
                    OpFunc.Help();
                }
                else if (parts[0] == "disp")
                {
                    OpFunc.DispJSON(file);
                }
                else if (parts[0] == "load")
                {
                    if (parts[1].Contains("json"))
                    {
                        QuoteFile processed = OpFunc.LoadJSON(parts[1]);
                        file.quotes.AddRange(processed.quotes); //append quotes
                        file.language = processed.language;
                        file.groups = processed.groups;
                    } else if (parts[1].Contains("csv"))
                    {
                        file.quotes.AddRange(OpFunc.LoadCSV(parts[1]));
                    }
                    
                    Console.WriteLine("> JSON loaded");
                } 
                else if (parts[0] == "write")
                {
                    string output = OpFunc.WriteJSON(file);
                    Console.WriteLine($"> Output written to output/{file.language}.json, write to console? (Y/N)");
                    string y = Console.ReadLine();
                    if(y.ToUpper() == "Y")
                    {
                        Console.WriteLine(output);
                    }
                }
                else if (parts[0] == "clear")
                {
                    file = new QuoteFile();
                    Console.WriteLine("> Cleared");
                }
                else if (parts[0] == "verify")
                {
                    bool fix = parts[1] == "1";
                    OpFunc.Verify(file, fix);
                    Console.WriteLine("> Verified");
                }
            }
        }
    }
}
