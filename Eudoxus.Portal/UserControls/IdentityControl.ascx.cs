using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Eudoxus.BusinessModel;

namespace Eudoxus.Portal.UserControls
{
    public partial class IdentityControl : System.Web.UI.UserControl, IScriptControl
    {
        protected void rblIdType_Init(object sender, EventArgs e)
        {
            bool isFirst = true;
            foreach (enIdentificationType item in Enum.GetValues(typeof(enIdentificationType)))
            {
                if (item == enIdentificationType.None)
                    continue;
                var li = new ListItem(item.GetLabel(), ((int)item).ToString());
                if (isFirst)
                {
                    li.Selected = true;
                    isFirst = false;
                }
                rblIdType.Items.Add(li);
            }
        }

        #region [ Databinding Methods ]

        public void FillIdDetails(IdentificationDetails idDetails)
        {
            idDetails.IdType = (enIdentificationType)int.Parse(rblIdType.SelectedItem.Value);
            idDetails.IdNumber = txtIdNumber.Text.ToNull();

            if (idDetails.IdType == enIdentificationType.ID)
            {
                idDetails.IdIssueDate = DateTime.Parse(txtIdIssueDate.Text);
                idDetails.IdIssuer = txtIdIssuer.Text.ToNull();
            }
            else
            {
                idDetails.IdIssueDate = null;
                idDetails.IdIssuer = null;
            }
        }

        protected void cvMaxDate_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime issueDate;
            var idType = (enIdentificationType)int.Parse(rblIdType.SelectedItem.Value);
            if (idType == enIdentificationType.ID)
            {
                if (DateTime.TryParse(txtIdIssueDate.Text, out issueDate))
                {
                    args.IsValid = DateTime.Now.Date > issueDate.Date;
                }
                else
                    args.IsValid = false;
            }
        }

        public void SetIdDetails(IdentificationDetails idDetails)
        {
            if (idDetails == null)
                return;
            txtIdNumber.Text = idDetails.IdNumber;
            txtIdIssuer.Text = idDetails.IdIssuer;
            rblIdType.SelectedValue = idDetails.IdType.ToString("D");
            if (idDetails.IdIssueDate.HasValue)
                txtIdIssueDate.Text = idDetails.IdIssueDate.Value.ToString("dd/MM/yyyy");
        }

        #endregion

        #region [ Required for Scripting ]

        protected override void OnPreRender(EventArgs e)
        {
            txtIdIssuer.Attributes["onkeyup"] = "Imis.Lib.ToUpper(this)";

            if (!ReadOnly)
            {
                cvIssuer.ClientValidationFunction = string.Format("$find('{0}').validateIssuer", tbIdentificationInfo.ClientID);
                cvNumber.ClientValidationFunction = string.Format("$find('{0}').validateNumber", tbIdentificationInfo.ClientID);
                cuvIssueDate.ClientValidationFunction = string.Format("$find('{0}').validateIssueDate", tbIdentificationInfo.ClientID);
            }
            if (ScriptManager.GetCurrent(Page) == null)
                throw new NullReferenceException("A ScriptManager control must exist on the Page");
            else
            {
                ScriptManager.GetCurrent(Page).RegisterScriptControl(this);
                ScriptManager.GetCurrent(Page).Scripts.Add(new ScriptReference("~/_js/jquery-ui.js"));
            }

            if (string.IsNullOrEmpty(ValidationGroup))
            {
                foreach (var v in Controls.OfType<BaseValidator>())
                    v.ValidationGroup = tbIdentificationInfo.ClientID + "_vgIdDetails";
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            ScriptManager.GetCurrent(Page).RegisterScriptDescriptors(this);
            base.Render(writer);
        }

        #endregion


        public string ValidationGroup
        {
            get { return cvIssuer.ValidationGroup; }
            set
            {
                foreach (var v in this.RecursiveOfType<BaseValidator>())
                {
                    v.ValidationGroup = value;
                }
            }
        }
        #region [ IScriptControl Members ]

        public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor scd = new ScriptControlDescriptor("Eudoxus.Portal.UserControls.IdentityControl", tbIdentificationInfo.ClientID);
            scd.AddElementProperty("rblist", rblIdType.ClientID);
            scd.AddProperty("txtIdIssuer", txtIdIssuer.ClientID);
            scd.AddProperty("txtIdIssueDate", txtIdIssueDate.ClientID);
            scd.AddProperty("txtIdNumber", txtIdNumber.ClientID);
            scd.AddProperty("lblIdNumber", lblIdNumber.ClientID);
            if (rblIdType.SelectedItem != null)
                scd.AddProperty("idType", rblIdType.SelectedItem.Value);

            yield return scd;
        }


        public IEnumerable<ScriptReference> GetScriptReferences()
        {
            yield return new ScriptReference("~/UserControls/IdentityControl.js");
        }


        #endregion


        public bool ReadOnly
        {
            private get { return !txtIdNumber.Enabled; }
            set
            {
                txtIdIssueDate.Enabled =
                txtIdNumber.Enabled =
                rblIdType.Enabled =
                txtIdIssuer.Enabled =
                lnkSelectDate.Enabled =
                ceSelectDate.Enabled = !value;
            }
        }

        public string ClientInstanceName { get { return tbIdentificationInfo.ClientID; } }
    }
}