using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace Eudoxus.BusinessModel
{
    public partial class IncidentReport : IUserChangeTracking
    {
        public enReportStatus ReportStatus
        {
            get { return (enReportStatus)ReportStatusInt; }
            set { ReportStatusInt = (int)value; }
        }

        public enReportSubmissionType SubmissionType
        {
            get { return (enReportSubmissionType)SubmissionTypeInt; }
            set { SubmissionTypeInt = (int)value; }
        }

        public enCallType CallType
        {
            get { return (enCallType)CallTypeInt; }
            set { CallTypeInt = (int)value; }
        }

        public enHandlerType HandlerType
        {
            get { return (enHandlerType)HandlerTypeInt; }
            set { HandlerTypeInt = (int)value; }
        }

        public enHandlerStatus HandlerStatus
        {
            get { return (enHandlerStatus)HandlerStatusInt; }
            set { HandlerStatusInt = (int)value; }
        }
    }
}
