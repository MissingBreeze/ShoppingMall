using Coldairarrow.Business.ShoppingMall.AccountOrder;
using Coldairarrow.Entity.ShoppingMall.AccountOrder;
using Coldairarrow.Entity.ShoppingMall.AccountOrder.Dto;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ShoppingMall.AccountOrder
{
    [Route("/ShoppingMall/AccountOrder/[controller]/[action]")]
    public class BasicAccountOrderController : BaseApiController
    {
        #region DI

        public BasicAccountOrderController(IBasicAccountOrderBusiness basicAccountOrderBus)
        {
            _basicAccountOrderBus = basicAccountOrderBus;
        }

        IBasicAccountOrderBusiness _basicAccountOrderBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<AccountOrderOutDto>> GetDataList(PageInput<AccountOrderSearchDto> input)
        {
            return await _basicAccountOrderBus.GetDataListAsync(input);
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
            await _basicAccountOrderBus.ChangeStateAsync(id, state);
        }

        [HttpPost]
        public async Task SaveData(BasicAccountOrder data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _basicAccountOrderBus.AddDataAsync(data);
            }
            else
            {
                await _basicAccountOrderBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _basicAccountOrderBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}