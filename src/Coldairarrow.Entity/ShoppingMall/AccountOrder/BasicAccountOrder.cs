using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ShoppingMall.AccountOrder
{
    /// <summary>
    /// 账户订单表
    /// </summary>
    [Table("BasicAccountOrder")]
    public class BasicAccountOrder
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 否已删除
        /// </summary>
        public Boolean Deleted { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public String State { get; set; }

        /// <summary>
        /// 最后操作人id
        /// </summary>
        public String OperateId { get; set; }

    }
}