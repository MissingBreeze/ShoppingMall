using Coldairarrow.Business.ShoppingMall.ProductOrder;
using Coldairarrow.Entity.ShoppingMall.ProductOrder;
using Coldairarrow.Entity.ShoppingMall.ProductOrder.Dto;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ShoppingMall.ProductOrder
{
    [Route("/ShoppingMall/ProductOrder/[controller]/[action]")]
    public class BasicOrderController : BaseApiController
    {
        #region DI

        public BasicOrderController(IBasicOrderBusiness basicOrderBus)
        {
            _basicOrderBus = basicOrderBus;
        }

        IBasicOrderBusiness _basicOrderBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<ProductOrderOutDto>> GetDataList(PageInput<ProductOrderSearchDto> input)
        {
            return await _basicOrderBus.GetDataListAsync(input);
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
            await _basicOrderBus.ChangeStateAsync(id, state);
        }

        [HttpPost]
        public async Task SaveData(BasicOrder data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _basicOrderBus.AddDataAsync(data);
            }
            else
            {
                await _basicOrderBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _basicOrderBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}