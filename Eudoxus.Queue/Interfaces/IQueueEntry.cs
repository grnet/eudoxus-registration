using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.Queue
{
    public interface IQueueEntry
    {
        string QueueDataXml { get; set; }
        object ID { get; set; }
        object QueueDataID { get; set; }
        int? MaxNoOfRetries { get; set; }
        int NoOfRetries { get; set; }
    }

    public interface IQueueEntry<T> : IQueueEntry
    {
        T ID { get; set; }
        T QueueDataID { get; set; }
    }

}
