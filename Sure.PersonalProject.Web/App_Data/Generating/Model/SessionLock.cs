namespace Sure.PersonalProject.Entity
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-08-29
    [explain]:sa
    -----------------------------------------------------------------------*/
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// sa
    /// </summary>
    [Serializable]
    public class SessionLock
    {
		private string sessionID;

		[DisplayName("")]
		public string SessionID{
			get { return sessionID; }
			set { sessionID = value; }
		}
		private int lockVersion;

		[DisplayName("")]
		public int LockVersion{
			get { return lockVersion; }
			set { lockVersion = value; }
		}
		private string sessionID;

		[DisplayName("")]
		public string SessionID{
			get { return sessionID; }
			set { sessionID = value; }
		}
		private string sessionID;

		[DisplayName("")]
		public string SessionID{
			get { return sessionID; }
			set { sessionID = value; }
		}

    }
}
