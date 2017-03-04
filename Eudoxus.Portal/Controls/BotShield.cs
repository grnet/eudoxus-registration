using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lanap.BotDetect;

namespace Eudoxus.Portal.Controls
{
    [ToolboxData("<{0}:BotShield runat=server></{0}:BotShield>")]
    public class BotShield : WebControl, INamingContainer
    {
        private ITemplate contentTemplate;

        [TemplateContainer(typeof(BotShield))]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public ITemplate ContentTemplate
        {
            get { return contentTemplate; }
            set { contentTemplate = value; }
        }

        protected virtual ITemplate CreateDefaultTemplate()
        {
            return new LanapTemplate();
        }

        protected override void CreateChildControls()
        {
            Controls.Clear();
            ITemplate tmpl = contentTemplate != null ? contentTemplate : CreateDefaultTemplate();
            tmpl.InstantiateIn(this);
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            Captcha c = FindControl(SHIELD_ID) as Captcha;
            if (c != null)
            {
                Array a = Enum.GetValues(typeof(TextStyleEnum));
                Random r = new Random();
                int x = r.Next(a.Length);
                c.TextStyle = (TextStyleEnum)a.GetValue(x);
            }
            if (this.Page != null && !this.Page.ClientScript.IsClientScriptIncludeRegistered(this.GetType(), SCRIPT_KEY))
            {
                this.Page.ClientScript.RegisterClientScriptInclude(this.GetType(), SCRIPT_KEY, this.Page.ClientScript.GetWebResourceUrl(this.GetType(), "Eudoxus.Portal.Controls.BotShield.js"));

                if (c != null)
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), SCRIPT_KEY + "1", string.Format("var BS_MAX_LENGTH = {0}; var DEFAULT_BS_ERROR_MESSAGE = '{1}';", c.CodeLength, DEFAULT_ERROR_MESSAGE), true);
            }


            if (Page.IsPostBack)
            {
                BaseValidator val = FindControl(VALIDATOR_ID) as BaseValidator;
                if (!val.IsValid)
                {
                    //ForceDraw();
                    ITextControl txt = FindControl(TEXT_ID) as ITextControl;
                    txt.Text = "";
                }
            }
        }

        public string ValidationGroup
        {
            get
            {
                return (FindControl(VALIDATOR_ID) as BaseValidator).ValidationGroup;
            }
            set { (FindControl(VALIDATOR_ID) as BaseValidator).ValidationGroup = value; }
        }

        public const string TEXT_ID = "txtFormShield";
        public const string SHIELD_ID = "formShield";
        public const string VALIDATOR_ID = "valFormShield";

        public const string SCRIPT_FUNCTION = "ChangeBotShieldInput";
        public const string DEFAULT_ERROR_MESSAGE = "<b>Παρακαλώ συμπληρώστε το κείμενο που εμφανίζεται στην εικόνα</b>";
        public const string SCRIPT_VALIDATION_FUNCTION = "ValidateBotShieldInput";
        public const string SCRIPT_KEY = "Eudoxus.Portal.Controls.ChangeBotShieldInput";
    }

    sealed class LanapTemplate : ITemplate
    {
        private Literal lt = new Literal();
        private Table tbl = new Table();
        private Captcha captcha = new Captcha();
        private TextBox txtFormShield = new TextBox();
        private CustomValidator valFormShield = new CustomValidator();

        internal LanapTemplate()
        {
            lt.Text = "<p><b>Γράψτε τους χαρακτήρες που εμφανίζονται στην εικόνα</b></p>";

            TableRow tr;
            TableCell tc;

            tbl.Rows.Add(tr = new TableRow());

            tr.Cells.Add(tc = new TableCell());
            captcha.ID = BotShield.SHIELD_ID;
            captcha.SoundEnabled = false;
            captcha.ReloadEnabled = true;
            captcha.ReloadIconAltText = "Αλλαγή εικόνας";
            tc.Controls.Add(captcha);

            tr.Cells.Add(tc = new TableCell());
            txtFormShield.ID = BotShield.TEXT_ID;
            txtFormShield.Attributes["onkeyup"] = BotShield.SCRIPT_FUNCTION + "(this)";
            tc.Controls.Add(txtFormShield);

            tbl.Rows.Add(tr = new TableRow());
            tr.Cells.Add(tc = new TableCell());
            tc.ColumnSpan = 2;
            valFormShield.ID = BotShield.VALIDATOR_ID;
            valFormShield.ErrorMessage = BotShield.DEFAULT_ERROR_MESSAGE;
            valFormShield.ClientValidationFunction = BotShield.SCRIPT_VALIDATION_FUNCTION;
            valFormShield.ControlToValidate = txtFormShield.ClientID;
            valFormShield.SetFocusOnError = false;
            valFormShield.ValidateEmptyText = true;
            valFormShield.ServerValidate += delegate(object sender, ServerValidateEventArgs e)
            {
                Control ctl = ((Control)sender).Parent;
                e.IsValid = ((Captcha)ctl.FindControl(BotShield.SHIELD_ID)).Validate(
                    ((TextBox)ctl.FindControl(BotShield.TEXT_ID)).Text);
            };

            tc.Controls.Add(valFormShield);
        }

        public void InstantiateIn(Control container)
        {
            container.Controls.Add(lt);
            container.Controls.Add(tbl);
        }
    }
}
