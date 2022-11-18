using Coldairarrow.Entity.ShoppingMall.PayWay;
using Coldairarrow.Entity.ShoppingMall.PayWay.Dto;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.PayWay
{
    public interface IBasicPayBusiness
    {
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResult<PayWayOutDto>> GetDataListAsync(PageInput<PayWaySearchDto> input);

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<PayWayOutDto> GetTheDataAsync(string id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task AddDataAsync(PayWayOutDto data);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task UpdateDataAsync(PayWayOutDto data);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="RootPath"></param>
        /// <returns></returns>
        Task DeleteDataAsync(List<string> ids, string RootPath);
    }
}