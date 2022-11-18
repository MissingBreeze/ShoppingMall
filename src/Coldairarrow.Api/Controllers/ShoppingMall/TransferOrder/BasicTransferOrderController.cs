using Coldairarrow.Business.ShoppingMall.TransferOrder;
using Coldairarrow.Entity.ShoppingMall.TransferOrder;
using Coldairarrow.Entity.ShoppingMall.TransferOrder.Dto;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ShoppingMall.TransferOrder
{
    [Route("/ShoppingMall/TransferOrder/[controller]/[action]")]
    public class BasicTransferOrderController : BaseApiController
    {
        #region DI

        public BasicTransferOrderController(IBasicTransferOrderBusiness basicTransferOrderBus)
        {
            _basicTransferOrderBus = basicTransferOrderBus;
        }

        IBasicTransferOrderBusiness _basicTransferOrderBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<TransferOrderOutDto>> GetDataList(PageInput<TransferOrderSearchDto> input)
        {
            return await _basicTransferOrderBus.GetDataListAsync(input);
        }

        #endregion

        #region 提交

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        [HttpGet]
        public async Task ChangeState(string id, string state)
        {
            await _basicTransferOrderBus.ChangeStateAsync(id, state);
        }

        #endregion
    }
}