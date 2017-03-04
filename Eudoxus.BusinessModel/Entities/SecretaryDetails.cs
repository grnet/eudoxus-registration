using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class SecretaryDetails
    {
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
