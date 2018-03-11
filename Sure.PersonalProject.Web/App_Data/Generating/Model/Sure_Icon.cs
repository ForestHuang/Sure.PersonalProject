namespace Sure.PersonalProject.Entity
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-09-27
    [explain]:ICON , 菜单图标
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
    /// ICON , 菜单图标
    /// </summary>
    [Serializable]
    public class Sure_Icon
    {
		private int iCON_Id;
		/// <summary>
		/// 主键
		/// <summary>
		[DisplayName("主键")]
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int ICON_Id{
			get { return iCON_Id; }
			set { iCON_Id = value; }
		}
		private string iCON_Name;
		/// <summary>
		/// 名称
		/// <summary>
		[DisplayName("名称")]
		public string ICON_Name{
			get { return iCON_Name; }
			set { iCON_Name = value; }
		}

    }
}
