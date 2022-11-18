using Coldairarrow.Entity.ShoppingMall.ProductOrder;
using Coldairarrow.Entity.ShoppingMall.ProductOrder.Dto;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.ProductOrder
{
    public interface IBasicOrderBusiness
    {
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResult<ProductOrderOutDto>> GetDataListAsync(PageInput<ProductOrderSearchDto> input);

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
        Task AddDataAsync(BasicOrder data);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task UpdateDataAsync(BasicOrder data);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteDataAsync(List<string> ids);
    }
}