using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Xen.Entity;

namespace Xen.Helpers
{
    public static class NotificationHelper
    {
        public static bool SendNotification(NotificationInfo notificationInfo)
        {
            switch (notificationInfo.NotificationType)
            {
                case MessageNotificationType.HtmlEMail:
                    return SendEMailNotification(notificationInfo);
                case MessageNotificationType.TextEMail:
                    return SendEMailNotification(notificationInfo);
                case MessageNotificationType.Pager:
                    break;
                case MessageNotificationType.SMS:
                    return SendSmsNotification(notificationInfo);
                case MessageNotificationType.IM:
                    break;
            }
            return false;
        }

        private static bool SendEMailNotification(NotificationInfo notificationMessage)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            if (!string.IsNullOrEmpty(notificationMessage.To))
                mailMessage.To.Add(notificationMessage.To);

            mailMessage.Subject = notificationMessage.Subject;
            mailMessage.IsBodyHtml = (notificationMessage.NotificationType == MessageNotificationType.HtmlEMail);
            mailMessage.Body = notificationMessage.Message;


            if (notificationMessage.Attachments != null && notificationMessage.Attachments.Count > 0)
            {
                foreach (Attachment attachment in notificationMessage.Attachments.Select(attachmentName => new Attachment(attachmentName)))
                    mailMessage.Attachments.Add(attachment);
            }

            if (!string.IsNullOrEmpty(notificationMessage.CC))
                mailMessage.CC.Add(notificationMessage.CC);

            if (!string.IsNullOrEmpty(notificationMessage.BCC))
                mailMessage.Bcc.Add(notificationMessage.BCC);

           //Below line is added, sometimes SMTP fails to send mail at first time
            smtpClient.ServicePoint.MaxIdleTime = 1;
            smtpClient.Send(mailMessage);
           
           // Below work around is added, sometimes SMTP fails to send mail at first time
            //bool result = false;
            //do
            //{
            //    result = SendMail(smtpClient, mailMessage);
            //} while (!result);

            return true;
        }

        private static bool SendMail(SmtpClient smtpClient, MailMessage mailMessage)
        {
            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool SendSmsNotification(NotificationInfo notificationMessage)
        {
            return SendSmsByFlashMediaApi(notificationMessage);
        }

        //private static bool SendSmsByModem(NotificationInfo notificationMessage)
        //{
        //    //SerialPort serialPort = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
        //    SerialPort serialPort = new SerialPort(notificationMessage.ComPort);
        //    serialPort.Open();

        //    #region Send SMS Using GSM Modem

        //    //serialPort.WriteLine("AT" + (char)(13));
        //    //Thread.Sleep(100);
        //    //serialPort.WriteLine("AT+CMGF=1" + (char)(13));
        //    //Thread.Sleep(100);
        //    //serialPort.WriteLine("AT+CMGS=\"" + notificationMessage.MobileNo + "\"" + (char)(13));
        //    //Thread.Sleep(100);
        //    //serialPort.WriteLine(notificationMessage.Message + (char)(26));
        //    //Thread.Sleep(100);

        //    #endregion

        //    #region Send SMS Using Customized GSM Modem (Wiman)

        //    serialPort.WriteLine("AT+CMGS=\"" + notificationMessage.MobileNo + "\" " + notificationMessage.Message +
        //                         (char) (13));
        //    Thread.Sleep(100);

        //    #endregion

        //    serialPort.Close();

        //    return true;
        //}

        public static bool SendSmsByFlashMediaApi(NotificationInfo notificationInfo)
        {
            SmsResponse smsResponse = null;
            string parameter = string.Format("destination={0}&message={1}", HttpUtility.UrlEncode(notificationInfo.MobileNo), HttpUtility.UrlEncode(notificationInfo.Message));
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(notificationInfo.SmsApiUrl);
            req.Method = "POST";
            var encodedStr = Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}:{1}", notificationInfo.SmsApiUserName, notificationInfo.SmsApiPassword)));
            var authorizationKey = "Basic " + encodedStr;
            req.Headers.Add("Authorization", authorizationKey);
            req.ContentType = "application/x-www-form-urlencoded";
            req.AllowAutoRedirect = true;
            req.KeepAlive = false;
            req.ContentLength = parameter.Length;
            StreamWriter writer = new StreamWriter(req.GetRequestStream());
            writer.Write(parameter);
            writer.Close();
            HttpWebResponse objResponse = (HttpWebResponse)req.GetResponse();
            
            if (objResponse != null && objResponse.GetResponseStream() != null)
            {
                using (StreamReader responseStream = new StreamReader(objResponse.GetResponseStream()))
                {
                    smsResponse = JsonConvert.DeserializeObject<SmsResponse>(responseStream.ReadToEnd());
                    responseStream.Close();
                    return (smsResponse != null && smsResponse.BatchSize == 1);
                }
            }
            return true;
        }

        //private static bool SendSmsBySmsPortalApi(NotificationInfo notificationInfo)
        //{
        //    //http://www.mymobileapi.com/api5/api.asmx
        //    string inputString = string.Format("<senddata><settings><return_entries_success_status>True</return_entries_success_status><return_entries_failed_status>True</return_entries_failed_status><default_date>{0}</default_date><default_time>{1}</default_time></settings><entries><numto>{2}</numto><customerid>Customer_Id</customerid><data1>{3}</data1></entries></senddata>", DateTime.Now.ToString("dd/MMM/yyyy"), DateTime.Now.ToString("HH:mm"), notificationInfo.MobileNo, notificationInfo.Message);
        //    APISoapClient apiSoapClient = SendApiSoapClient(notificationInfo.SmsApiUrl);
        //    var outputString = apiSoapClient.Send_STR_STR(notificationInfo.SmsApiUserName, notificationInfo.SmsApiPassword, inputString);

        //    using (var reader = new StringReader(outputString))
        //    using (var xmlReader = XmlReader.Create(reader))
        //    {
        //        while (xmlReader.Read())
        //        {
        //            if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "entries_failed")
        //                return false;
        //            //else if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "entries_success")
        //            //    return true;
        //        }
        //    }
        //    return true;
        //}

        //private static APISoapClient SendApiSoapClient(string url)
        //{
        //    EndpointAddress endpoint = new EndpointAddress(new Uri(url));
        //    BasicHttpBinding httpBinding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
        //    httpBinding.MaxReceivedMessageSize = 2147483647;
        //    httpBinding.MaxBufferSize = 2147483647;
        //    httpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
        //    return new APISoapClient(httpBinding, endpoint);
        //}

        public class SmsResponse
        {
            public long BatchId { get; set; }
            public bool Accepted { get; set; }
            public int RemainingCredits { get; set; }
            public int BatchSize { get; set; }
            public bool ReceiveUpdates { get; set; }
        }
    }
}
