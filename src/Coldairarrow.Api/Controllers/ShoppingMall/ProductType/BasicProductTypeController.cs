using Coldairarrow.Business.ShoppingMall.ProductType;
using Coldairarrow.Entity.ShoppingMall.ProductType;
using Coldairarrow.Entity.ShoppingMall.ProductType.Dto;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ShoppingMall.ProductType
{
    [Route("/ShoppingMall/ProductType/[controller]/[action]")]
    public class BasicProductTypeController : BaseApiController
    {
        #region DI

        public BasicProductTypeController(IBasicProductTypeBusiness basicProductTypeBus)
        {
            _basicProductTypeBus = basicProductTypeBus;
        }

        IBasicProductTypeBusiness _basicProductTypeBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<List<ProductTypeOutDto>> GetDataList(ProductTypeSearchDto input)
        {
            return await _basicProductTypeBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<BasicProductType> GetTheData(IdInputDTO input)
        {
            return await _basicProductTypeBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(BasicProductType data)
        {
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _basicProductTypeBus.AddDataAsync(data);
            }
            else
            {
                await _basicProductTypeBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _basicProductTypeBus.DeleteDataAsync(ids);
        }

        #endregion
    }
}