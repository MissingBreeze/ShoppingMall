using Coldairarrow.Entity.ShoppingMall.ProductOrder;

namespace Coldairarrow.Entity.ShoppingMall.ProductOrder.Dto
{
    public class ProductOrderOutDto : BasicOrder
    {
        /// <summary>
        /// 订单用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 状态名
        /// </summary>
        public string StateName { get; set; }
    }
}