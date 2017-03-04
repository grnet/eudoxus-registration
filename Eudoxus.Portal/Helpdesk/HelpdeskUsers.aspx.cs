using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;
using Imis.Domain;
using DevExpress.Web.ASPxGridView;
using Eudoxus.Portal.Controls;
using System.Web.Security;
using Eudoxus.Utils;

namespace Eudoxus.Portal.Helpdesk
{
    public partial class HelpdeskUsers : BasePortalPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cmdRefresh_Click(object sender, EventArgs e)
        {
            gvHelpdeskUsers.DataBind();
        }

        protected void odsHelpdeskUsers_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            Criteria<HelpdeskUser> criteria = new Criteria<HelpdeskUser>();

            e.InputParameters["criteria"] = criteria;
        }

        public void gvHelpdeskUsers_RowCommand(object sender, ASPxGridViewRowCommandEventArgs e)
        {
            int sID = Convert.ToInt32(e.CommandArgs.CommandArgument);

            using (IUnitOfWork unitOfWork = UnitOfWorkFactory.Create())
            {
                HelpdeskUser hUser = new HelpdeskUserRepository(unitOfWork).Load(sID);

                if (e.CommandArgs.CommandName == "DeleteHelpdeskUser")
                {
                    int evaluationCount = new IncidentReportPostRepository().GetAnsweredIncidentReportCount(hUser.UserName);

                    if (evaluationCount > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert_cannotdelete", string.Format("alert('{0}')", "Δεν μπορείτε να διαγράψετε το συγκεκριμένο χρήστη, γιατί έχει ήδη απαντήσει σε συμβάντα.\\n\\nΑν θέλετε ο χρήστης να μην έχει πλέον πρόσβαση στην εφαρμογή μπορείτε να τον απενεργοποιήσετε πατώντας το εικονίδιο με το λουκετάκι κάτω από τη στήλη \"Επεξεργασία Χρήστη\""), true);
                        return;
                    }
                    else
                    {
                        try
                        {
                            Roles.RemoveUserFromRoles(hUser.UserName, new string[] { RoleNames.Helpdesk });
                            unitOfWork.MarkAsDeleted(hUser);
                            unitOfWork.Commit();

                            Membership.DeleteUser(hUser.UserName);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.LogError<HelpdeskUsers>(ex, this, string.Format("Unable to delete HelpdeskUser with username: {0}", hUser.UserName));
                        }
                    }
                }
                else if (e.CommandArgs.CommandName == "LockHelpdeskUser")
                {
                    if (hUser.IsApproved)
                    {
                        try
                        {
                            MembershipUser mu = Membership.GetUser(hUser.UserName);
                            mu.IsApproved = false;
                            Membership.UpdateUser(mu);

                            hUser.IsApproved = false;
                            unitOfWork.Commit();
                        }
                        catch (Exception ex)
                        {
                            LogHelper.LogError<HelpdeskUsers>(ex, this, string.Format("Unable to lock HelpdeskUser with username: {0}", hUser.UserName));
                        }
                    }
                }
                else if (e.CommandArgs.CommandName == "UnLockHelpdeskUser")
                {
                    if (!hUser.IsApproved)
                    {
                        try
                        {
                            MembershipUser mu = Membership.GetUser(hUser.UserName);
                            mu.IsApproved = true;
                            Membership.UpdateUser(mu);

                            hUser.IsApproved = true;
                            unitOfWork.Commit();
                        }
                        catch (Exception ex)
                        {
                            LogHelper.LogError<HelpdeskUsers>(ex, this, string.Format("Unable to unlock HelpdeskUser with username: {0}", hUser.UserName));
                        }
                    }
                }

                var gridViewHelpdeskUser = gvHelpdeskUsers.GetRow(e.VisibleIndex) as HelpdeskUser;
                if (gridViewHelpdeskUser != null && hUser != null)
                    gridViewHelpdeskUser.IsApproved = hUser.IsApproved;
            }

            gvHelpdeskUsers.DataBind();
        }

        protected string GetAccountDetails(HelpdeskUser hUser)
        {
            if (hUser == null)
                return string.Empty;

            string accountDetails = string.Empty;

            accountDetails = string.Format("{0}<br/>{1}", hUser.UserName, hUser.Email);

            return accountDetails;
        }

        protected string GetContactDetails(HelpdeskUser helpdeskUser)
        {
            if (helpdeskUser == null)
                return string.Empty;

            string contactDetails = string.Empty;

            contactDetails = string.Format("{0}<br/>{1}", helpdeskUser.ContactName, helpdeskUser.ContactMobilePhone);

            return contactDetails;
        }

        protected string GetAnsweredIncidentReports(HelpdeskUser helpdeskUser)
        {
            if (helpdeskUser == null)
                return string.Empty;

            int evaluationCount = new IncidentReportPostRepository().GetAnsweredIncidentReportCount(helpdeskUser.UserName);

            return string.Format("{0:D}", evaluationCount);
        }
    }
}
