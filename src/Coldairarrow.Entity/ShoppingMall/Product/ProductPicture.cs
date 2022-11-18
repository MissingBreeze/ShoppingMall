using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ShoppingMall.Product
{
    /// <summary>
    /// ProductPicture
    /// </summary>
    [Table("ProductPicture")]
    public class ProductPicture
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
        /// 产品id
        /// </summary>
        public String ProductId { get; set; }

        /// <summary>
        /// 图片类型，1：主图；2：轮播图；3：缩略图
        /// </summary>
        public Int32 PictureType { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public String PicturePath { get; set; }

    }
}