
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

        public static string DispJSON(QuoteFile file)
        {
            return JsonConvert.SerializeObject(file, Formatting.Indented);
        }

        /*public static QuoteFile Verify(QuoteFile file, bool fix)
        {
            
            for(int i = 0; i < file.quotes.Count; i++)
            {
                Quote x = file.quotes[i];
                x = Filter.StdFilter(x, fix);
                x = Filter.StartEndFilter(x, fix);
                x = Filter.LengthFilter(x, fix);
                file.quotes[i] = x;
            }
            return file;
        }*/
    }
}

