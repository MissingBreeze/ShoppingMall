using Coldairarrow.Entity.ShoppingMall.ProductType;
using Coldairarrow.Entity.ShoppingMall.ProductType.Dto;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.ProductType
{
    public interface IBasicProductTypeBusiness
    {
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<ProductTypeOutDto>> GetDataListAsync(ProductTypeSearchDto input);

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BasicProductType> GetTheDataAsync(string id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task AddDataAsync(BasicProductType data);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task UpdateDataAsync(BasicProductType data);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task DeleteDataAsync(List<string> ids);
    }
}