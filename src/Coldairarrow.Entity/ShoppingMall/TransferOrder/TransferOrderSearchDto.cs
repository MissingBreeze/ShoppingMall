
namespace Coldairarrow.Entity.ShoppingMall.TransferOrder.Dto
{
    public class TransferOrderSearchDto
    {
        public string id { get; set; }

        /// <summary>
        /// 账单类型，1转入，2转出
        /// </summary>
        public string type { get; set; }

        public string userName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string state { get; set; }
    }
}