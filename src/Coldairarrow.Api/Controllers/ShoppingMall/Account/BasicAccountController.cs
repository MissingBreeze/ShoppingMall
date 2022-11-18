using Coldairarrow.Business.ShoppingMall.Account;
using Coldairarrow.Entity.ShoppingMall.Account;
using Coldairarrow.Entity.ShoppingMall.Account.Dto;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ShoppingMall.Account
{
    [Route("/ShoppingMall/Account/[controller]/[action]")]
    public class BasicAccountController : BaseApiController
    {
        #region DI

        public BasicAccountController(IBasicAccountBusiness basicAccountBus)
        {
            _basicAccountBus = basicAccountBus;
        }

        IBasicAccountBusiness _basicAccountBus { get; }

        #endregion

        #region 获取

        //[HttpPost]
        //public async Task<PageResult<AccountOutDto>> GetDataList(PageInput<AccountSearchDto> input)
        //{
        //    return await _basicAccountBus.GetDataListAsync(input);
        //}

        //[HttpPost]
        //public async Task<BasicAccount> GetTheData(IdInputDTO input)
        //{
        //    return await _basicAccountBus.GetTheDataAsync(input.id);
        //}

        #endregion

        #region 提交

        //[HttpPost]
        //public async Task SaveData(BasicAccount data)
        //{
        //    if (data.Id.IsNullOrEmpty())
        //    {
        //        InitEntity(data);

        //        await _basicAccountBus.AddDataAsync(data);
        //    }
        //    else
        //    {
        //        await _basicAccountBus.UpdateDataAsync(data);
        //    }
        //}

        //[HttpPost]
        //public async Task DeleteData(List<string> ids)
        //{
        //    await _basicAccountBus.DeleteDataAsync(ids);
        //}

        #endregion
    }
}