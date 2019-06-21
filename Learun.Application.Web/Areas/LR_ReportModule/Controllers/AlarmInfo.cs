using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteJnrs.Models
{
    public class AlarmInfo
    {
        public int group_id { get; set; }
        public string group_name { get; set; }
        public int machine_id { get; set; }
        public string machine_name { get; set; }
        public string machine_series { get; set; }
        public string machine_number { get; set; }
        public int axis_num { get; set; }    //报警轴
        public int axis_no { get; set; }     //轴号
        public int alarm_no { get; set; }
        public string str_alarm_no { get; set; }
        public int alarm_group { get; set; }  //报警类别
        public int urge_info { get; set; }     //紧急程度:info 提示；warn 警告；merg 严重
        public int urge_warn { get; set; }
        public int urge_criti { get; set; }
        public string alarm_msg { get; set; }
        public int alarm_num { get; set; } //数量
        public int read_date { get; set; }
        public DateTime read_time { get; set; }
        public string str_read_time { get; set; }

        public AlarmInfo()
        {
            alarm_no = 0;
            read_time = DateTime.Now;
            alarm_msg = "";
        }
    }
}