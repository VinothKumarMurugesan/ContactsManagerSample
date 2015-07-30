using System.Runtime.Serialization;

namespace Xen.SOA.Base
{
    public class SessionManager
    {
        [DataMember]
        private static UserContext _currentUserContext = null;

        public static UserContext CurrentUserContext
        {
            get
            {
                if (_currentUserContext == null)
                    _currentUserContext = new UserContext();
                return _currentUserContext;
            }

            set { _currentUserContext = value; }
        }
    }
}
