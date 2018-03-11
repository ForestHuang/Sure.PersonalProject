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
    public class DBUpgradeHistory
    {
		private long upgradeID;

		[DisplayName("")]
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long UpgradeID{
			get { return upgradeID; }
			set { upgradeID = value; }
		}
		private string dbVersion;

		[DisplayName("")]
		public string DbVersion{
			get { return dbVersion; }
			set { dbVersion = value; }
		}
		private string user;

		[DisplayName("")]
		public string User{
			get { return user; }
			set { user = value; }
		}
		private DateTime dateTime;

		[DisplayName("")]
		public DateTime DateTime{
			get { return dateTime; }
			set { dateTime = value; }
		}

    }
}
