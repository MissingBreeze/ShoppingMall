using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ShoppingMall.ProductOrder
{
    /// <summary>
    /// BasicOrder
    /// </summary>
    [Table("BasicOrder")]
    public class BasicOrder
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
        /// 产品名称
        /// </summary>
        public String ProductName { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public Decimal Price { get; set; }

        /// <summary>
        /// 优惠折扣金额
        /// </summary>
        public Decimal? Discount { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public Decimal TotalAmount { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public Decimal PayAmount { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public String State { get; set; }
    }
}