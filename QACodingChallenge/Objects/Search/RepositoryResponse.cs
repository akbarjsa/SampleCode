using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QACodingChallenge.Objects.Search
{
    public class ExpectedRepoResponse
    {
        public static int OverResultsCount { get; set; }
        public static int ItemsCount { get; set; }
        public static string Language { get; set; }
        public static int Stars { get; set; }
        public static bool Fork { get; set; }
    }
}
