using Coldairarrow.Business.ShoppingMall.Product;
using Coldairarrow.Entity.ShoppingMall.Product;
using Coldairarrow.Entity.ShoppingMall.Product.Dto;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.ShoppingMall.Product
{
    [Route("/ShoppingMall/Product/[controller]/[action]")]
    public class BasicProductController : BaseApiController
    {
        #region DI

        public BasicProductController(IBasicProductBusiness basicProductBus)
        {
            _basicProductBus = basicProductBus;
        }

        IBasicProductBusiness _basicProductBus { get; }

        #endregion

        #region 获取

        [HttpPost]
        public async Task<PageResult<ProductOutDto>> GetDataList(PageInput<ProductSearchDto> input)
        {
            return await _basicProductBus.GetDataListAsync(input);
        }

        [HttpPost]
        public async Task<BasicProduct> GetTheData(IdInputDTO input)
        {
            return await _basicProductBus.GetTheDataAsync(input.id);
        }

        #endregion

        #region 提交

        [HttpPost]
        public async Task SaveData(ProductOutDto data)
        {
            data.RootPath = GetRootPath();
            if (data.Id.IsNullOrEmpty())
            {
                InitEntity(data);

                await _basicProductBus.AddDataAsync(data);
            }
            else
            {
                await _basicProductBus.UpdateDataAsync(data);
            }
        }

        [HttpPost]
        public async Task DeleteData(List<string> ids)
        {
            await _basicProductBus.DeleteDataAsync(ids, GetRootPath());
        }

        #endregion
    }
}