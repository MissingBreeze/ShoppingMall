using Coldairarrow.Entity.ShoppingMall.AccountOrder;
using Coldairarrow.Entity.ShoppingMall.AccountOrder.Dto;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.AccountOrder
{
    public interface IBasicAccountOrderBusiness
    {
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResult<AccountOrderOutDto>> GetDataListAsync(PageInput<AccountOrderSearchDto> input);

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        Task ChangeStateAsync(string id, string state);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task AddDataAsync(BasicAccountOrder data);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task UpdateDataAsync(BasicAccountOrder data);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task DeleteDataAsync(List<string> ids);
    }
}