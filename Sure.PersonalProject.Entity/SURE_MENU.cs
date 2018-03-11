namespace Sure.PersonalProject.Entity
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-11-27
    [explain]:SURE_MENU，菜单
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
    /// SURE_MENU，菜单
    /// </summary>
    [Serializable]
    public class SURE_MENU
    {
        private int? sURE_MENU_ID;
        /// <summary>
        /// 主键（自增长：一级菜单ID）
        /// </summary>
        [DisplayName("主键（自增长：一级菜单ID）")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? SURE_MENU_ID
        {
            get { return sURE_MENU_ID; }
            set { sURE_MENU_ID = value; }
        }
        private string sURE_MENU_NAME_CN;
        /// <summary>
        /// 菜单名称
        /// </summary>
        [DisplayName("菜单名称")]
        public string SURE_MENU_NAME_CN
        {
            get { return sURE_MENU_NAME_CN; }
            set { sURE_MENU_NAME_CN = value; }
        }
        private int sURE_HIGH_MENU_ID;
        /// <summary>
        /// 上级菜单ID（一级菜单：0）
        /// </summary>
        [DisplayName("上级菜单ID（一级菜单：0）")]
        public int SURE_HIGH_MENU_ID
        {
            get { return sURE_HIGH_MENU_ID; }
            set { sURE_HIGH_MENU_ID = value; }
        }
        private int sURE_MENU_LEVEL;
        /// <summary>
        /// 菜单级别
        /// </summary>
        [DisplayName("菜单级别")]
        public int SURE_MENU_LEVEL
        {
            get { return sURE_MENU_LEVEL; }
            set { sURE_MENU_LEVEL = value; }
        }
        private string sURE_MENU_URL;
        /// <summary>
        /// 菜单URL
        /// </summary>
        [DisplayName("菜单URL")]
        public string SURE_MENU_URL
        {
            get { return sURE_MENU_URL; }
            set { sURE_MENU_URL = value; }
        }
        private bool sURE_USE_YN;
        /// <summary>
        /// 是否启用
        /// </summary>
        [DisplayName("是否启用")]
        public bool SURE_USE_YN
        {
            get { return sURE_USE_YN; }
            set { sURE_USE_YN = value; }
        }
        private string sURE_MENU_ICON;
        /// <summary>
        /// ICON-菜单图标
        /// </summary>
        [DisplayName("ICON-菜单图标")]
        public string SURE_MENU_ICON
        {
            get { return sURE_MENU_ICON; }
            set { sURE_MENU_ICON = value; }
        }
        private string sURE_MENU_DESC;
        /// <summary>
        /// 菜单描述
        /// </summary>
        [DisplayName("菜单描述")]
        public string SURE_MENU_DESC
        {
            get { return sURE_MENU_DESC; }
            set { sURE_MENU_DESC = value; }
        }
        private DateTime sURE_CREATE_DATE;
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime SURE_CREATE_DATE
        {
            get { return sURE_CREATE_DATE; }
            set { sURE_CREATE_DATE = value; }
        }
        private string sURE_CREATE_EMP;
        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        public string SURE_CREATE_EMP
        {
            get { return sURE_CREATE_EMP; }
            set { sURE_CREATE_EMP = value; }
        }
        private DateTime? sURE_MODIFY_DATE;
        /// <summary>
        /// 修改时间
        /// </summary>
        [DisplayName("修改时间")]
        public DateTime? SURE_MODIFY_DATE
        {
            get { return sURE_MODIFY_DATE; }
            set { sURE_MODIFY_DATE = value; }
        }
        private string sURE_MODIFY_EMP;
        /// <summary>
        /// 修改人
        /// </summary>
        [DisplayName("修改人")]
        public string SURE_MODIFY_EMP
        {
            get { return sURE_MODIFY_EMP; }
            set { sURE_MODIFY_EMP = value; }
        }
        private string sURE_MENU_REMARK;
        /// <summary>
        /// 备注、说明
        /// </summary>
        [DisplayName("备注、说明")]
        public string SURE_MENU_REMARK
        {
            get { return sURE_MENU_REMARK; }
            set { sURE_MENU_REMARK = value; }
        }

    }
}
