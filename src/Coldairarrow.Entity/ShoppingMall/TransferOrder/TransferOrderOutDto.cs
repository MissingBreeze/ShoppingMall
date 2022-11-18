using Coldairarrow.Entity.ShoppingMall.TransferOrder;

namespace Coldairarrow.Entity.ShoppingMall.TransferOrder.Dto
{
    public class TransferOrderOutDto : BasicTransferOrder
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 状态名
        /// </summary>
        public string StateName { get; set; }
    }
}