using System;
using Eudoxus.BusinessModel;
using System.IO;

using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using Eudoxus.Portal.Controls;
using System.Web.Security;
using Microsoft.Data.Extensions;

namespace Eudoxus.Portal.Secure.PublicationsOffices
{
    public partial class PrintPublicationsOfficeCertification : BaseEntityPortalPage<PublicationsOffice>
    {
        protected override void Fill()
        {
            Entity = new PublicationsOfficeRepository(UnitOfWork).FindByUsername(AppUser.Username);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity == null)
            {
                UI.Message("PublicationsOfficeNotFound");
                return;
            }

            string filename = "PublicationsOffice-" + Entity.CertificationNumber + "-Certification.pdf";
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.ContentType = "application/octet-stream";

            CreatePDF();

            Response.End();
        }

        private void CreatePDF()
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Server.MapPath("~/_rdlc/PublicationsOfficeCertification.rdlc");

            MembershipUser user = Membership.GetUser(Entity.UserName);
            Eudoxus.BusinessModel.PublicationsOfficeDetails publicationsOfficeDetails = Entity.PublicationsOfficeDetails;
            var institution = CacheManager.Institutions.Get((int)Entity.InstitutionReference.GetKey());
            ReportParameter p1 = new ReportParameter("CertificationNumber", string.Format("Αριθμός Βεβαίωσης: {0} / {1:dd-MM-yyyy}", Entity.CertificationNumber, Entity.CertificationDate));
            ReportParameter p2 = new ReportParameter("Institution", institution.Name);
            ReportParameter p3 = new ReportParameter("DirectorName", publicationsOfficeDetails.DirectorName);
            ReportParameter p4 = new ReportParameter("UserName", user.UserName);

            ReportParameter p5;
            if (publicationsOfficeDetails.AlternateContactName != null)
            {
                p5 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, το Γραφείο Διδακτικών Συγγραμμάτων εκπροσωπεί ο/η <{0}>  με e-mail <{1}> και τηλέφωνο <{2}>, τον/την οποίο/οποία αναπληρώνει ο/η <{3}> με e-mail <{4}> και τηλέφωνο <{5}>.", Entity.ContactName, Entity.ContactEmail, Entity.ContactPhone, publicationsOfficeDetails.AlternateContactName, publicationsOfficeDetails.AlternateContactEmail, publicationsOfficeDetails.AlternateContactPhone));
            }
            else
            {
                p5 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, το Γραφείο Διδακτικών Συγγραμμάτων εκπροσωπεί ο/η <{0}>  με e-mail <{1}> και τηλέφωνο <{2}>.", Entity.ContactName, Entity.ContactEmail, Entity.ContactPhone));
            }

            lr.DataSources.Add(new ReportDataSource("PublicationsOffice", new PublicationsOffice[] { new PublicationsOffice() }));

            lr.SetParameters(new List<ReportParameter> { p1, p2, p3, p4, p5 });

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
