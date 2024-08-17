
using CsvHelper;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Func
{
    public static class OpFunc
    {
        public static QuoteFile LoadJSON(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                QuoteFile file = JsonConvert.DeserializeObject<QuoteFile>(json);
                return file;
            }
        }

        public static List<Quote> LoadCSV(string path)
        {
            using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture))
            {
                List<Quote> quotes = csv.GetRecords<Quote>().ToList();
                return quotes;
            }
        }

        public static string WriteJSON(QuoteFile file)
        {
            string output = JsonConvert.SerializeObject(file, Formatting.Indented);
            File.WriteAllText($"output/{file.language}.json", output);
            return output;
        }

        public static void DispJSON(QuoteFile file)
        {
            Console.WriteLine(JsonConvert.SerializeObject(file, Formatting.Indented));
        }

        public static void Verify(QuoteFile file, bool fix)
        {
            
            foreach(Quote quote in file.quotes)
            {
                Filter.StdFilter(quote, fix);
                Filter.StartEndFilter(quote, fix);
                Filter.LengthFilter(quote, fix);
            }
        }

        public static void Help()
        {
            string[] msg = {
                "> load <path> : load json or csv file into memory",
                "> write : write memory to output",
                "> clear : clear memory",
                "> verify <0 or 1> : verify memory",
                "> disp: display memory"
            };
            foreach(string x in msg)
            {
                Console.WriteLine(x);
            }
        }
    }
}

