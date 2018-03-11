using System;

namespace Sure.PersonalProject.DataAccess.Attributes
{
    /*---------------------------------------------------------------------
    [author]:senlin.huang
    [time]:2017-8-14
    [explain]: PrimaryKeyAttribute  
    -----------------------------------------------------------------------*/

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class PrimaryKeyAttribute : Attribute
    {
        /// <summary>
        /// 无参构造
        /// </summary>
        public PrimaryKeyAttribute()
        {

        }

        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="name">名称</param>
        public PrimaryKeyAttribute(string name)
        {
            this._name = name;
        }

        private string _name;

        public virtual string Name
        {
            get { return this._name; }
            set { this._name = value; }
        }
    }
}