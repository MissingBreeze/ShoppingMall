using Coldairarrow.Entity.ShoppingMall.User;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.User
{
    public interface IBasicUserBusiness
    {
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PageResult<BasicUser>> GetDataListAsync(PageInput<ConditionDTO> input);

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BasicUser> GetTheDataAsync(string id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task AddDataAsync(BasicUser data);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task UpdateDataAsync(BasicUser data);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task DeleteDataAsync(List<string> ids);
    }
}