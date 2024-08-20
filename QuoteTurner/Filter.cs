using System;
using System.ComponentModel.Design;
namespace Func
{
    public class Filter
    {
        public static Quote FilterInvalids(Quote quote, bool fix)
        {
            string[,] invalids = {
                { "‎‎ ", "" },
                { "“", "\""},
                { "”", "\""},
                { "‘", "\'"},
                { "’", "\'"},
                { "…", "..." },
                { "  ", " " }
            };
            for (int i = 0; i < invalids.GetLength(0); i++)
            {
                if (quote.text.Contains(invalids[i, 0]))
                {
                    Console.WriteLine($"Invalid str found id {quote.id}: [{invalids[i, 0]}]");
                    if (fix)
                    {
                        if (PromptChange(invalids[i, 0], invalids[i, 1], quote.text))
                        {
                            quote.text = quote.text.Replace(invalids[i, 0], invalids[i, 1]);
                        }
                    }
                }
            }

            return quote;
        }

        public static Quote FilterDquote(Quote quote, bool fix)
        {
            string[,] invalids = {
                { "\'\'", "" },
                { "\"\"", "" },
            };
            for (int i = 0; i < invalids.GetLength(0); i++)
            {
                if (quote.text.Contains(invalids[i, 0]))
                {
                    Console.WriteLine($"Invalid str found id {quote.id}: [{invalids[i, 0]}]");
                    if (fix)
                    {
                        if (PromptChange(invalids[i, 0], invalids[i, 1], quote.text))
                        {
                            quote.text = quote.text.Replace(invalids[i, 0], invalids[i, 1]);
                        }
                    }
                }
            }

            return quote;
        }
        
        public static Quote FilterDash(Quote quote, bool fix)
        {
            string[,] invalids = {
                { "—", "-"},
                { "–", "-"}

            };
            for (int i = 0; i < invalids.GetLength(0); i++)
            {
                if (quote.text.Contains(invalids[i, 0]))
                {
                    Console.WriteLine($"Invalid str found id {quote.id}: [{invalids[i, 0]}]");
                    if (fix)
                    {
                        if (PromptChange(invalids[i, 0], invalids[i, 1], quote.text))
                        {
                            quote.text = quote.text.Replace(invalids[i, 0], invalids[i, 1]);
                        }
                    }
                }
            }

            return quote;
        }

        public static Quote FilterEnters(Quote quote, bool fix)
        {
            string[,] invalids = {
                { "\n", " " }
            };
            for (int i = 0; i < invalids.GetLength(0); i++)
            {
                if (quote.text.Contains(invalids[i, 0]))
                {
                    Console.WriteLine($"Invalid str found id {quote.id}: [{invalids[i, 0]}]");
                    if (fix)
                    {
                        if (PromptChange(invalids[i, 0], invalids[i, 1], quote.text))
                        {
                            quote.text = quote.text.Replace(invalids[i, 0], invalids[i, 1]);
                        }
                    }
                }
            }
            return quote;
        }

        public static Quote FilterValids(Quote quote, bool fix)
        {
            char[] valids = {
                ' ', '!', '#', '$', '%', '&', '*', '(', ')', '-', '_',
                '[', ']', '{', '}', '/', '.', ',', ';', ':', '\'', '\"',
                '?' , '+', '=',  '¡', '¿'
            };

            foreach (char c in quote.text)
            {
                if (!Char.IsLetterOrDigit(c))
                {
                    bool found = false;
                    foreach (char valid in valids)
                    {
                        if (c == valid)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        Console.WriteLine($"Invalid str found id {quote.id}: [{c}]");
                    }
                }
            }
            return quote;
        }

        public static Quote FilterEnds(Quote quote, bool fix)
        {
            if (quote.text.Substring(quote.text.Length - 1, 1) == " ")
            {
                Console.WriteLine($"Space ending text id {quote.id}");
                if (fix)
                {
                    if (PromptChange(" end", "end", quote.text))
                    {
                        quote.text = quote.text.Substring(0, quote.text.Length - 1);
                    }
                }
            }
            if (quote.text.Substring(0, 1) == " ")
            {
                Console.WriteLine($"Space begining text id {quote.id}");
                if (fix)
                {
                    if (PromptChange("begin ", "begin", quote.text))
                    {
                        quote.text = quote.text.Substring(1);
                    }
                }
            }
            if (quote.source.Length > 0)
            {
                if (quote.source.Substring(quote.source.Length - 1, 1) == " ")
                {
                    Console.WriteLine($"Space ending source id {quote.id}");
                    if (fix)
                    {
                        if (PromptChange(" end", "end", quote.source))
                        {
                            quote.source = quote.source.Substring(0, quote.source.Length - 1);
                        }
                    }
                }
                if (quote.source.Substring(0, 1) == " ")
                {
                    Console.WriteLine($"Space begining source id {quote.id}");
                    if (fix)
                    {
                        if (PromptChange("begin ", "begin", quote.source))
                        {
                            quote.source = quote.source.Substring(1);
                        }
                    }
                }
            }

            return quote;
        }

        public static Quote FilterLength(Quote quote, bool fix)
        {
            if (quote.text.Length != quote.length)
            {
                Console.WriteLine($"Wrong length id {quote.id}, updated {quote.length} to {quote.text.Length}");
                if (PromptChange($"{quote.length}", $"{quote.text.Length}", " length "))
                {
                    quote.length = quote.text.Length;
                }
            }
            return quote;
        }

        public static bool PromptChange(string bef, string aft, string text)
        {
            Console.WriteLine($"> Ability to fix (Y/N/R): [{bef}] -> [{aft}]");
            string x = Console.ReadLine();
            if (x.ToUpper() == "Y")
            {
                Console.WriteLine($"> Changed [{bef}] -> [{aft}]");
                return true;
            }
            else if (x.ToUpper() == "R")
            {
                Console.WriteLine($"\nbegin{text}end\n");
                return PromptChange(bef, aft, text);
            }
            else if (x.ToUpper() == "N")
            {
                return false;
            }
            else
            {
                Console.WriteLine($"try a valid input");
                return PromptChange(bef, aft, text);
            }
        }
    }
}

