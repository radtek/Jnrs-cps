using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteJnrs.Models
{
    public class ProductInfo
    {
        public int product_id { get; set; }
        public string product_no { get; set; }
        public string product_name { get; set; }
        public string custom_no { get; set; }
        public string custom_name { get; set; }
        public string product_type { get; set; }
        public string product_ecode { get; set; }
        public string sheet_no { get; set; }
        public string unit { get; set; }
        public DateTime create_time { get; set; }
        public string cre_empid { get; set; }
        public string cre_empname { get; set; }
        public DateTime alter_time { get; set; }
        public string alt_content { get; set; }
        public string alt_empid { get; set; }
        public string alt_empname { get; set; }
        public DateTime machine_time { get; set; }
        public string mac_empid { get; set; }
        public string mac_empname { get; set; }
    }
}