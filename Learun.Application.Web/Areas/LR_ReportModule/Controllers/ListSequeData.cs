using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteJnrs.Models
{
    public class ListSequeData
    {
        public RunCountData stateNum { get; set; }
        public SecTimeSeque totDurSeque { get; set; }
        public List<SecTimeSeque> listSecDurSeque { get; set; }
        public List<UsedTimeSeque> listUsedTimeSeque { get; set; }
        public int DurLength { get; set; }
    }
}