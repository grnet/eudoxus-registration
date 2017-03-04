using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Eudoxus.BusinessModel
{
    public class GenericQueueDataCollection : List<GenericQueueData>
    {

    }

    public class GenericQueueData
    {
        public int NoOfRetry { get; set; }
        public string Message { get; set; }
        public string ServerName { get; set; }
        public enReporterType ReporterType { get; set; }
    }
}
