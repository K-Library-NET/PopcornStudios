using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities
{
    /// <summary>
    /// 系统权限
    /// </summary>
    public class Privilege
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int PrivilegeId
        {
            get;
            set;
        }

        /// <summary>
        /// 系统功能名称
        /// </summary>
        public string FunctionName
        {
            get;
            set;
        }

        /// <summary>
        /// 互市地点编号
        /// </summary>
        public int LocationCode
        {
            get;
            set;
        }

        /// <summary>
        /// 一级模块标识
        /// </summary>
        public string Area
        {
            get;
            set;
        }

        /// <summary>
        /// 二级模块标识
        /// </summary>
        public string Controller
        {
            get;
            set;
        }

        /// <summary>
        /// 模块功能标识
        /// </summary>
        public string Action
        {
            get;
            set;
        }
    }
}
