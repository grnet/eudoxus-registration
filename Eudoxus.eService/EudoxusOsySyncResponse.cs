using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.eService
{
    public class EudoxusOsySyncResponse
    {   
        public bool Success { get; set; }        
        public int StatusCode { get; set; }        
        public string StatusMessage { get; set; }
    }
}
