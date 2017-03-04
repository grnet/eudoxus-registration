using System;
using Eudoxus.BusinessModel;
using System.IO;

using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using Eudoxus.Portal.Controls;
using System.Web.Security;
using Microsoft.Data.Extensions;

namespace Eudoxus.Portal.Secure.BookSuppliers
{
    public partial class PrintBookSupplierCertification : BaseEntityPortalPage<BookSupplier>
    {
        protected override void Fill()
        {
            Entity = new BookSupplierRepository(UnitOfWork).FindByUsername(AppUser.Username);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity == null)
            {
                UI.Message("BookSupplierNotFound");
                return;
            }

            string filename = "BookSupplier-" + Entity.CertificationNumber + "-Certification.pdf";
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.ContentType = "application/octet-stream";

            CreatePDF();

            Response.End();
        }

        private void CreatePDF()
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Server.MapPath("~/_rdlc/BookSupplierCertification.rdlc");

            MembershipUser user = Membership.GetUser(Entity.UserName);
            Eudoxus.BusinessModel.BookSupplierDetails bookSupplierDetails = Entity.BookSupplierDetails;
            var institution = CacheManager.Institutions.Get((int)Entity.InstitutionReference.GetKey());
            ReportParameter p1 = new ReportParameter("CertificationNumber", string.Format("Αριθμός Βεβαίωσης: {0} / {1:dd-MM-yyyy}", Entity.CertificationNumber, Entity.CertificationDate));
            ReportParameter p2 = new ReportParameter("Institution", institution.Name);
            ReportParameter p3 = new ReportParameter("CertifierType", bookSupplierDetails.CertifierType == enCertifierType.AEIPresident ? "ΠΡΥΤΑΝΗ" : "ΠΡΟΕΔΡΟΥ");
            ReportParameter p4 = new ReportParameter("CertifierName", bookSupplierDetails.CertifierName);

            ReportParameter p5;
            if (bookSupplierDetails.AlternateContactName != null)
            {
                p5 = new ReportParameter("ContactPersonDetails", string.Format("Βεβαιώνεται ότι το ως άνω Ίδρυμα ορίζει ως Υπεύθυνο Παραγγελίας Βιβλίων στο πρόγραμμα «Εύδοξος - Ηλεκτρονική Υπηρεσία Ολοκληρωμένης Διαχείρησης Συγγραμμάτων» τον/την <{0}> με e-mail <{1}> και τηλέφωνο <{2}>, τον/την οποίο/οποία αναπληρώνει ο/η <{3}> με e-mail <{4}>, τηλέφωνο <{5}> και με username <{6}>", Entity.ContactName, Entity.ContactEmail, Entity.ContactPhone, bookSupplierDetails.AlternateContactName, bookSupplierDetails.AlternateContactEmail, bookSupplierDetails.AlternateContactPhone, Entity.UserName));
            }
            else
            {
                p5 = new ReportParameter("ContactPersonDetails", string.Format("Βεβαιώνεται ότι το ως άνω Ίδρυμα ορίζει ως Υπεύθυνο Παραγγελίας Βιβλίων στο πρόγραμμα «Εύδοξος - Ηλεκτρονική Υπηρεσία Ολοκληρωμένης Διαχείρησης Συγγραμμάτων» τον/την <{0}> με e-mail <{1}>, τηλέφωνο <{2}> και με username <{3}>", Entity.ContactName, Entity.ContactEmail, Entity.ContactPhone, Entity.UserName));
            }

            lr.DataSources.Add(new ReportDataSource("BookSupplier", new BookSupplier[] { new BookSupplier() }));

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
