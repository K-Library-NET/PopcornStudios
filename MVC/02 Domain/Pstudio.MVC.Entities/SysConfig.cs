using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pstudio.MVC.Entities
{
    /// <summary>
    /// 系统配置表
    /// </summary>
    public class SysConfig
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public int SysConfigId
        {
            get;
            set;
        }

        /// <summary>
        /// 配置项名称
        /// </summary>
        public string ConfigName
        {
            get;
            set;
        }

        /// <summary>
        /// 配置项键值
        /// </summary>
        public string ConfigKey
        {
            get;
            set;
        }

        /// <summary>
        /// 配置值
        /// </summary>
        public string ConfigValue
        {
            get;
            set;
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get;
            set;
        }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public long UTIME
        {
            get;
            set;
        }
    }
}
