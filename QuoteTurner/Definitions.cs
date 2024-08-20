using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Func
{
    public class Quote
    {
        public string text { get; set; }
        public string source { get; set; }
        public int length { get; set; }
        public int id { get; set; }

        public Quote()
        {
            text = "none";
            source = "none";
            length = -1;
            id = -1;
        }

    }

    public class QuoteFile
    {
        public string language { get; set; }
        public int[,] groups { get; set; }
        public List<Quote> quotes { get; set; }
        public QuoteFile()
        {
            language = "none";
            int[,] defaultGroups = {
                    {0,100},
                    {101, 300 },
                    {301, 600 },
                    {601, 9999 }
            };
            groups = defaultGroups;
            quotes = new List<Quote>();
        }

    }

    public class FileFolder
    {
        public List<QuoteFile> files { get; set; }
        public FileFolder()
        {
            files = new List<QuoteFile>();
        }
    }
}
