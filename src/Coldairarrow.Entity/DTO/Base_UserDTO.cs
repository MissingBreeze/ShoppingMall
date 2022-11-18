using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;

namespace Coldairarrow.Entity
{
    [Map(typeof(Base_User))]
    public class Base_UserDTO : Base_User
    {
        
        /// <summary>
        /// 上级名称
        /// </summary>
        public string ParentName { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 所属客户账号
        /// </summary>
        public string BelongName { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal? Balance { get; set; }
    }
}
