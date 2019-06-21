using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Diagnostics;

namespace MachineDesign.Models
{
    public class ProductSchedual
    {
        public string machine_id { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public int run_state { get; set; }
        public int calc_date { get; set; }
        public int[] data1{ get; set; }
        public int[] data2 { get; set; }
        public int[] data3 { get; set; }
        public int[] data4 { get; set; }
        public int[] data5 { get; set; }
        public int[] data6 { get; set; }
        public int[] data7 { get; set; }
        public string[] str { get; set; }
    }
    public class PageProductSchedual
    {
        public static List<ProductSchedual> ListProdSch { get; set; }
    }
}