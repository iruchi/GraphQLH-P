using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandPApis_GraphQL.Model
{
    public class TestClass
    {
        public TestClass(DateTime start, string type, string location)
        {
            Start = start.ToString("ddd") +" "+ start.ToShortDateString();
            Type = type;
            Location = location;
        }

        public override string ToString()
        {
            return String.Format("{0} {1} {2}", Location, Type, Start);
        }

        public string Start { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
    }
}
