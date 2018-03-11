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
    public class Sure_LoggerInfo
    {
		private int loggerId;
		/// <summary>
		/// 主键，自增长
		/// <summary>
		[DisplayName("主键，自增长")]
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int LoggerId{
			get { return loggerId; }
			set { loggerId = value; }
		}
		private DateTime loggerDate;
		/// <summary>
		/// 日志发生时间
		/// <summary>
		[DisplayName("日志发生时间")]
		public DateTime LoggerDate{
			get { return loggerDate; }
			set { loggerDate = value; }
		}
		private string loggerLevel;
		/// <summary>
		/// 错误级别
		/// <summary>
		[DisplayName("错误级别")]
		public string LoggerLevel{
			get { return loggerLevel; }
			set { loggerLevel = value; }
		}
		private int loggerThread;
		/// <summary>
		/// 线程ID
		/// <summary>
		[DisplayName("线程ID")]
		public int LoggerThread{
			get { return loggerThread; }
			set { loggerThread = value; }
		}
		private string loggerPosition;
		/// <summary>
		/// 错误位置
		/// <summary>
		[DisplayName("错误位置")]
		public string LoggerPosition{
			get { return loggerPosition; }
			set { loggerPosition = value; }
		}
		private string loggerMessage;
		/// <summary>
		/// 错误信息
		/// <summary>
		[DisplayName("错误信息")]
		public string LoggerMessage{
			get { return loggerMessage; }
			set { loggerMessage = value; }
		}
		private string loggerAction;
		/// <summary>
		/// 方法名称
		/// <summary>
		[DisplayName("方法名称")]
		public string LoggerAction{
			get { return loggerAction; }
			set { loggerAction = value; }
		}
		private string loggerException;
		/// <summary>
		/// 异常信息
		/// <summary>
		[DisplayName("异常信息")]
		public string LoggerException{
			get { return loggerException; }
			set { loggerException = value; }
		}

    }
}
