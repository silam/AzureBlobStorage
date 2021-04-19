using System;
using System.Collections.Generic;
using System.Text;

namespace cosmos_adddata
{
    class ActivityData
    {
        public string id { get; set; }
        public string Correlationid { get; set; }
        public string Operationname { get; set; }
        public string status { get; set; }
        // This is the partition key
        public string EventCategory { get; set; }
        public string Level { get; set; }
        public DateTime dttime { get; set; }

        public string subscription { get; set; }
        public string InitiatedBy { get; set; }

        public string resourcetype { get; set; }
        public string resourcegroup { get; set; }
        public string resource { get; set; }
    }
}
