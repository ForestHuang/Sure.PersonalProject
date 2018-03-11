namespace Sure.PersonalProject.Entity
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-11-26
    [explain]:SURE_FILEINFO，文件生成列表
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
    /// SURE_FILEINFO，文件生成列表
    /// </summary>
    [Serializable]
    public class SURE_FILEINFO
    {
		private int sURE_FILE_ID;
		/// <summary>
		/// 主键自增长（文件ID）
		/// </summary>
		[DisplayName("主键自增长（文件ID）")]
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SURE_FILE_ID{
			get { return sURE_FILE_ID; }
			set { sURE_FILE_ID = value; }
		}
		private string sURE_FILE_NAME;
		/// <summary>
		/// 文件名称
		/// </summary>
		[DisplayName("文件名称")]
		public string SURE_FILE_NAME{
			get { return sURE_FILE_NAME; }
			set { sURE_FILE_NAME = value; }
		}
		private string sURE_FILE_TYPE;
		/// <summary>
		/// 文件类型
		/// </summary>
		[DisplayName("文件类型")]
		public string SURE_FILE_TYPE{
			get { return sURE_FILE_TYPE; }
			set { sURE_FILE_TYPE = value; }
		}
		private string sURE_FILE_PATH;
		/// <summary>
		/// 文件存储路径
		/// </summary>
		[DisplayName("文件存储路径")]
		public string SURE_FILE_PATH{
			get { return sURE_FILE_PATH; }
			set { sURE_FILE_PATH = value; }
		}
		private DateTime sURE_FILE_DATE;
		/// <summary>
		/// 文件生成时间
		/// </summary>
		[DisplayName("文件生成时间")]
		public DateTime SURE_FILE_DATE{
			get { return sURE_FILE_DATE; }
			set { sURE_FILE_DATE = value; }
		}
		private int sURE_FILE_GROUPING;
		/// <summary>
		/// 分组（1：类生成）
		/// </summary>
		[DisplayName("分组（1：类生成）")]
		public int SURE_FILE_GROUPING{
			get { return sURE_FILE_GROUPING; }
			set { sURE_FILE_GROUPING = value; }
		}
		private string sURE_FILE_OPERATOR;
		/// <summary>
		/// 操作人
		/// </summary>
		[DisplayName("操作人")]
		public string SURE_FILE_OPERATOR{
			get { return sURE_FILE_OPERATOR; }
			set { sURE_FILE_OPERATOR = value; }
		}

    }
}
