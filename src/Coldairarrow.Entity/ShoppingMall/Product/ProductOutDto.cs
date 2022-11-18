using Coldairarrow.Entity.ShoppingMall.Product;

namespace Coldairarrow.Entity.ShoppingMall.Product.Dto
{
    public class ProductOutDto : BasicProduct
    {
        /// <summary>
        /// 缩略图路径
        /// </summary>
        public string IconPath { get; set; }

        /// <summary>
        /// 物理地址
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// 请求图片的服务器地址
        /// </summary>
        public string ServerUrl { get; set; }

        public string TypeName { get; set; }
    }
}