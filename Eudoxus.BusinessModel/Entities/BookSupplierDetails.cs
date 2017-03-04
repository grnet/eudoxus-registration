using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class BookSupplierDetails
    {
        public enCertifierType CertifierType
        {
            get { return (enCertifierType)CertifierTypeInt; }
            set
            {
                if (CertifierTypeInt != (int)value)
                    CertifierTypeInt = (int)value;
            }
        }
    }
}
