using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MachineDesign.Models;

namespace SiteJnrs.Models
{
    /*机器实时信息*/
    public class MachineInfo
    {
        //log4net.ILog log = log4net.LogManager.GetLogger("MachineInfo");
        ReadData rd = new ReadData();

        public int calc_date { get; set; }
        public int day_time { get; set; }
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
        public int rank_num { get; set; }
        public string sets_no { get; set; }
        public string is_main { get; set; }
        public string machine_ip { get; set; }
        public ushort machine_port { get; set; }
        public string machine_addr { get; set; }
        public int scan_interval { get; set; }
        public int outtime { get; set; }
        public bool ip_enabled { get; set; }
        public bool connect_state { get; set; }
        public string memo { get; set; }

        public int run_state { get; set; }
        public string run_class { get; set; }
        public int prod_num { get; set; }
        public int plan_prod_num { get; set; }
        public int pass_prod_num { get; set; }
        public int tot_prod_num { get; set; }
        public double prod_rate { get; set; }
        public int run_duration { get; set; }
        public double dur_rate { get; set; }
        public int last_run_state { get; set; }
        public DateTime state_start_time { get; set; }
        public int wshift_id { get; set; }
        public string wshift_date { get; set; }
        public string wshift_name { get; set; }
        public int second_num { get; set; }
        public string read_short_time { get; set; }
        public string read_long_time { get; set; }
        public string read_univer_time { get; set; }
        public string read_formf_time { get; set; }
        public DateTime read_time { get; set; }

        protected static int _RunBit = 0;
        protected static int _AlarmBit = 0;
        protected static int _PauseBit = 0;
        protected static int _StopBit = 0;
        protected static int _ReadyBit = 0;
        protected static int _EditBit = 0;

        public List<MachineInfo> initMachineInfo(string comm_protocol, string comm_interface)
        {
            List<MachineInfo> lmi = rd.readMachineInfo(comm_protocol, comm_interface);
            
            return lmi;
        }
        /*
        public void initMachineStateBit(MachineInfo mi)
        {
            rd.readMachineStateBit(mi);
        }

        public void initMachineStateBit(List<MachineInfo> lmi)
        {
            rd.readMachineStateBit(lmi);
        }

        public void initMachineParamInfo(List<MachineInfo> lmi)
        {
            rd.readParamAddrInfo(lmi);
        }
        */
        public int RunBit
        {
            get
            {
                return _RunBit;
            }
            set
            {
                _RunBit = value;
            }
        }

        public int AlarmBit
        {
            get
            {
                return _AlarmBit;
            }
            set
            {
                _AlarmBit = value;
            }
        }

        public int PauseBit
        {
            get
            {
                return _PauseBit;
            }
            set
            {
                _PauseBit = value;
            }
        }

        public int StopBit
        {
            get
            {
                return _StopBit;
            }
            set
            {
                _StopBit = value;
            }
        }

        public int ReadyBit
        {
            get
            {
                return _ReadyBit;
            }
            set
            {
                _ReadyBit = value;
            }
        }

        public int EditBit
        {
            get
            {
                return _EditBit;
            }
            set
            {
                _EditBit = value;
            }
        }

    }
    /*状态数量*/
    public class RunStateNum
    {
        public int run_state { get; set; }
        public int run_num { get; set; }
        public string run_type { get; set; }
    }
    /*自动线条数*/
    public class DeviceLineNum
    {
        public int group_id { get; set; }
        public string group_name { get; set; }
        public string sets_no { get; set; }
    }
    /*机器实时运行信息*/
    public class DeviceRealState
    {
        public int group_id { get; set; }
        public int machine_id { get; set; }
        public string group_name { get; set; }         //设备组
        public string machine_name { get; set; }       //机器名称
        public string machine_series { get; set; }     //机器系列
        public string machine_number { get; set; }     //机器编号
        public string sets_no { get; set; }            //机器分组
        public int rank_num { get; set; }              //机器序号
        public int run_state { get; set; }
        public string run_class { get; set; }
        public string pic_name { get; set; }
        public string pic_path { get; set; }           //状态图片
        public int pic_height { get; set; }            //状态图片高度
        public int pic_width { get; set; }             //状态图片宽度
        public int pic_top { get; set; }               //状态图片上偏移
        public int pic_bottom { get; set; }            //状态图片下偏移
        public int pic_left { get; set; }              //状态图片左偏移
        public int pic_right { get; set; }             //状态图片右偏移
        public string main_prog_num { get; set; }      //主程序号
        public string running_prog_num { get; set; }   //程序行号
        public double act_spindle_speed_0 { get; set; }     //主轴转速
        public double act_spindle_override_0 { get; set; }  //主轴倍率
        public double act_feed_speed_0 { get; set; }        //进给速度
        public double act_feed_override_0 { get; set; }     //进给倍率
        public double act_axis_0 { get; set; }              //位置
        public double act_spindle_speed_1 { get; set; }     //主轴转速
        public double act_spindle_override_1 { get; set; }  //主轴倍率
        public double act_feed_speed_1 { get; set; }        //进给速度
        public double act_feed_override_1 { get; set; }     //进给倍率
        public double act_axis_1 { get; set; }              //位置
        public double act_spindle_speed_2 { get; set; }     //主轴转速
        public double act_spindle_override_2 { get; set; }  //主轴倍率
        public double act_feed_speed_2 { get; set; }        //进给速度
        public double act_feed_override_2 { get; set; }     //进给倍率
        public double act_axis_2 { get; set; }              //位置
        public double sp_load_0 { get; set; }          //主轴负载 A
        public double sp_load_1 { get; set; }          //主轴负载 B
        public double sp_load_2 { get; set; }          //主轴负载 C
        public double sv_load_0 { get; set; }          //伺服负载 A
        public double sv_load_1 { get; set; }          //伺服负载 B
        public double sv_load_2 { get; set; }          //伺服负载 C
        public int tool_no { get; set; }                        //刀具号
        public double len_offset { get; set; }                  //刀具长度偏置
        public double len_wear_compensate { get; set; }         //刀具长度磨损补偿
        public double diameter_compensate { get; set; }         //刀具直径补偿
        public double diameter_wear_compensate { get; set; }    //刀具直径磨损补偿
        public int initial_life { get; set; }                   //初期寿命
        public int life_prediction { get; set; }                //寿命预告
        public int tool_life { get; set; }                      //刀具寿命
        public string read_short_date { get; set; }          //最后采集日期
        public string read_short_time { get; set; }          //最后采集时间
        public string read_time { get; set; }          //最后采集时间
    }
}