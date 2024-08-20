using System;
using System.IO;
using System.Xml;
namespace Func
{
    public static class Cmd
    {
        public static void Help()
        {
            string[] msg = {
                "> load <filepaths[]>\n\tload json or csv file into memory\n\t--all: treat path as folder instead",
                "> unload <languages[]>\n\tunload language from memory",
                "> clear\n\tclear memory",
                "> write\n\twrite memory to ./output\n\t--disp",
                "> verify\n\tverify memory\n\t--valids\n\t--invalids\n\t--ends\n\t--enters\n\t--dash\n\t--dquote\n\t--length\n\t--all\n\t--nocorr",
                "> disp <optional:language>\n\tdisplay memory\n\t--lang: display languages",
                "> langset <old> <new>\n\tset language"
            };
            foreach (string x in msg)
            {
                Console.WriteLine(x);
            }
        }

        public static FileFolder Load(FileFolder fileFolder, string[] options)
        {
            bool all = false;
            List<string> filePaths = new List<string>();
            foreach (string x in options) //option handler
            {
                if (x.Contains("--all"))
                {
                    all = true;
                }
                else
                {
                    filePaths.Add(x);
                }
            }
            if (filePaths.Count == 0) //no path specified
            {
                Console.WriteLine(">>> incorrect format");
                return fileFolder;
            }
            if (all)
            {
                string path = filePaths[0];
                filePaths = Directory.GetFiles(path, "*.json", SearchOption.TopDirectoryOnly).ToList();
                filePaths.AddRange(Directory.GetFiles(path, "*.csv", SearchOption.TopDirectoryOnly).ToList());

            }
            else
            {
                List<string> tempPaths = filePaths.ToList();
                foreach (string x in tempPaths)
                {
                    if (!File.Exists(x)) //check if path does not exists
                    {
                        filePaths.Remove(x);
                    }
                }

            }
            Console.WriteLine("> Loaded:");
            foreach (string x in filePaths)
            {
                QuoteFile file = new QuoteFile();
                if(x.Contains(".json"))
                {
                    file = OpFunc.LoadJSON(x);
                    
                } else if (x.Contains(".csv"))
                {
                    file.quotes = OpFunc.LoadCSV(x);
                    Console.WriteLine($"> Enter a language for {x}");
                    file.language = Console.ReadLine();
                }
                QuoteFile match = fileFolder.files.Find(o => o.language == file.language);
                if (match == null)
                {
                    fileFolder.files.Add(file);
                }
                else
                {
                    match.quotes.AddRange(file.quotes);
                }
                Console.WriteLine($"> {x}");
            }
            Console.WriteLine("> Finished");
            return fileFolder;


        }

        public static void Write(FileFolder fileFolder, string[] options)
        {
            bool disp = false;
            foreach (string x in options)
            {
                if (x.Contains("--disp"))
                {
                    disp = true;
                }
            }
            string totalOut = "";
            foreach (QuoteFile x in fileFolder.files)
            {
                totalOut = totalOut + OpFunc.WriteJSON(x) + "\n";
            }
            if (disp)
            {
                Console.WriteLine(totalOut);
            }
            Console.WriteLine("> Finished");
        }

        public static void Disp(FileFolder fileFolder, string[] options)
        {
            bool lang = false;
            string language = null;
            foreach (string x in options)
            {
                if (x.Contains("--lang"))
                {
                    lang = true;
                } else
                {
                    language = x;
                }
            }
            string totalOut = "";
            if(language!=null)
            {
                totalOut = totalOut + OpFunc.DispJSON(fileFolder.files.Find(o => o.language == language)) + "\n";
            } else
            {
                foreach (QuoteFile x in fileFolder.files)
                {
                    if (lang)
                    {
                        totalOut = totalOut + x.language + "\n";
                    }
                    else
                    {
                        totalOut = totalOut + OpFunc.DispJSON(x) + "\n";
                    }
                }
            }
           
            Console.WriteLine(totalOut);
            Console.WriteLine("> Finished");
        }

        public static FileFolder Clear(FileFolder fileFolder, string[] options)
        {
            Console.WriteLine("> Finished");
            return new FileFolder();

        }
        public static FileFolder Unload(FileFolder fileFolder, string[] options)
        {
            Console.WriteLine("Removed:");
            foreach (string x in options)
            {
                QuoteFile file = fileFolder.files.Find(o => o.language == x);
                if (file != null)
                {
                    fileFolder.files.Remove(file);
                    Console.WriteLine(x);
                }
            }
            Console.WriteLine("> Finished");
            return fileFolder;


        }

        public static FileFolder Verify(FileFolder fileFolder, string[] options)
        {
            bool filterInvalids = false;
            bool filterValids = false;
            bool filterEnds = false;
            bool filterEnters = false;
            bool filterDash = false;
            bool filterLength = false;
            bool filterDquote = false;
            bool correction = true;
            foreach (string x in options)
            {
                if (x.Contains("--invalids"))
                {
                    filterInvalids = true;
                }
                if (x.Contains("--valids"))
                {
                    filterValids = true;
                }
                if (x.Contains("--ends"))
                {
                    filterEnds = true;
                }
                if (x.Contains("--enters"))
                {
                    filterEnters = true;
                }
                if (x.Contains("--dash"))
                {
                    filterDash = true;
                }
                if (x.Contains("--length"))
                {
                    filterLength = true;
                }
                if (x.Contains("--nocorr"))
                {
                    correction = false;
                }
                if(x.Contains("--dquote"))
                {
                    filterDquote = true;
                }
                if (x.Contains("--all"))
                {
                    filterInvalids = true;
                    filterValids = true;
                    filterEnds = true;
                    filterLength = true;
                    filterEnters = true;
                    filterDquote = true;
                }
            }
            foreach (QuoteFile file in fileFolder.files)
            {
                Console.WriteLine($"\n> open {file.language}\n");
                foreach (Quote quote in file.quotes)
                {
                    if (filterInvalids)
                    {
                        Filter.FilterInvalids(quote, correction);
                    }
                    if (filterValids)
                    {
                        Filter.FilterValids(quote, correction);
                    }
                    if (filterEnds)
                    {
                        Filter.FilterEnds(quote, correction);
                    }
                    if(filterEnters)
                    {
                        Filter.FilterEnters(quote, correction);
                    }
                    if(filterDash)
                    {
                        Filter.FilterDash(quote, correction);
                    }
                    if(filterDquote)
                    {
                        Filter.FilterDquote(quote, correction);
                    }
                    if (filterLength)
                    {
                        Filter.FilterLength(quote, correction);
                    }

                }
                OpFunc.WriteJSON(file);
                Console.WriteLine($"\n> write {file.language}\n");
            }
            Console.WriteLine("> Finished");
            return fileFolder;
        }

        public static FileFolder Langset(FileFolder fileFolder, string[] options)
        {
            if (options.Length != 2)
            {
                Console.WriteLine(">>> invalid format");
                return fileFolder;
            }
            QuoteFile match = fileFolder.files.Find(o => o.language == options[0]);
            if (match == null)
            {
                Console.WriteLine(">>> language not found");
                return fileFolder;
            }
            match.language = options[1];
            Console.WriteLine($"replace {options[0]} -> {options[1]}");
            Console.WriteLine("> Finished");
            return fileFolder;

        }
    }
}
