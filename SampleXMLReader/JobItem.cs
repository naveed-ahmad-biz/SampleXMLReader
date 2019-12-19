using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleXMLReader
{
    public class JobItem
    {
        public string Title { get; set; }

        public string Guid { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public DateTime PubDate { get; set; }

        public string JobPosition { get; set; }

        public string JobCompany { get; set; }

        public string JobLocation { get; set; }
    }
}
