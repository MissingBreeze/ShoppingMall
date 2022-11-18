using Coldairarrow.Entity.ShoppingMall.AccountOrder;

namespace Coldairarrow.Entity.ShoppingMall.AccountOrder.Dto
{
    public class AccountOrderOutDto : BasicAccountOrder
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 状态名
        /// </summary>
        public string StateName { get; set; }
        /// <summary>
        /// 类型名
        /// </summary>
        public string TypeName { get; set; }
    }
}