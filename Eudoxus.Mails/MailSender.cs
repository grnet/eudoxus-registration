using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using Eudoxus.BusinessModel;
using System.Xml.Linq;
using System.IO;
using System.Web;
using System.Net.Mail;
using System.Web.Security;
using System.Web.Profile;

namespace Eudoxus.Mails
{
    public static class MailSender
    {
        private static readonly ILog log = LogManager.GetLogger("MailSender");

        /// <summary>
        /// When true, no mails are sent, they are only logged.
        /// </summary>
        public static bool DebugMode = false;

        /// <summary>
        /// Replaces all possible variables in mail body templates.
        /// </summary>
        /// <param name="body">The body of the Message</param>
        /// <param name="values">A dictionary with the values to replace. ex: Key: Username,Value: "djsolid" </param>
        /// <returns></returns>
        public static string ReplaceVars(string body, Dictionary<string, string> values)
        {
            string bodyReplaced = body;
            foreach (var value in values)
            {
                bodyReplaced = bodyReplaced.Replace(string.Format("%{0}%", value.Key.ToUpper()), value.Value);
            }
            return bodyReplaced;
        }

        /// <summary>
        /// Sends mail
        /// </summary>
        /// <param name="From">Sender e-mail address</param>
        /// <param name="To">Receiver e-mail address</param>
        /// <param name="Subject">E-mail subject</param>
        /// <param name="Body">E-mail body</param>
        private static void Send(string from, string to, string subject, string body, string footer, bool htmlBody)
        {
            try
            {
                if (string.IsNullOrEmpty(from))
                    throw new ArgumentException("Will not send email from invalid address");
                else if (string.IsNullOrEmpty(to))
                    throw new ArgumentException("Will not send email to invalid address");
                else
                {
                    if (DebugMode)
                    {
                        log.InfoFormat("From {0}, to {1}, subject {2}, body {3}", from, to, subject, body + footer);
                    }
                    else
                    {
                        MailMessage m = new MailMessage(from, to, subject, body + footer);
                        SmtpClient sc = new SmtpClient();

                        m.IsBodyHtml = htmlBody;

                        sc.Send(m);
                        log.InfoFormat("Sent. From {0}, to {1}, subject {2}, body {3}", from, to, subject, body + footer);
                    }
                }
            }
            catch (ArgumentException ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        private static void Send(string to, string subject, string body, string footer, bool htmlBody)
        {
            body = body.Replace("«", "\"").Replace("»", "\"").Replace("&#171;", "\"").Replace("&#187;", "\"");
            Send(GetNoReplyMail(), to, subject, body, footer, htmlBody);
        }

        public static void SendUserActivation(string to, string username, Uri uri)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_Activation);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("username", username);
            values.Add("link", uri.ToString());
            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendPublisherVerification(string to, string username)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_PublisherVerification);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("username", username);
            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendEbookPublisherVerification(string to, string username)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_EbookPublisherVerification);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("username", username);
            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendSecretaryVerification(string to, string username)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_SecretaryVerification);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("username", username);
            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendDistributionPointVerification(string to, string username)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_DistributionPointVerification);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("username", username);
            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendPublicationsOfficeVerification(string to, string username)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_PublicationsOfficeVerification);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("username", username);
            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendDataCenterVerification(string to, string username)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_DataCenterVerification);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("username", username);
            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendLibraryVerification(string to, string username)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_LibraryVerification);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("username", username);
            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendBookSupplierVerification(string to, string username)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_BookSupplierVerification);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("username", username);
            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendPricingCommitteeVerification(string to, string username)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_PricingCommitteeVerification);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("username", username);
            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendMinistryPaymentsUserVerification(string to, string username)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_MinistryPaymentsUserVerification);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("username", username);
            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendForgotPassword(string to, string username, string password)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_ForgotPassword);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("username", username);
            values.Add("password", password);
            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendIncidentReportAnswer(string to, string reportID, string question, string answer)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_IncidentReportAnswer);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("reportID", reportID);
            values.Add("reportText", question);
            values.Add("reportAnswer", answer);

            MailSender.Send(to, mailDetails.Subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static void SendCustomMessage(string to, string subject, string message)
        {
            MailDetails mailDetails = MailDetailsReader.GetMailDetails(AV_CustomMessage);
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("message", message);
            values.Add("subject", subject);

            MailSender.Send(to, subject, ReplaceVars(mailDetails.Body, values), MailDetailsReader.GetMailFooter(), false);
        }

        public static string GetNoReplyMail()
        {
            return "no-reply@eudoxus.gr";
        }

        public static string GetHelpDeskMail()
        {
            return "info@eudoxus.gr";
        }

        #region XML emails configuration

        // Element names in XML file
        private static readonly string E_Mail = "Mail";
        private static readonly string E_Subject = "Subject";
        private static readonly string E_Body = "Body";
        private static readonly string E_Footer = "Footer";
        // Attribute keys in XML file
        private static readonly string AK_Category = "Category";
        // Attribute values in XML file
        private static readonly string AV_Activation = "Activation";
        private static readonly string AV_PublisherVerification = "PublisherVerification";
        private static readonly string AV_EbookPublisherVerification = "EbookPublisherVerification";
        private static readonly string AV_SecretaryVerification = "SecretaryVerification";
        private static readonly string AV_DistributionPointVerification = "DistributionPointVerification";
        private static readonly string AV_PublicationsOfficeVerification = "PublicationsOfficeVerification";
        private static readonly string AV_DataCenterVerification = "DataCenterVerification";
        private static readonly string AV_LibraryVerification = "LibraryVerification";
        private static readonly string AV_BookSupplierVerification = "BookSupplierVerification";
        private static readonly string AV_PricingCommitteeVerification = "PricingCommitteeVerification";
        private static readonly string AV_MinistryPaymentsUserVerification = "MinistryPaymentsUserVerification";
        private static readonly string AV_ForgotPassword = "ForgotPassword";
        private static readonly string AV_IncidentReportAnswer = "IncidentReportAnswer";
        private static readonly string AV_CustomMessage = "CustomMessage";

        private class MailDetails
        {
            public string Subject;
            public string Body;
        }

        private static class MailDetailsReader
        {
            private static XElement cachedMailsDetails = null;
            private static XElement CachedMailsDetails
            {
                get
                {
                    if (cachedMailsDetails == null)
                    {
                        if (HttpContext.Current == null)
                            cachedMailsDetails = XElement.Parse(File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/Mails.xml")));
                        else
                            cachedMailsDetails = XElement.Parse(File.ReadAllText(HttpContext.Current.Server.MapPath("~/App_Data/Mails.xml")));
                    }
                    return cachedMailsDetails;
                }
            }

            public static MailDetails GetMailDetails(string category)
            {
                return (from z in CachedMailsDetails.Descendants(E_Mail)
                        where (string)z.Attributes(AK_Category).Single() == category
                        select new MailDetails()
                        {
                            Subject = z.Descendants(E_Subject).Single().Value,
                            Body = z.Descendants(E_Body).Single().Value
                        }).Single();
            }

            public static string GetMailFooter()
            {
                return (from z in CachedMailsDetails.Elements(E_Footer)
                        select ((XCData)z.FirstNode).Value).Single();
            }
        }

        #endregion
    }
}
