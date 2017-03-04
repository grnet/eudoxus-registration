using System;
using Eudoxus.BusinessModel;
using System.IO;

using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using Eudoxus.Portal.Controls;
using System.Web.Security;
using Microsoft.Data.Extensions;

namespace Eudoxus.Portal.Secure.Secretaries
{
    public partial class PrintSecretaryCertification : BaseEntityPortalPage<Secretary>
    {
        protected override void Fill()
        {
            Entity = new SecretaryRepository(UnitOfWork).FindByUsername(AppUser.Username);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity == null)
            {
                UI.Message("SecretaryNotFound");
                return;
            }

            string filename = "Secretary-" + Entity.CertificationNumber + "-Certification.pdf";
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.ContentType = "application/octet-stream";

            CreatePDF();

            Response.End();
        }

        private void CreatePDF()
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Server.MapPath("~/_rdlc/SecretaryCertification.rdlc");

            MembershipUser user = Membership.GetUser(Entity.UserName);
            Eudoxus.BusinessModel.SecretaryDetails secretaryDetails = Entity.SecretaryDetails;
            var academic = CacheManager.Academics.Get((int)Entity.AcademicReference.GetKey());
            ReportParameter p1 = new ReportParameter("CertificationNumber", string.Format("Αριθμός Βεβαίωσης: {0} / {1:dd-MM-yyyy}", Entity.CertificationNumber, Entity.CertificationDate));
            ReportParameter p2 = new ReportParameter("Institution", academic.Institution);
            ReportParameter p3 = new ReportParameter("School", academic.School != null ? academic.School : "-");
            ReportParameter p4 = new ReportParameter("Department", academic.Department != null ? academic.Department : "-");
            ReportParameter p5 = new ReportParameter("RepresentativeType", ((int)secretaryDetails.RepresentativeType).ToString());
            ReportParameter p6 = new ReportParameter("RepresentativeName", secretaryDetails.RepresentativeName);
            ReportParameter p7 = new ReportParameter("UserName", user.UserName);

            ReportParameter p8;
            if (secretaryDetails.AlternateContactName != null)
            {
                p8 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, το Τμήμα εκπροσωπεί ο/η <{0}>  με e-mail <{1}> και τηλέφωνο <{2}>, τον/την οποίο/οποία αναπληρώνει ο/η <{3}> με e-mail <{4}> και τηλέφωνο <{5}>.", Entity.ContactName, Entity.ContactEmail, Entity.ContactPhone, secretaryDetails.AlternateContactName, secretaryDetails.AlternateContactEmail, secretaryDetails.AlternateContactPhone));
            }
            else
            {
                p8 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, το Τμήμα εκπροσωπεί ο/η <{0}>  με e-mail <{1}> και τηλέφωνο <{2}>.", Entity.ContactName, Entity.ContactEmail, Entity.ContactPhone));
            }

            lr.DataSources.Add(new ReportDataSource("Secretary", new Secretary[] { new Secretary() }));

            lr.SetParameters(new List<ReportParameter> { p1, p2, p3, p4, p5, p6, p7, p8 });

            string deviceInfo = @"<DeviceInfo>
            <OutputFormat>PDF</OutputFormat>
            <PageWidth>21cm</PageWidth>
            <PageHeight>29.7cm</PageHeight>
            <MarginTop>0.5in</MarginTop>
            <MarginLeft>0.0in</MarginLeft>
            <MarginRight>0.0in</MarginRight>
            <MarginBottom>0.5in</MarginBottom>
            </DeviceInfo>";
            var reportBytes = lr.Render("PDF", deviceInfo);
            Response.BinaryWrite(reportBytes);
        }
    }
}
