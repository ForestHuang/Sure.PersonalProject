namespace Sure.PersonalProject.Entity
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-09-04
    [explain]:检查发现问题文件上传表
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
    /// 检查发现问题文件上传表
    /// </summary>
    [Serializable]
    public class TFB_BranchFile
    {
		private string branchCode;
		/// <summary>
		/// 分行代码
		/// <summary>
		[DisplayName("分行代码")]
		public string BranchCode{
			get { return branchCode; }
			set { branchCode = value; }
		}
		private int enCode;
		/// <summary>
		/// 附件序号
		/// <summary>
		[DisplayName("附件序号")]
		public int EnCode{
			get { return enCode; }
			set { enCode = value; }
		}
		private string filePath;
		/// <summary>
		/// 附件路径
		/// <summary>
		[DisplayName("附件路径")]
		public string FilePath{
			get { return filePath; }
			set { filePath = value; }
		}
		private string createDate;
		/// <summary>
		/// 创建时间
		/// <summary>
		[DisplayName("创建时间")]
		public string CreateDate{
			get { return createDate; }
			set { createDate = value; }
		}
		private string updateDate;
		/// <summary>
		/// 修改时间
		/// <summary>
		[DisplayName("修改时间")]
		public string UpdateDate{
			get { return updateDate; }
			set { updateDate = value; }
		}
		private string operator;
		/// <summary>
		/// 操作人
		/// <summary>
		[DisplayName("操作人")]
		public string Operator{
			get { return operator; }
			set { operator = value; }
		}
		private string branchCode;
		/// <summary>
		/// 序号
		/// <summary>
		[DisplayName("序号")]
		public string BranchCode{
			get { return branchCode; }
			set { branchCode = value; }
		}
		private string createDate;
		/// <summary>
		/// 创建时间
		/// <summary>
		[DisplayName("创建时间")]
		public string CreateDate{
			get { return createDate; }
			set { createDate = value; }
		}
		private string updateDate;
		/// <summary>
		/// 修改时间
		/// <summary>
		[DisplayName("修改时间")]
		public string UpdateDate{
			get { return updateDate; }
			set { updateDate = value; }
		}
		private string operator;
		/// <summary>
		/// 操作人
		/// <summary>
		[DisplayName("操作人")]
		public string Operator{
			get { return operator; }
			set { operator = value; }
		}
		private string createDate;
		/// <summary>
		/// 上报时间
		/// <summary>
		[DisplayName("上报时间")]
		public string CreateDate{
			get { return createDate; }
			set { createDate = value; }
		}
		private string operator;

		[DisplayName("")]
		public string Operator{
			get { return operator; }
			set { operator = value; }
		}

    }
}
