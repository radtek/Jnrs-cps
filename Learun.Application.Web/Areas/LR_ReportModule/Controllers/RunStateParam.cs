using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MachineDesign.Models;

namespace SiteJnrs.Models
{
    public class RunStateParam
    {
       // public static log4net.ILog log = log4net.LogManager.GetLogger("RunStateParam");
        private static ReadData rd = new ReadData();

        private static int _StopState = 0;
        private static int _RunState = 0;
        private static int _AlarmState = 0;
        private static int _PauseState = 0;
        private static int _ReadyState = 0;
        private static int _EditState = 0;
        private static int _ExcpState = 0;
        private static int _ReadState = 0;
        public static bool NotEmergState = false;
        public static bool EmergState = true;
        public static int OnState = 1;
        public static int OffState = 0;

        public static bool readInitRunState()
        {
            if(rd.readInitRunState()){
                return true;
            }
            return false;
        }

        public static int StopState
        {
            get
            {
                return _StopState;
            }
            set
            {
                _StopState = value;
            }
        }

        public static int RunState
        {
            get
            {
                return _RunState;
            }
            set
            {
                _RunState = value;
            }
        }

        public static int AlarmState
        {
            get
            {
                return _AlarmState;
            }
            set
            {
                _AlarmState = value;
            }
        }

        public static int PauseState
        {
            get
            {
                return _PauseState;
            }
            set
            {
                _PauseState = value;
            }
        }

        public static int ReadyState
        {
            get
            {
                return _ReadyState;
            }
            set
            {
                _ReadyState = value;
            }
        }

        public static int EditState
        {
            get
            {
                return _EditState;
            }
            set
            {
                _EditState = value;
            }
        }

        public static int ExcpState
        {
            get
            {
                return _ExcpState;
            }
            set
            {
                _ExcpState = value;
            }
        }

        public static int ReadState
        {
            get
            {
                return _ReadState;
            }
            set
            {
                _ReadState = value;
            }
        }
    }
}