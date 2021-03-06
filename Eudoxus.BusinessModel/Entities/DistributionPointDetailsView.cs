﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class DistributionPointDetailsView
    {
        public enVerificationStatus VerificationStatus
        {
            get { return (enVerificationStatus)VerificationStatusInt; }
            set
            {
                if (VerificationStatusInt != (int)value)
                    VerificationStatusInt = (int)value;
            }
        }

        public enDistributionPointType DistributionPointType
        {
            get { return (enDistributionPointType)DistributionPointTypeInt; }
            set
            {
                if (DistributionPointTypeInt != (int)value)
                    DistributionPointTypeInt = (int)value;
            }
        }
    }
}
