using Coldairarrow.Business.ShoppingMall.PayWay;
using Coldairarrow.Entity.ShoppingMall.PayWay;
using Coldairarrow.Entity.ShoppingMall.PayWay.Dto;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ShoppingMall.PayWay
{
    [Route("/ShoppingMall/PayWay/[controller]/[action]")]
    public class BasicPayController : BaseApiController
    {
        #region DI

        public BasicPayController(IBasicPayBusiness basicPayBus)
        {
            _basicPayBus = basicPayBus;
        }

        IBasicPayBusiness _basicPayBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<PayWayOutDto>> GetDataList(PageInput<PayWaySearchDto> input)
        {
            return await _basicPayBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<BasicPay> GetTheData(IdInputDTO input)
        {
            return await _basicPayBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(PayWayOutDto data)
        {
            data.RootPath = GetRootPath();
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _basicPayBus.AddDataAsync(data);
            }
            else
            {
                await _basicPayBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _basicPayBus.DeleteDataAsync(ids, GetRootPath());
        }

        #endregion
    }
}