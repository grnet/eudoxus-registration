using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eudoxus.Queue;

namespace Eudoxus.BusinessModel
{
    public partial class QueueEntry : IQueueEntry<int>
    {
        #region IQueueEntry Members


        object IQueueEntry.ID
        {
            get { return ID; }
            set { }
        }

        object IQueueEntry.QueueDataID
        {
            get { return QueueDataID; }
            set { QueueDataID = (int)value; }
        }

        #endregion
    }
}
