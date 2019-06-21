using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteJnrs.Models
{
    public class MachineSeries
    {
        public int group_id { get; set; }
        public string group_name { get; set; }
        public int machine_id { get; set; }
        public string machine_name { get; set; }
        public int series_id { get; set; }
        public string machine_series { get; set; }
        public string machine_number { get; set; }
        public string comm_protocol { get; set; }
        public string comm_interface { get; set; }
        public string category { get; set; }
    }
}