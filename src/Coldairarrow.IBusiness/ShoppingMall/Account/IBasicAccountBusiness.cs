using Coldairarrow.Entity.ShoppingMall.Account;
using Coldairarrow.Entity.ShoppingMall.Account.Dto;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.Account
{
    public interface IBasicAccountBusiness
    {
        /// <summary>
        /// 修改用户账户余额
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="amount">金额</param>
        /// <param name="type">类型，1充值，2提现</param>
        /// <returns></returns>
        Task ChangeAccountAsync(string id, decimal amount, string type);


        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //Task<PageResult<AccountOutDto>> GetDataListAsync(PageInput<AccountSearchDto> input);

        /// <summary>
        /// 获取单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Task<AccountOutDto> GetTheDataAsync(string id);

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //Task AddDataAsync(BasicAccount data);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //Task UpdateDataAsync(BasicAccount data);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //Task DeleteDataAsync(List<string> ids);
    }
}