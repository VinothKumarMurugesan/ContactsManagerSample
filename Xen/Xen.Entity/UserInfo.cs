
using System;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;

namespace Xen.Entity
{
    [DataContract]
    public class UserInfo
    {
        #region Properties

        [DataMember]
        public string UserId { get; set; }

        [DataMember]
        public string UserDisplayName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string SessionId { get; set; }

        //[DataMember]
        //public byte RoleType { get; set; }

        [DataMember]
        public string ConfirmPassword { get; set; }

        [DataMember]
        public int SelectedUserId { get; set; }


        #endregion Properties
    }

    [DataContract]
    public class LicenseBasicInfo 
    {
        #region Properties

        [DataMember]
        public int NoOfControllers
        {
            get { return _noOfControllers; }
            set
            {
                _noOfControllers = value;
                //RaisePropertyChanged("NoOfControllers");
            }
        }
        private int _noOfControllers;

        [DataMember]
        public Nullable<DateTime> EndTs
        {
            get { return _endTs; }
            set
            {
                _endTs = value;
                //RaisePropertyChanged("EndTs");
            }
        }
        private Nullable<DateTime> _endTs;

        [DataMember]
        public DateTime StartTs
        {
            get { return _startTs; }
            set
            {
                _startTs = value;
                //RaisePropertyChanged("StartTs");
            }
        }
        private DateTime _startTs;

        [DataMember]
        public int Country_Id
        {
            get { return _countryId; }
            set
            {
                _countryId = value;
                //RaisePropertyChanged("Country_Id");
            }
        }
        private int _countryId;

        [DataMember]
        public int CountryDivisionHierarchyDetail_Id
        {
            get { return _countryDivisionHierarchyDetailId; }
            set
            {
                _countryDivisionHierarchyDetailId = value;
                //RaisePropertyChanged("CountryDivisionHierarchyDetail_Id");
            }
        }
        private int _countryDivisionHierarchyDetailId;

        [DataMember]
        public bool IsPermanent
        {
            get;
            set;
        }

        [DataMember]
        public bool IsValidLicense
        {
            get;
            set;
        }

        [DataMember]
        public BitmapImage ClientLogoSource
        {
            get { return _clientLogoSource; }
            set
            {
                _clientLogoSource = value;
                //RaisePropertyChanged("ClientLogoSource");
            }
        }
        private BitmapImage _clientLogoSource;

        [DataMember]
        public string ClientName
        {
            get { return _clientName; }
            set
            {
                _clientName = value;
               // RaisePropertyChanged("ClientName");
            }
        }
        private string _clientName;

        [DataMember]
        public bool IsValidMacID
        {
            get;
            set;
        }

        [DataMember]
        public string LicenseType
        {
            get { return _licenseType; }
            set
            {
                _licenseType = value;
                //RaisePropertyChanged("LicenseType");
            }
        }
        private string _licenseType;

        #endregion Properties

    }

    [DataContract]
    public class RoleRightsPagesInfo
    {
        #region Properties
        [DataMember]
        public int RoleId
        {
            get;
            set;
        }

        [DataMember]
        public string ControlName
        {
            get;
            set;
        }

        [DataMember]
        public string DisplayName
        {
            get;
            set;
        }

        [DataMember]
        public int Page_Id
        {
            get;
            set;
        }

        [DataMember]
        public string PageName
        {
            get;
            set;
        }

        #endregion Properties

    }

    [DataContract]
    public class ClientInfo
    {
        #region Properties
        [DataMember]
        public int? ClientId //public Nullable<int> ClientId 
        {
            get;
            set;
        }

        [DataMember]
        public string ClientName
        {
            get;
            set;
        }

       
        #endregion Properties
    }
}
