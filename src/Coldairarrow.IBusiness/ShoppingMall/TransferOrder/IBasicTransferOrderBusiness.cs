using Coldairarrow.Entity.ShoppingMall.TransferOrder;
using Coldairarrow.Entity.ShoppingMall.TransferOrder.Dto;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.TransferOrder
{
    public interface IBasicTransferOrderBusiness
    {
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResult<TransferOrderOutDto>> GetDataListAsync(PageInput<TransferOrderSearchDto> input);

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        Task ChangeStateAsync(string id, string state);
    }
}