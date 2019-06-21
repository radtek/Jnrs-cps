using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteJnrs.Models
{
    public class UsedTimeSeque
    {
        public int group_id { get; set; }
        public int machine_id { get; set; }
        public string group_name { get; set; }         //设备组
        public string machine_name { get; set; }       //机器名称
        public string machine_series { get; set; }     //机器系列
        public string machine_number { get; set; }     //机器编号
        public string sets_no { get; set; }
        public List<TimeSeque> litime_seq { get; set; }
        public List<DateTime> liread_time { get; set; }          //最后采集时间
    }
    public class TimeSeque
    {
        public int wshift_id { get; set; }
        public int run_state { get; set; }
        public string run_class { get; set; }
        public int run_duration { get; set; }
        public double dur_rate { get; set; }
        public int last_run_state { get; set; }
        public int next_run_state { get; set; }
        public DateTime state_start_time { get; set; }
        public DateTime state_end_time { get; set; }
        public string read_short_time { get; set; }
        public string read_long_time { get; set; }
        public string read_univer_time { get; set; }
        public string read_formf_time { get; set; }
        public DateTime read_time { get; set; }          //最后采集时间
    }
    public class SecTimeSeque
    {
        public int id { get; set; }
        public string calc_date { get; set; }
        public int group_id { get; set; }
        public int machine_id { get; set; }
        public string group_name { get; set; }
        public string machine_name { get; set; }
        public string machine_series { get; set; }
        public string machine_number { get; set; }
        public double run_duration { get; set; }
        public double alarm_duration { get; set; }
        public double pause_duration { get; set; }
        public double ready_duration { get; set; }
        public double edit_duration { get; set; }
        public double stop_duration { get; set; }
        public double dur_length { get; set; }
        public double run_rate { get; set; }
        public double alarm_rate { get; set; }
        public double pause_rate { get; set; }
        public double ready_rate { get; set; }
        public double edit_rate { get; set; }
        public double stop_rate { get; set; }
        public DateTime read_time { get; set; }
    }
    public class SecTimeSeqSource
    {
        public SecTimeSeque stsSource { get; set; }
        public List<SecTimeSeque> lstsSource { get; set; }
    }
}