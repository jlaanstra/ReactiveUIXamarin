using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Coolness: You don't have to actually write this code at all, 
// just go to json2csharp.com or download JsonCSharpClassGenerator 
// from Codeplex. The latter has more flexibility and configuration 
// options.
namespace ReactiveUIXamarin.Core.Models
{
    public class Metadata
    {
        public string photoID { get; set; }
        public string userID { get; set; }
        public string imageHeight { get; set; }
        public string imageWidth { get; set; }
    }

    public class Result
    {
        public string score { get; set; }
        public Metadata metadata { get; set; }
        public string filepath { get; set; }
    }

    public class Results
    {
        public string count { get; set; }
        public string status { get; set; }
        public List<string> error { get; set; }
        public string method { get; set; }
        public List<Result> result { get; set; }
    }
}
