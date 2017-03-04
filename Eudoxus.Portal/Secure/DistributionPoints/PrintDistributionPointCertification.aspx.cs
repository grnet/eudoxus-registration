using System;
using Eudoxus.BusinessModel;
using System.IO;

using Microsoft.Reporting.WebForms;
using System.Collections.Generic;
using Eudoxus.Portal.Controls;
using System.Web.Security;
using Microsoft.Data.Extensions;

namespace Eudoxus.Portal.Secure.DistributionPoints
{
    public partial class PrintDistributionPointCertification : BaseEntityPortalPage<DistributionPoint>
    {
        protected override void Fill()
        {
            Entity = new DistributionPointRepository(UnitOfWork).FindByUsername(AppUser.Username);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity == null)
            {
                UI.Message("DistributionPointNotFound");
                return;
            }

            string filename = "DistributionPoint-" + Entity.CertificationNumber + "-Certification.pdf";
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.ContentType = "application/octet-stream";

            CreatePDF();

            Response.End();
        }

        private void CreatePDF()
        {
            LocalReport lr = new LocalReport();
            lr.ReportPath = Server.MapPath("~/_rdlc/DistributionPointCertification.rdlc");

            MembershipUser user = Membership.GetUser(Entity.UserName);
            Eudoxus.BusinessModel.DistributionPointDetails distributionPointDetails = Entity.DistributionPointDetails;

            var city = CacheManager.Cities.Get(distributionPointDetails.CityID).Name;
            var prefecture = CacheManager.Prefectures.Get(distributionPointDetails.PrefectureID).Name;

            ReportParameter p1 = new ReportParameter("CertificationNumber", string.Format("Αριθμός Βεβαίωσης: {0} / {1:dd-MM-yyyy}", Entity.CertificationNumber, Entity.CertificationDate));
            ReportParameter p2 = new ReportParameter("DistributionPointDetails", string.Format("Βεβαιώνεται ότι ο/η/το <{0}> με διεύθυνση <{1}>, <{2}>, <{3}>, <{4}>, με e-mail <{5}> και τηλέφωνο <{6}> συμμετέχει στο πρόγραμμα «Εύδοξος – Ηλεκτρονική Υπηρεσία Ολοκληρωμένης Διαχείρισης Συγγραμμάτων» ως Σημείο Διανομής με το username: <{7}>.", Entity.DistributionPointName, distributionPointDetails.DistributionPointAddress, distributionPointDetails.DistributionPointZipCode, city, prefecture, distributionPointDetails.DistributionPointEmail, distributionPointDetails.DistributionPointPhone, user.UserName));
            ReportParameter p3 = new ReportParameter("ContactPersonDetails", string.Format("Για τις ανάγκες του συγκεκριμένου προγράμματος, το Σημείο Διανομής εκπροσωπεί ο/η <{0}>  με e-mail <{1}> και τηλέφωνο <{2}>.", Entity.ContactName, Entity.ContactEmail, Entity.ContactPhone));

            lr.DataSources.Add(new ReportDataSource("DistributionPoint", new DistributionPoint[] { new DistributionPoint() }));

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
