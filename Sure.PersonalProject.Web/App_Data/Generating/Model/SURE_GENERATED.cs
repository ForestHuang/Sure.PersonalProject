namespace Sure.PersonalProject.Entity
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-11-26
    [explain]:BLL/DAL/MODEL，文件生成列表
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
    /// BLL/DAL/MODEL，文件生成列表
    /// </summary>
    [Serializable]
    public class SURE_GENERATED
    {
		private int sURE_GENERATED_ID;
		/// <summary>
		/// 主键自增长
		/// </summary>
		[DisplayName("主键自增长")]
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int SURE_GENERATED_ID{
			get { return sURE_GENERATED_ID; }
			set { sURE_GENERATED_ID = value; }
		}
		private string sURE_GENERATED_NAME;
		/// <summary>
		/// 生成类名称（BLL/DAL/MODEL）
		/// </summary>
		[DisplayName("生成类名称（BLL/DAL/MODEL）")]
		public string SURE_GENERATED_NAME{
			get { return sURE_GENERATED_NAME; }
			set { sURE_GENERATED_NAME = value; }
		}
		private string sURE_GENERATED_TYPE;
		/// <summary>
		/// 类型（DAL/BLL/MODEL）
		/// </summary>
		[DisplayName("类型（DAL/BLL/MODEL）")]
		public string SURE_GENERATED_TYPE{
			get { return sURE_GENERATED_TYPE; }
			set { sURE_GENERATED_TYPE = value; }
		}
		private string sURE_GENERATED_PATH;
		/// <summary>
		/// 生成文件存放路径
		/// </summary>
		[DisplayName("生成文件存放路径")]
		public string SURE_GENERATED_PATH{
			get { return sURE_GENERATED_PATH; }
			set { sURE_GENERATED_PATH = value; }
		}
		private DateTime sURE_GENERATED_DATE;
		/// <summary>
		/// 文件生成时间
		/// </summary>
		[DisplayName("文件生成时间")]
		public DateTime SURE_GENERATED_DATE{
			get { return sURE_GENERATED_DATE; }
			set { sURE_GENERATED_DATE = value; }
		}
		private string sURE_GENERATED_OPERATOR;
		/// <summary>
		/// 操作人
		/// </summary>
		[DisplayName("操作人")]
		public string SURE_GENERATED_OPERATOR{
			get { return sURE_GENERATED_OPERATOR; }
			set { sURE_GENERATED_OPERATOR = value; }
		}

    }
}
