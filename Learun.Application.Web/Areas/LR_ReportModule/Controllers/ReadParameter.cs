using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using System.Web.Script.Serialization;

namespace SiteJnrs.Models
{
    public class ReadParameter
    {
        ILog log = LogManager.GetLogger("ReadParameter");

        protected static int ByteLen = 8;
        protected static int DataLen = 16;
        protected static int DWordLen = 32;
        protected static int OneByte = 1;
        protected static int TwoByte = 2;
        protected static int FouByte = 4;

        public static string SeriesParamType = "'ProdData','RunStateData','DeviceData','AlarmData','RunParamData','ProgramData'";
        public static string SeriesParamPrwType = "'ReadOut','InOut'";
        public static string ToOutParamPrwType = "ToOut";
        public static string ReadOutParamPrwType = "ReadOut";
        public static string InOutParamPrwType = "InOut";

        public static string ProductNo_065J = "0BH.325.065J";
        public static string ProductNo_066F = "0BH.325.066F";
        public static string ProductNo_066B = "0DW.325.066B";

        public static int PlanProdNum = 1000;
        public static int PlanProdFlag = 0;
        public static int PassProdNum = 0;
        public static string DeviceSXLProd = "SXL";
        public static string DeviceSLProd = "SL";
        public static string DeviceXLProd = "XL";
        public static int WShiftOffsetHour = -8;
        public static int WShiftOffsetMinute = 0;

        public static string MisNumvALine = "'V_A'";
        public static string MisNumvBLine = "'V_B'";
        public static string MisNumvCLine = "'V_C'";
        public static string MisNumvDLine = "'V_D'";
        public static string MisNumvELine = "'V_E'";
        public static string MisNumvFLine = "'V_F'";
        public static string MisNumvGLine = "'V_G'";
        public static string MisNumvHLine = "'V_H'";
        public static string MisNumvILine = "'V_I'";
        public static string MisNumvJLine = "'V_J'";
        public static string MisNumvKLine = "'V_K'";
        public static string MisNumvLLine = "'V_L'";
        public static string MisNumvMLine = "'V_M'";
        public static string MisNumvNLine = "'V_N'";

        public static string GisNumvALine = "V_A";
        public static string GisNumvBLine = "V_B";
        public static string GisNumvCLine = "V_C";
        public static string GisNumvDLine = "V_D";
        public static string GisNumvELine = "V_E";
        public static string GisNumvFLine = "V_F";
        public static string GisNumvGLine = "V_G";
        public static string GisNumvHLine = "V_H";
        public static string GisNumvILine = "V_I";
        public static string GisNumvJLine = "V_J";
        public static string GisNumvKLine = "V_K";
        public static string GisNumvLLine = "V_L";
        public static string GisNumvMLine = "V_M";
        public static string GisNumvNLine = "V_N";

        private int _MachineId;
        private int _ParamId;
        private string _ParamName;
        private string _ParamType;
        private string _PRWType;
        private string _PFieldsName;
        private string _PFieldsType;
        private int _PFieldsLen;
        private string _PDataAddr;
        private object _PDataSetup;
        private string _PDataType;
        private int _PDataLen;
        private bool _PDataIsVisual;
        private bool _PDataIsHot;

        public int MachineId
        {
            get
            {
                return _MachineId;
            }
            set
            {
                _MachineId = value;
            }
        }
        public int ParamId
        {
            get
            {
                return _ParamId;
            }
            set
            {
                _ParamId = value;
            }
        }
        public string ParamName
        {
            get
            {
                return _ParamName;
            }
            set
            {
                _ParamName = value;
            }
        }
        public string ParamType
        {
            get
            {
                return _ParamType;
            }
            set
            {
                _ParamType = value;
            }
        }

        public string PRWType
        {
            get
            {
                return _PRWType;
            }
            set
            {
                _PRWType = value;
            }
        }
        public string PFieldsName
        {
            get
            {
                return _PFieldsName;
            }
            set
            {
                _PFieldsName = value;
            }
        }
        public string PFieldsType
        {
            get
            {
                return _PFieldsType;
            }
            set
            {
                _PFieldsType = value;
            }
        }
        public int PFieldsLen
        {
            get
            {
                return _PFieldsLen;
            }
            set
            {
                _PFieldsLen = value;
            }
        }
        public string PDataAddr
        {
            get
            {
                return _PDataAddr;
            }
            set
            {
                _PDataAddr = value;
            }
        }
        public object PDataSetup
        {
            get
            {
                return _PDataSetup;
            }
            set
            {
                _PDataSetup = value;
            }
        }
        public string PDataType
        {
            get
            {
                return _PDataType;
            }
            set
            {
                _PDataType = value;
            }
        }
        public int PDataLen
        {
            get
            {
                return _PDataLen;
            }
            set
            {
                _PDataLen = value;
            }
        }
        public bool PDataIsVisual
        {
            get
            {
                return _PDataIsVisual;
            }
            set
            {
                _PDataIsVisual = value;
            }
        }
        public bool PDataIsHot
        {
            get
            {
                return _PDataIsHot;
            }
            set
            {
                _PDataIsHot = value;
            }
        }
    }
}