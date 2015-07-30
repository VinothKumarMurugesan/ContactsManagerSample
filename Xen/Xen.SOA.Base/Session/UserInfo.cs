using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Unify.SOA.Base
{
    public class UserInfo
    {
        #region Fields

        [DataMember]
        private string _userId = String.Empty;

        [DataMember]
        private string _userDisplayName = String.Empty;

        [DataMember]
        private string _password = String.Empty;

        [DataMember]
        private string _sessionId = String.Empty;

        #endregion

        #region Properties

        public string UserID
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string UserDisplayName
        {
            get { return _userDisplayName; }
            set { _userDisplayName = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        string SessionID
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }

        #endregion Properties
    }
}
