using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ShoppingMall.Product
{
    /// <summary>
    /// BasicProduct
    /// </summary>
    [Table("BasicProduct")]
    public class BasicProduct
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
        /// 产品名
        /// </summary>
        public String ProductName { get; set; }

        /// <summary>
        /// 昵称、简称
        /// </summary>
        public String NickName { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        public Decimal Price { get; set; }

        /// <summary>
        /// 分类id
        /// </summary>
        public String TypeId { get; set; }
    }
}