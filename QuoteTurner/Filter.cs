using System;
namespace Func
{
    public class Filter
    {
        public static void StdFilter(Quote quote, bool fix)
        {
            string[,] invalids = {
                { "\'\'", "" },
                { "\"\"", "" },
                { "‎‎ ", "" },
                { "“", "\""},
                { "”", "\""},
                { "—", "-"},
                { "–", "-"},
                { "‘", "\'"},
                { "’", "\'"},
                { "\n", "" },
                { "…", "..." }
            };
            string[] inv_corr = { "", "", " ", "\"", "\"", "-", "-", "\'", "\'" };
            char[] valids = {
                ' ', '!', '#', '$', '%', '&', '*', '(', ')', '-', '_',
                '[', ']', '{', '}', '/', '.', ',', ';', ':', '\'', '\"',
                '?' , '+', '='
            };
            for(int i = 0; i < invalids.GetLength(0); i++)
            {
                if (quote.text.Contains(invalids[i,0]))
                {
                    Console.WriteLine($"Invalid str found id {quote.id}: [{invalids[i,0]}]");
                    if(fix)
                    {
                        if (PromptChange(invalids[i, 0], invalids[i, 1], quote))
                        {
                            quote.text.Replace(invalids[i, 0], invalids[i, 1]);
                        }
                    }
                    return;
                }
            }
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
        }

        public static void StartEndFilter(Quote quote, bool fix)
        {
            if (quote.text.Substring(quote.text.Length - 1, 1) == " ")
            {
                Console.WriteLine($"Space ending text id {quote.id}");
            }
            if (quote.text.Substring(0, 1) == " ")
            {
                Console.WriteLine($"Space begining text id {quote.id}");
            }
            if (quote.source.Substring(quote.source.Length - 1, 1) == " ")
            {
                Console.WriteLine($"Space ending source id {quote.id}");
            }
            if (quote.source.Substring(0, 1) == " ")
            {
                Console.WriteLine($"Space begining source id {quote.id}");
            }
        }

        public static void LengthFilter(Quote quote, bool fix)
        {
            if (quote.text.Length != quote.length)
            {
                Console.WriteLine($"Wrong length id {quote.id}, updated {quote.length} to {quote.text.Length}");
            }
        }

        public static bool PromptChange(string bef, string aft, Quote quote)
        {
            Console.WriteLine($"> Ability to fix (Y/N/R): [{bef}] -> [{aft}]");
            string x = Console.ReadLine();
            if (x.ToUpper() == "Y")
            {
                Console.WriteLine($"> Changed [{bef}] -> [{aft}]");
                return true;
            }
            else if(x.ToUpper() == "R")
            {
                Console.WriteLine($"begin{quote.text}end");
                return PromptChange(bef, aft, quote);
            }
            else
            {
                return false;
            }
        }
    }
}

