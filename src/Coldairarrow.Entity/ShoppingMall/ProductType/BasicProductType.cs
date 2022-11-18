using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ShoppingMall.ProductType
{
    /// <summary>
    /// BasicProductType
    /// </summary>
    [Table("BasicProductType")]
    public class BasicProductType
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
        /// 类型名
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 上级分类id
        /// </summary>
        public String ParentId { get; set; }
    }
}