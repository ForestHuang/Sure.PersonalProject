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
    public class SureUser
    {
		private int user_Id;
		/// <summary>
		/// 标识Id、主键
		/// <summary>
		[DisplayName("标识Id、主键")]
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int User_Id{
			get { return user_Id; }
			set { user_Id = value; }
		}
		private string user_FirstName;
		/// <summary>
		/// 姓氏
		/// <summary>
		[DisplayName("姓氏")]
		public string User_FirstName{
			get { return user_FirstName; }
			set { user_FirstName = value; }
		}
		private string user_LastName;
		/// <summary>
		/// 名字
		/// <summary>
		[DisplayName("名字")]
		public string User_LastName{
			get { return user_LastName; }
			set { user_LastName = value; }
		}
		private string user_Account;
		/// <summary>
		/// 账号
		/// <summary>
		[DisplayName("账号")]
		public string User_Account{
			get { return user_Account; }
			set { user_Account = value; }
		}
		private string user_Pass;
		/// <summary>
		/// 密码
		/// <summary>
		[DisplayName("密码")]
		public string User_Pass{
			get { return user_Pass; }
			set { user_Pass = value; }
		}
		private string user_IP;
		/// <summary>
		/// IP地址
		/// <summary>
		[DisplayName("IP地址")]
		public string User_IP{
			get { return user_IP; }
			set { user_IP = value; }
		}
		private DateTime user_LoginTime;
		/// <summary>
		/// 登录时间
		/// <summary>
		[DisplayName("登录时间")]
		public DateTime User_LoginTime{
			get { return user_LoginTime; }
			set { user_LoginTime = value; }
		}
		private DateTime user_DownTime;
		/// <summary>
		/// 下线时间
		/// <summary>
		[DisplayName("下线时间")]
		public DateTime User_DownTime{
			get { return user_DownTime; }
			set { user_DownTime = value; }
		}
		private DateTime user_InserttTime;
		/// <summary>
		/// 注册时间
		/// <summary>
		[DisplayName("注册时间")]
		public DateTime User_InserttTime{
			get { return user_InserttTime; }
			set { user_InserttTime = value; }
		}
		private string user_Email;
		/// <summary>
		/// Email
		/// <summary>
		[DisplayName("Email")]
		public string User_Email{
			get { return user_Email; }
			set { user_Email = value; }
		}
		private string user_MobilePhone;
		/// <summary>
		/// 手机号
		/// <summary>
		[DisplayName("手机号")]
		public string User_MobilePhone{
			get { return user_MobilePhone; }
			set { user_MobilePhone = value; }
		}

    }
}
