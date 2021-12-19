using System;
using System.Collections.Generic;
using System.Text;

namespace scrape
{
    class IndeedJob
    {
        public int IndeedJobID { get; set; }
        public int IndeedSearchID { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string CompanyLink { get; set; }
        public string Location { get; set; }
        public string JobLink { get; set; }
    }
}
