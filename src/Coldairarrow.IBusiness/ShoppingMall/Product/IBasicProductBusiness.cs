using Coldairarrow.Entity.ShoppingMall.Product;
using Coldairarrow.Entity.ShoppingMall.Product.Dto;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.Product
{
    public interface IBasicProductBusiness
    {
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResult<ProductOutDto>> GetDataListAsync(PageInput<ProductSearchDto> input);

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductOutDto> GetTheDataAsync(string id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task AddDataAsync(ProductOutDto data);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task UpdateDataAsync(ProductOutDto data);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="RootPath"></param>
        /// <returns></returns>
        Task DeleteDataAsync(List<string> ids, string RootPath);
    }
}