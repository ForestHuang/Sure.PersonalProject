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
    public class TFB_ImportInfo
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
		private string branchName;
		/// <summary>
		/// 分行名称
		/// <summary>
		[DisplayName("分行名称")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string checkBeginDate;
		/// <summary>
		/// 检查开始时间
		/// <summary>
		[DisplayName("检查开始时间")]
		public string CheckBeginDate{
			get { return checkBeginDate; }
			set { checkBeginDate = value; }
		}
		private string checkEndDate;
		/// <summary>
		/// 检查结束时间
		/// <summary>
		[DisplayName("检查结束时间")]
		public string CheckEndDate{
			get { return checkEndDate; }
			set { checkEndDate = value; }
		}
		private string checkProType;
		/// <summary>
		/// 检查项分类
		/// <summary>
		[DisplayName("检查项分类")]
		public string CheckProType{
			get { return checkProType; }
			set { checkProType = value; }
		}
		private string questionType;
		/// <summary>
		/// 问题性质
		/// <summary>
		[DisplayName("问题性质")]
		public string QuestionType{
			get { return questionType; }
			set { questionType = value; }
		}
		private string questionDes;
		/// <summary>
		/// 问题描述
		/// <summary>
		[DisplayName("问题描述")]
		public string QuestionDes{
			get { return questionDes; }
			set { questionDes = value; }
		}
		private string questionOpinion;
		/// <summary>
		/// 检查意见
		/// <summary>
		[DisplayName("检查意见")]
		public string QuestionOpinion{
			get { return questionOpinion; }
			set { questionOpinion = value; }
		}
		private string recMeasures;
		/// <summary>
		/// 整改措施
		/// <summary>
		[DisplayName("整改措施")]
		public string RecMeasures{
			get { return recMeasures; }
			set { recMeasures = value; }
		}
		private string recRecMeasuresFlag;
		/// <summary>
		/// 整改完成标志
		/// <summary>
		[DisplayName("整改完成标志")]
		public string RecRecMeasuresFlag{
			get { return recRecMeasuresFlag; }
			set { recRecMeasuresFlag = value; }
		}
		private string planedDate;
		/// <summary>
		/// 计划完成时间
		/// <summary>
		[DisplayName("计划完成时间")]
		public string PlanedDate{
			get { return planedDate; }
			set { planedDate = value; }
		}
		private string propulsion;
		/// <summary>
		/// 推进情况
		/// <summary>
		[DisplayName("推进情况")]
		public string Propulsion{
			get { return propulsion; }
			set { propulsion = value; }
		}
		private string recMeasuresStatus;
		/// <summary>
		/// 整改状态
		/// <summary>
		[DisplayName("整改状态")]
		public string RecMeasuresStatus{
			get { return recMeasuresStatus; }
			set { recMeasuresStatus = value; }
		}
		private string finalDate;
		/// <summary>
		/// 最终完成时间
		/// <summary>
		[DisplayName("最终完成时间")]
		public string FinalDate{
			get { return finalDate; }
			set { finalDate = value; }
		}
		private string evaInstitution;
		/// <summary>
		/// 评估机构
		/// <summary>
		[DisplayName("评估机构")]
		public string EvaInstitution{
			get { return evaInstitution; }
			set { evaInstitution = value; }
		}
		private string evaConclusion;
		/// <summary>
		/// 评估结论
		/// <summary>
		[DisplayName("评估结论")]
		public string EvaConclusion{
			get { return evaConclusion; }
			set { evaConclusion = value; }
		}
		private string evaNotes;
		/// <summary>
		/// 评估说明
		/// <summary>
		[DisplayName("评估说明")]
		public string EvaNotes{
			get { return evaNotes; }
			set { evaNotes = value; }
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
		private string branchName;
		/// <summary>
		/// 分行名称
		/// <summary>
		[DisplayName("分行名称")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;
		/// <summary>
		/// 所属分行名称
		/// <summary>
		[DisplayName("所属分行名称")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;
		/// <summary>
		/// 分行名称
		/// <summary>
		[DisplayName("分行名称")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string operator;

		[DisplayName("")]
		public string Operator{
			get { return operator; }
			set { operator = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}
		private string branchName;

		[DisplayName("")]
		public string BranchName{
			get { return branchName; }
			set { branchName = value; }
		}

    }
}
