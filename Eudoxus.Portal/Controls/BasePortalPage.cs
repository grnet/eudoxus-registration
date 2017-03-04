using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eudoxus.BusinessModel;
using Imis.Domain;
using Imis.Web.Controls;
using log4net;
using System.Web.Security;
using System.Web.UI;

namespace Eudoxus.Portal.Controls
{
    public class BasePortalPage<TMaster> : BasePortalPage where TMaster : MasterPage
    {
        public new TMaster Master { get { return (TMaster)base.Master; } }
    }

    public class BaseEntityPortalPage<T> : BasePortalPage
    {
        protected virtual void Fill() { }

        protected override void OnPreInit(EventArgs e)
        {
            Fill();
            base.OnPreInit(e);
        }

        IUnitOfWork _UnitOfWork = null;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_UnitOfWork == null)
                    _UnitOfWork = UnitOfWorkFactory.Create(); ;
                return _UnitOfWork;
            }
            set
            {
                _UnitOfWork = value;
            }
        }

        public T Entity { get; set; }
    }

    public class BaseEntityPortalPage<T, TMaster> : BaseEntityPortalPage<T> where TMaster : MasterPage
    {
        public new TMaster Master { get { return (TMaster)base.Master; } }
    }

    public class BasePortalPage : BasePage
    {
        public new RolePrincipal User
        {
            get { return base.User as RolePrincipal; }
        }

        private ApplicationUser _appUser;
        public ApplicationUser AppUser
        {
            get
            {
                if (!User.Identity.IsAuthenticated)
                    return null;

                if (_appUser == null)
                    _appUser = new ApplicationUser(User.Identity.Name);

                return _appUser;
            }
            set { _appUser = value; }
        }

        public BasePortalPage()
        {

        }
    }
}