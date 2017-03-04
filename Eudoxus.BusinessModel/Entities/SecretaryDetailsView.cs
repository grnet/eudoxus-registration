using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class SecretaryDetailsView
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

        public enRepresentativeType RepresentativeType
        {
            get { return (enRepresentativeType)RepresentativeTypeInt; }
            set
            {
                if (RepresentativeTypeInt != (int)value)
                    RepresentativeTypeInt = (int)value;
            }
        }
    }
}
