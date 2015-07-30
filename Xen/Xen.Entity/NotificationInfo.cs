using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xen.Entity
{
    [DataContract]
    public class NotificationInfo
    {
        [DataMember]
        public string To { get; set; }

        [DataMember]
        public string CC { get; set; }

        [DataMember]
        public string BCC { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Body { get; set; }

        private List<string> _attachments;

        [DataMember]
        public List<string> Attachments
        {
            get { return _attachments ?? (_attachments = new List<string>()); }
            set { _attachments = value; }
        }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string MobileNo { get; set; }

        [DataMember]
        public MessageNotificationType NotificationType { get; set; }

        [DataMember]
        public string Port { get; set; }

        [DataMember]
        public ModemType ModemType { get; set; }

        [DataMember]
        public string ComPort { get; set; }

        [DataMember]
        public string SmsApiUserName { get; set; }

        [DataMember]
        public string SmsApiPassword { get; set; }

        [DataMember]
        public string SmsApiUrl { get; set; }

        [DataMember]
        public bool IsHighPriority { get; set; }

    }
}
