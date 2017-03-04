using System;
using Eudoxus.BusinessModel;
using System.IO;

using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using Eudoxus.Portal.Controls;
using System.Web.Security;

namespace Eudoxus.Portal.Secure.Publishers
{
    public partial class PrintPublisherCertification : BaseEntityPortalPage<Publisher>
    {
        protected override void Fill()
        {
            Entity = new PublisherRepository(UnitOfWork).FindByUsername(AppUser.Username);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity == null)
            {
                UI.Message("OrderNotFound");
                return;
            }

            string filename = "Publisher-" + Entity.CertificationNumber + "-Certification.pdf";
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.ContentType = "application/octet-stream";

            if (Entity.PublisherType == enPublisherType.LegalPerson)
            {
                CreateLegalPersonPDF();
            }
            else if (Entity.PublisherType == enPublisherType.SelfPublisher)
            {
                CreateSelfPublisherPDF();
            }
            else if (Entity.PublisherType == enPublisherType.EbookPublisher)
            {
                CreateEbookPublisherPDF();
            }

            Response.End();
        }

        private void CreateLegalPersonPDF()
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Server.MapPath("~/_rdlc/LegalPersonCertification.rdlc");

            MembershipUser user = Membership.GetUser(Entity.UserName);
            Eudoxus.BusinessModel.PublisherDetails publisherDetails = Entity.PublisherDetails;

            ReportParameter p1 = new ReportParameter("CertificationNumber", string.Format("Αριθμός Βεβαίωσης: {0} / {1:dd-MM-yyyy}", Entity.CertificationNumber, Entity.CertificationDate));

            ReportParameter p2;
            if (publisherDetails.LegalPersonIdentificationType == enIdentificationType.ID)
            {
                p2 = new ReportParameter("PublisherDetails", string.Format("Βεβαιώνεται ότι η εταιρεία <{0}> με Α.Φ.Μ. <{1}>, Δ.Ο.Υ. <{2}> της οποίας Νόμιμος Εκπρόσωπος είναι ο/η <{3}> με Α.Δ.Τ. <{4}>, που εκδόθηκε την <{5:dd/MM/yyyy}> στο <{6}>, με τηλέφωνο <{7}> συμμετέχει στο πρόγραμμα «Εύδοξος – Ηλεκτρονική Υπηρεσία Ολοκληρωμένης Διαχείρισης Συγγραμμάτων» ως εκδότης με το username: <{8}>.", Entity.PublisherName, Entity.PublisherAFM, publisherDetails.PublisherDOY, publisherDetails.LegalPersonName, publisherDetails.LegalPersonIdentificationNumber, publisherDetails.LegalPersonIdentificationIssueDate, publisherDetails.LegalPersonIdentificationIssuer, publisherDetails.LegalPersonPhone, user.UserName));
            }
            else
            {
                p2 = new ReportParameter("PublisherDetails", string.Format("Βεβαιώνεται ότι η εταιρεία <{0}> με Α.Φ.Μ. <{1}>, Δ.Ο.Υ. <{2}> της οποίας Νόμιμος Εκπρόσωπος είναι ο/η <{3}> με Αριθμό Διαβατηρίου <{4}>, με τηλέφωνο <{5}> συμμετέχει στο πρόγραμμα «Εύδοξος – Ηλεκτρονική Υπηρεσία Ολοκληρωμένης Διαχείρισης Συγγραμμάτων» ως εκδότης με το username: <{6}>.", Entity.PublisherName, Entity.PublisherAFM, publisherDetails.PublisherDOY, publisherDetails.LegalPersonName, publisherDetails.LegalPersonIdentificationNumber, publisherDetails.LegalPersonPhone, user.UserName));
            }

            ReportParameter p3;
            if (publisherDetails.ContactIdentificationType == enIdentificationType.ID)
            {
                if (publisherDetails.AlternateContactName != null)
                {
                    p3 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, την εταιρεία εκπροσωπεί ο/η <{0}> με Α.Δ.Τ. <{1}>, που εκδόθηκε την <{2:dd/MM/yyyy}> στο <{3}>, με e-mail <{4}> και τηλέφωνο <{5}> του/της οποίου/οποίας αναπληρωτής/αναπληρώτρια είναι ο/η <{6}> με e-mail <{7}> και τηλέφωνο <{8}>.", Entity.ContactName, publisherDetails.ContactIdentificationNumber, publisherDetails.ContactIdentificationIssueDate, publisherDetails.ContactIdentificationIssuer, Entity.ContactEmail, Entity.ContactPhone, publisherDetails.AlternateContactName, publisherDetails.AlternateContactEmail, publisherDetails.AlternateContactPhone));
                }
                else
                {
                    p3 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, την εταιρεία εκπροσωπεί ο/η <{0}> με Α.Δ.Τ. <{1}>, που εκδόθηκε την <{2:dd/MM/yyyy}> στο <{3}>, με e-mail <{4}> και τηλέφωνο <{5}>.", Entity.ContactName, publisherDetails.ContactIdentificationNumber, publisherDetails.ContactIdentificationIssueDate, publisherDetails.ContactIdentificationIssuer, Entity.ContactEmail, Entity.ContactPhone));
                }
            }
            else
            {
                if (publisherDetails.AlternateContactName != null)
                {
                    p3 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, την εταιρεία εκπροσωπεί ο/η <{0}> με Αριθμό Διαβατηρίου <{1}>, με e-mail <{2}> και τηλέφωνο <{3}> του/της οποίου/οποίας αναπληρωτής/αναπληρώτρια είναι ο/η <{4}> με e-mail <{5}> και τηλέφωνο <{6}>.", Entity.ContactName, publisherDetails.ContactIdentificationNumber, Entity.ContactEmail, Entity.ContactPhone, publisherDetails.AlternateContactName, publisherDetails.AlternateContactEmail, publisherDetails.AlternateContactPhone));
                }
                else
                {
                    p3 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, την εταιρεία εκπροσωπεί ο/η <{0}> με Αριθμό Διαβατηρίου <{1}>, με e-mail <{2}> και τηλέφωνο <{3}>.", Entity.ContactName, publisherDetails.ContactIdentificationNumber, Entity.ContactEmail, Entity.ContactPhone));
                }
            }

            ReportParameter p4;
            if (publisherDetails.LegalPersonIdentificationType == enIdentificationType.ID)
            {
                p4 = new ReportParameter("IdentificationCopy", "Επισυνάπτεται φωτοτυπία αστυνομικής ταυτότητας του Νομίμου Εκπροσώπου.");
            }
            else
            {
                p4 = new ReportParameter("IdentificationCopy", "Επισυνάπτεται φωτοτυπία διαβατηρίου του Νομίμου Εκπροσώπου.");
            }

            lr.DataSources.Add(new ReportDataSource("Publisher", new Publisher[] { new Publisher() }));

            lr.SetParameters(new List<ReportParameter> { p1, p2, p3, p4 });

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

        private void CreateSelfPublisherPDF()
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Server.MapPath("~/_rdlc/SelfPublisherCertification.rdlc");

            MembershipUser user = Membership.GetUser(Entity.UserName);
            Eudoxus.BusinessModel.PublisherDetails publisherDetails = Entity.PublisherDetails;

            ReportParameter p1 = new ReportParameter("CertificationNumber", string.Format("Αριθμός Βεβαίωσης: {0} / {1:dd-MM-yyyy}", Entity.CertificationNumber, Entity.CertificationDate));

            ReportParameter p2;
            if (publisherDetails.SelfPublisherIdentificationType == enIdentificationType.ID)
            {
                p2 = new ReportParameter("PublisherDetails", string.Format("Βεβαιώνεται ότι ο/η <{0}> με Α.Δ.Τ. <{1}> που εκδόθηκε την <{2:dd/MM/yyyy}> στο <{3}>, με Α.Φ.Μ. <{4}>, Δ.Ο.Υ. <{5}>, με e-mail <{6}> και τηλέφωνο <{7}> συμμετέχει στο πρόγραμμα «Εύδοξος – Ηλεκτρονική Υπηρεσία Ολοκληρωμένης Διαχείρισης Συγγραμμάτων» ως Αυτοεκδότης με το username: <{8}>.", Entity.PublisherName, publisherDetails.SelfPublisherIdentificationNumber, publisherDetails.SelfPublisherIdentificationIssueDate, publisherDetails.SelfPublisherIdentificationIssuer, Entity.PublisherAFM, publisherDetails.PublisherDOY, publisherDetails.PublisherEmail, publisherDetails.PublisherPhone, user.UserName));
            }
            else
            {
                p2 = new ReportParameter("PublisherDetails", string.Format("Βεβαιώνεται ότι ο/η <{0}> με Αριθμό Διαβατηρίου <{1}>, με Α.Φ.Μ. <{2}>, Δ.Ο.Υ. <{3}>, με e-mail <{4}> και τηλέφωνο <{5}> συμμετέχει στο πρόγραμμα «Εύδοξος – Ηλεκτρονική Υπηρεσία Ολοκληρωμένης Διαχείρισης Συγγραμμάτων» ως Αυτοεκδότης με το username: <{6}>.", Entity.PublisherName, publisherDetails.SelfPublisherIdentificationNumber, Entity.PublisherAFM, publisherDetails.PublisherDOY, publisherDetails.PublisherEmail, publisherDetails.PublisherPhone, user.UserName));
            }

            ReportParameter p3;
            if (publisherDetails.ContactIdentificationType == enIdentificationType.ID)
            {
                p3 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, ο/η Αυτοεκδότης εκπροσωπείται από τον/την <{0}> με Α.Δ.Τ. <{1}>, που εκδόθηκε την <{2:dd/MM/yyyy}> στο <{3}>, με e-mail <{4}> και τηλέφωνο <{5}>.", Entity.ContactName, publisherDetails.ContactIdentificationNumber, publisherDetails.ContactIdentificationIssueDate, publisherDetails.ContactIdentificationIssuer, Entity.ContactEmail, Entity.ContactPhone));
            }
            else
            {
                p3 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, ο/η Αυτοεκδότης εκπροσωπείται από τον/την <{0}> με Αριθμό Διαβατηρίου <{1}>, με e-mail <{2}> και τηλέφωνο <{3}>.", Entity.ContactName, publisherDetails.ContactIdentificationNumber, Entity.ContactEmail, Entity.ContactPhone));
            }

            ReportParameter p4;
            if (publisherDetails.SelfPublisherIdentificationType == enIdentificationType.ID)
            {
                p4 = new ReportParameter("IdentificationCopy", "Επισυνάπτεται φωτοτυπία αστυνομικής ταυτότητας του Αυτοεκδότη.");
            }
            else
            {
                p4 = new ReportParameter("IdentificationCopy", "Επισυνάπτεται φωτοτυπία διαβατηρίου του Αυτοεκδότη.");
            }

            lr.DataSources.Add(new ReportDataSource("Publisher", new Publisher[] { new Publisher() }));

            lr.SetParameters(new List<ReportParameter> { p1, p2, p3, p4 });

            lr.DataSources.Add(new ReportDataSource("Publisher", new Publisher[] { new Publisher() }));

            lr.SetParameters(new List<ReportParameter> { p1, p2, p3, p4 });

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

        private void CreateEbookPublisherPDF()
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Server.MapPath("~/_rdlc/EbookPublisherCertification.rdlc");

            MembershipUser user = Membership.GetUser(Entity.UserName);
            Eudoxus.BusinessModel.PublisherDetails publisherDetails = Entity.PublisherDetails;

            ReportParameter p1 = new ReportParameter("CertificationNumber", string.Format("Αριθμός Βεβαίωσης: {0} / {1:dd-MM-yyyy}", Entity.CertificationNumber, Entity.CertificationDate));

            ReportParameter p2;
            if (publisherDetails.ContactIdentificationType == enIdentificationType.ID)
            {
                p2 = new ReportParameter("PublisherDetails", string.Format("Βεβαιώνεται ότι ο/η <{0}> με Α.Δ.Τ. <{1}> που εκδόθηκε την <{2:dd/MM/yyyy}> στο <{3}>, με e-mail <{4}> και τηλέφωνο <{5}> συμμετέχει στο πρόγραμμα «Εύδοξος – Ηλεκτρονική Υπηρεσία Ολοκληρωμένης Διαχείρισης Συγγραμμάτων» ως Διαθέτης Δωρεάν Ηλεκτρονικών Βοηθημάτων και Σημειώσεων με το username: <{6}>.", Entity.PublisherName, publisherDetails.ContactIdentificationNumber, publisherDetails.ContactIdentificationIssueDate, publisherDetails.ContactIdentificationIssuer, publisherDetails.PublisherEmail, publisherDetails.PublisherPhone, user.UserName));
            }
            else
            {
                p2 = new ReportParameter("PublisherDetails", string.Format("Βεβαιώνεται ότι ο/η <{0}> με Αριθμό Διαβατηρίου <{1}>, με e-mail <{2}> και τηλέφωνο <{3}> συμμετέχει στο πρόγραμμα «Εύδοξος – Ηλεκτρονική Υπηρεσία Ολοκληρωμένης Διαχείρισης Συγγραμμάτων» ως Διαθέτης Δωρεάν Ηλεκτρονικών Βοηθημάτων και Σημειώσεων με το username: <{4}>.", Entity.PublisherName, publisherDetails.ContactIdentificationNumber, publisherDetails.PublisherEmail, publisherDetails.PublisherPhone, user.UserName));
            }

            ReportParameter p3;
            if (publisherDetails.ContactIdentificationType == enIdentificationType.ID)
            {
                p3 = new ReportParameter("IdentificationCopy", "Επισυνάπτεται φωτοτυπία αστυνομικής ταυτότητας του Διαθέτη Δωρεάν Ηλεκτρονικών Βοηθημάτων και Σημειώσεων.");
            }
            else
            {
                p3 = new ReportParameter("IdentificationCopy", "Επισυνάπτεται φωτοτυπία διαβατηρίου του Διαθέτη Δωρεάν Ηλεκτρονικών Βοηθημάτων και Σημειώσεων.");
            }

            lr.DataSources.Add(new ReportDataSource("Publisher", new Publisher[] { new Publisher() }));

            lr.SetParameters(new List<ReportParameter> { p1, p2, p3 });

            lr.DataSources.Add(new ReportDataSource("Publisher", new Publisher[] { new Publisher() }));

            lr.SetParameters(new List<ReportParameter> { p1, p2, p3 });

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
