
namespace Coldairarrow.Entity.ShoppingMall.ProductType.Dto
{
    public class ProductTypeSearchDto
    {
        public string id { get; set; }

        /// <summary>
        /// 上级id
        /// </summary>
        public string parentId { get; set; }
    }
}