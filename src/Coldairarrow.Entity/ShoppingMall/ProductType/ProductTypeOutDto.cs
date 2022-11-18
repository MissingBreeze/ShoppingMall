using Coldairarrow.Entity.ShoppingMall.ProductType;
using Coldairarrow.Util;

namespace Coldairarrow.Entity.ShoppingMall.ProductType.Dto
{
    public class ProductTypeOutDto : TreeModel
    {
        public object children { get => Children; }
        public string title { get => Text; }
        public string value { get => Id; }
        public string key { get => Id; }
    }
}