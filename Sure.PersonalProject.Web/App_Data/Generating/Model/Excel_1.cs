namespace Sure.PersonalProject.Entity
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-11-06
    [explain]:11
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
    /// 11
    /// </summary>
    [Serializable]
    public class Excel_1
    {
		private int id;
		/// <summary>
		/// ID
		/// <summary>
		[DisplayName("ID")]
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id{
			get { return id; }
			set { id = value; }
		}
		private string name;
		/// <summary>
		/// 姓名
		/// <summary>
		[DisplayName("姓名")]
		public string Name{
			get { return name; }
			set { name = value; }
		}
		private string type_1;
		/// <summary>
		/// 激活
		/// <summary>
		[DisplayName("激活")]
		public string Type_1{
			get { return type_1; }
			set { type_1 = value; }
		}
		private int id;
		/// <summary>
		/// ID
		/// <summary>
		[DisplayName("ID")]
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id{
			get { return id; }
			set { id = value; }
		}

    }
}
