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
    public class ExecutionCache
    {
		private Guid snapshotDataID;

		[DisplayName("")]
		public Guid SnapshotDataID{
			get { return snapshotDataID; }
			set { snapshotDataID = value; }
		}
		private Guid executionCacheID;

		[DisplayName("")]
		public Guid ExecutionCacheID{
			get { return executionCacheID; }
			set { executionCacheID = value; }
		}
		private Guid reportID;

		[DisplayName("")]
		public Guid ReportID{
			get { return reportID; }
			set { reportID = value; }
		}
		private int expirationFlags;

		[DisplayName("")]
		public int ExpirationFlags{
			get { return expirationFlags; }
			set { expirationFlags = value; }
		}
		private DateTime absoluteExpiration;

		[DisplayName("")]
		public DateTime AbsoluteExpiration{
			get { return absoluteExpiration; }
			set { absoluteExpiration = value; }
		}
		private int relativeExpiration;

		[DisplayName("")]
		public int RelativeExpiration{
			get { return relativeExpiration; }
			set { relativeExpiration = value; }
		}
		private Guid snapshotDataID;

		[DisplayName("")]
		public Guid SnapshotDataID{
			get { return snapshotDataID; }
			set { snapshotDataID = value; }
		}
		private DateTime lastUsedTime;

		[DisplayName("")]
		public DateTime LastUsedTime{
			get { return lastUsedTime; }
			set { lastUsedTime = value; }
		}
		private int paramsHash;

		[DisplayName("")]
		public int ParamsHash{
			get { return paramsHash; }
			set { paramsHash = value; }
		}
		private Guid snapshotDataID;

		[DisplayName("")]
		public Guid SnapshotDataID{
			get { return snapshotDataID; }
			set { snapshotDataID = value; }
		}
		private int paramsHash;

		[DisplayName("")]
		public int ParamsHash{
			get { return paramsHash; }
			set { paramsHash = value; }
		}
		private Guid snapshotDataID;

		[DisplayName("")]
		public Guid SnapshotDataID{
			get { return snapshotDataID; }
			set { snapshotDataID = value; }
		}
		private Guid snapshotDataID;

		[DisplayName("")]
		public Guid SnapshotDataID{
			get { return snapshotDataID; }
			set { snapshotDataID = value; }
		}

    }
}
