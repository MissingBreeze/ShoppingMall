using Coldairarrow.Entity.ShoppingMall.ProfitSet;
using Coldairarrow.Entity.ShoppingMall.ProfitSet.Dto;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.ProfitSet
{
    public interface IBasicProfitSetBusiness
    {

        /// <summary>
        /// 获取所有上级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<ProfitSetOutDto>> GetParentAsync(ProfitSetSearchDto input);
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResult<ProfitSetOutDto>> GetDataListAsync(PageInput<ProfitSetSearchDto> input);

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProfitSetOutDto> GetTheDataAsync(string id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task AddDataAsync(BasicProfitSet data);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task UpdateDataAsync(BasicProfitSet data);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task DeleteDataAsync(List<string> ids);
    }
}