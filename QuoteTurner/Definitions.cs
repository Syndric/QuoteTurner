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
    }

    public class QuoteFile
    {
        public string language { get; set; }
        public int[,] groups { get; set; }
        public List<Quote> quotes { get; set; }
        public QuoteFile()
        {
            quotes = new List<Quote>();
        }

    }
}
