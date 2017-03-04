using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.BusinessModel
{
    public partial class Dispatch
    {
        public enDispatchType DispatchType
        {
            get { return (enDispatchType)DispatchTypeInt; }
            set
            {
                if (DispatchTypeInt != (int)value)
                    DispatchTypeInt = (int)value;
            }
        }
    }
}
