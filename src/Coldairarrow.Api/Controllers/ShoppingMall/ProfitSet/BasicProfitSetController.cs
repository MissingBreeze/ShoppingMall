using Coldairarrow.Business.ShoppingMall.ProfitSet;
using Coldairarrow.Entity.ShoppingMall.ProfitSet;
using Coldairarrow.Entity.ShoppingMall.ProfitSet.Dto;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ShoppingMall.ProfitSet
{
    [Route("/ShoppingMall/ProfitSet/[controller]/[action]")]
    public class BasicProfitSetController : BaseApiController
    {
        #region DI

        public BasicProfitSetController(IBasicProfitSetBusiness basicProfitSetBus)
        {
            _basicProfitSetBus = basicProfitSetBus;
        }

        IBasicProfitSetBusiness _basicProfitSetBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<ProfitSetOutDto>> GetDataList(PageInput<ProfitSetSearchDto> input)
        {
            return await _basicProfitSetBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<BasicProfitSet> GetTheData(IdInputDTO input)
        {
            return await _basicProfitSetBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(BasicProfitSet data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _basicProfitSetBus.AddDataAsync(data);
            }
            else
            {
                await _basicProfitSetBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _basicProfitSetBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}