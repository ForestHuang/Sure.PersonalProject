namespace Sure.PersonalProject.Entity
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-08-29
    [explain]:sas
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
    /// sas
    /// </summary>
    [Serializable]
    public class Sure_GeneratingInfo
    {
		private int generatingId;
		/// <summary>
		/// 主键,自增长
		/// <summary>
		[DisplayName("主键,自增长")]
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int GeneratingId{
			get { return generatingId; }
			set { generatingId = value; }
		}
		private string generatingName;
		/// <summary>
		/// 名称
		/// <summary>
		[DisplayName("名称")]
		public string GeneratingName{
			get { return generatingName; }
			set { generatingName = value; }
		}
		private string generatingPath;
		/// <summary>
		/// 路径
		/// <summary>
		[DisplayName("路径")]
		public string GeneratingPath{
			get { return generatingPath; }
			set { generatingPath = value; }
		}
		private string generatingTemplateName;
		/// <summary>
		/// 模板名称
		/// <summary>
		[DisplayName("模板名称")]
		public string GeneratingTemplateName{
			get { return generatingTemplateName; }
			set { generatingTemplateName = value; }
		}
		private string generatingTemplatePath;
		/// <summary>
		/// 模板路径
		/// <summary>
		[DisplayName("模板路径")]
		public string GeneratingTemplatePath{
			get { return generatingTemplatePath; }
			set { generatingTemplatePath = value; }
		}
		private string generatingType;
		/// <summary>
		/// 类型（Model、DAL、BLL）
		/// <summary>
		[DisplayName("类型（Model、DAL、BLL）")]
		public string GeneratingType{
			get { return generatingType; }
			set { generatingType = value; }
		}

    }
}
