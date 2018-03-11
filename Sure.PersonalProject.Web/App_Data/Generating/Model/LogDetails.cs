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
    public class LogDetails
    {
		private int log_LogID;
		/// <summary>
		/// 主键
		/// <summary>
		[DisplayName("主键")]
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Log_LogID{
			get { return log_LogID; }
			set { log_LogID = value; }
		}
		private DateTime log_LogDate;
		/// <summary>
		/// 时间
		/// <summary>
		[DisplayName("时间")]
		public DateTime Log_LogDate{
			get { return log_LogDate; }
			set { log_LogDate = value; }
		}
		private int log_Thread;
		/// <summary>
		/// 线程ID
		/// <summary>
		[DisplayName("线程ID")]
		public int Log_Thread{
			get { return log_Thread; }
			set { log_Thread = value; }
		}
		private string log_Level;
		/// <summary>
		/// 级别
		/// <summary>
		[DisplayName("级别")]
		public string Log_Level{
			get { return log_Level; }
			set { log_Level = value; }
		}
		private string log_Logger;
		/// <summary>
		/// 错误
		/// <summary>
		[DisplayName("错误")]
		public string Log_Logger{
			get { return log_Logger; }
			set { log_Logger = value; }
		}
		private string log_Message;

		[DisplayName("")]
		public string Log_Message{
			get { return log_Message; }
			set { log_Message = value; }
		}

    }
}
