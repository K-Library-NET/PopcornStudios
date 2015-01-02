using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities
{
    /// <summary>
    /// 互市点/互市区/城市/县城等
    /// </summary>
    public class Organization
    {
        /// <summary>
        /// ID，自增
        /// </summary>
        public int OrganizationId
        {
            get;
            set;
        }

        /// <summary>
        /// 机构名称
        /// 互市点/互市区/城市/县城等
        /// </summary>
        public string Location
        {
            get;
            set;
        }

        /// <summary>
        /// 上级机构ID
        /// </summary>
        public int ParentId
        {
            get;
            set;
        }

        /// <summary>
        /// 互市地点编号（用于数据权限过滤）
        /// </summary>
        public int LocationCode
        {
            get;
            set;
        }

        /// <summary>
        /// 下级互市地点编号范围起始值（闭区间）
        /// </summary>
        public int ChildLocationCodeStart
        {
            get;
            set;
        }

        /// <summary>
        /// 下级互市地点编号范围结束值（闭区间）
        /// </summary>
        public int ChildLocationCodeEnd
        {
            get;
            set;
        }
    }
}
