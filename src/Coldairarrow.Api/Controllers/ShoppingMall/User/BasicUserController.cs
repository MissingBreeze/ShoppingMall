using Coldairarrow.Business.ShoppingMall.User;
using Coldairarrow.Entity.ShoppingMall.User;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ShoppingMall.User
{
    [Route("/ShoppingMall/User/[controller]/[action]")]
    public class BasicUserController : BaseApiController
    {
        #region DI

        public BasicUserController(IBasicUserBusiness basicUserBus)
        {
            _basicUserBus = basicUserBus;
        }

        IBasicUserBusiness _basicUserBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<BasicUser>> GetDataList(PageInput<ConditionDTO> input)
        {
            return await _basicUserBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<BasicUser> GetTheData(IdInputDTO input)
        {
            return await _basicUserBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(BasicUser data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _basicUserBus.AddDataAsync(data);
            }
            else
            {
                await _basicUserBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _basicUserBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}