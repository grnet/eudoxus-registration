using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Imis.Domain;

namespace Eudoxus.BusinessModel
{
    public class UnitOfWorkFactory
    {
        public static IUnitOfWork Create()
        {
            var ctx = new HelpDeskEntities();
            ctx.MetadataWorkspace.LoadFromAssembly(ctx.GetType().Assembly);
            return ctx;
        }
    }
}
