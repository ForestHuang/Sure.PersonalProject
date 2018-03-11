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
    public class TempDataSources
    {
		private int version;

		[DisplayName("")]
		public int Version{
			get { return version; }
			set { version = value; }
		}
		private string name;

		[DisplayName("")]
		public string Name{
			get { return name; }
			set { name = value; }
		}
		private string extension;

		[DisplayName("")]
		public string Extension{
			get { return extension; }
			set { extension = value; }
		}
		private int version;

		[DisplayName("")]
		public int Version{
			get { return version; }
			set { version = value; }
		}
		private string name;

		[DisplayName("")]
		public string Name{
			get { return name; }
			set { name = value; }
		}
		private Guid dSID;

		[DisplayName("")]
		public Guid DSID{
			get { return dSID; }
			set { dSID = value; }
		}
		private Guid itemID;

		[DisplayName("")]
		public Guid ItemID{
			get { return itemID; }
			set { itemID = value; }
		}
		private string name;

		[DisplayName("")]
		public string Name{
			get { return name; }
			set { name = value; }
		}
		private string extension;

		[DisplayName("")]
		public string Extension{
			get { return extension; }
			set { extension = value; }
		}
		private Guid link;

		[DisplayName("")]
		public Guid Link{
			get { return link; }
			set { link = value; }
		}
		private int credentialRetrieval;

		[DisplayName("")]
		public int CredentialRetrieval{
			get { return credentialRetrieval; }
			set { credentialRetrieval = value; }
		}
		private string prompt;

		[DisplayName("")]
		public string Prompt{
			get { return prompt; }
			set { prompt = value; }
		}
		private byte[] connectionString;

		[DisplayName("")]
		public byte[] ConnectionString{
			get { return connectionString; }
			set { connectionString = value; }
		}
		private byte[] originalConnectionString;

		[DisplayName("")]
		public byte[] OriginalConnectionString{
			get { return originalConnectionString; }
			set { originalConnectionString = value; }
		}
		private bool originalConnectStringExpressionBased;

		[DisplayName("")]
		public bool OriginalConnectStringExpressionBased{
			get { return originalConnectStringExpressionBased; }
			set { originalConnectStringExpressionBased = value; }
		}
		private byte[] userName;

		[DisplayName("")]
		public byte[] UserName{
			get { return userName; }
			set { userName = value; }
		}
		private byte[] password;

		[DisplayName("")]
		public byte[] Password{
			get { return password; }
			set { password = value; }
		}
		private int flags;

		[DisplayName("")]
		public int Flags{
			get { return flags; }
			set { flags = value; }
		}
		private int version;

		[DisplayName("")]
		public int Version{
			get { return version; }
			set { version = value; }
		}
		private Guid itemID;

		[DisplayName("")]
		public Guid ItemID{
			get { return itemID; }
			set { itemID = value; }
		}
		private string name;

		[DisplayName("")]
		public string Name{
			get { return name; }
			set { name = value; }
		}

    }
}
