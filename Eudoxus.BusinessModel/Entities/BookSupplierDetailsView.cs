using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class BookSupplierDetailsView
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
    }
}
