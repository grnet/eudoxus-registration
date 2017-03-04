using System;
using Eudoxus.BusinessModel;
using System.IO;

using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using Eudoxus.Portal.Controls;
using System.Web.Security;
using Microsoft.Data.Extensions;

namespace Eudoxus.Portal.Secure.Libraries
{
    public partial class PrintLibraryCertification : BaseEntityPortalPage<Library>
    {
        protected override void Fill()
        {
            Entity = new LibraryRepository(UnitOfWork).FindByUsername(AppUser.Username);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity == null)
            {
                UI.Message("LibraryNotFound");
                return;
            }

            string filename = "Library-" + Entity.CertificationNumber + "-Certification.pdf";
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.ContentType = "application/octet-stream";

            CreatePDF();

            Response.End();
        }

        private void CreatePDF()
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Server.MapPath("~/_rdlc/LibraryCertification.rdlc");

            MembershipUser user = Membership.GetUser(Entity.UserName);
            Eudoxus.BusinessModel.LibraryDetails libraryDetails = Entity.LibraryDetails;
            var institution = CacheManager.Institutions.Get((int)Entity.InstitutionReference.GetKey());
            ReportParameter p1 = new ReportParameter("CertificationNumber", string.Format("Αριθμός Βεβαίωσης: {0} / {1:dd-MM-yyyy}", Entity.CertificationNumber, Entity.CertificationDate));
            ReportParameter p2 = new ReportParameter("Institution", institution.Name);
            ReportParameter p3 = new ReportParameter("LibraryName", Entity.LibraryName);
            ReportParameter p4 = new ReportParameter("DirectorName", libraryDetails.DirectorName);
            ReportParameter p5 = new ReportParameter("UserName", user.UserName);

            ReportParameter p6;
            if (libraryDetails.AlternateContactName != null)
            {
                p6 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, τη Βιβλιοθήκη εκπροσωπεί ο/η <{0}>  με e-mail <{1}> και τηλέφωνο <{2}>, τον/την οποίο/οποία αναπληρώνει ο/η <{3}> με e-mail <{4}> και τηλέφωνο <{5}>.", Entity.ContactName, Entity.ContactEmail, Entity.ContactPhone, libraryDetails.AlternateContactName, libraryDetails.AlternateContactEmail, libraryDetails.AlternateContactPhone));
            }
            else
            {
                p6 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, τη Βιβλιοθήκη εκπροσωπεί ο/η <{0}>  με e-mail <{1}> και τηλέφωνο <{2}>.", Entity.ContactName, Entity.ContactEmail, Entity.ContactPhone));
            }

            lr.DataSources.Add(new ReportDataSource("Library", new Library[] { new Library() }));

            lr.SetParameters(new List<ReportParameter> { p1, p2, p3, p4, p5, p6 });

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
