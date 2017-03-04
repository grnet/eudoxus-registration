using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eudoxus.eService
{
    public enum enEudoxusOsyStatusCode
    {
        OK = 1,
        Errors,
        UnexpectedError,

        SupplierCreated,
        SupplierUpdated,
        SupplierUsernameExists
    }
}
