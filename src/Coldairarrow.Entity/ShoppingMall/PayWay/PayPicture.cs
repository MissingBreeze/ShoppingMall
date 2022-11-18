using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ShoppingMall.PayWay
{
    /// <summary>
    /// PayPicture
    /// </summary>
    [Table("PayPicture")]
    public class PayPicture
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
        /// 支付方式Id
        /// </summary>
        public String PayId { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public String PicturePath { get; set; }

    }
}