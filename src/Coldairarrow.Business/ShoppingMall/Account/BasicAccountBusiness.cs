using Coldairarrow.Entity.Enum;
using Coldairarrow.Entity.ShoppingMall.Account;
using Coldairarrow.Entity.ShoppingMall.Account.Dto;
using Coldairarrow.IBusiness;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.Account
{
    public class BasicAccountBusiness : BaseBusiness<BasicAccount>, IBasicAccountBusiness, ITransientDependency
    {

        readonly IOperator _operator;

        public BasicAccountBusiness(IDbAccessor db, IOperator @operator)
            : base(db)
        {
            _operator = @operator;
        }

        #region 外部接口

        /// <summary>
        /// 修改用户账户余额
        /// </summary>
        /// <param name="id">用户id</param>
        /// <param name="amount">金额</param>
        /// <param name="type">类型，1充值，2提现</param>
        /// <returns></returns>
        [Transactional]
        public async Task ChangeAccountAsync(string id, decimal amount, string type)
        {
            var userAccount = GetIQueryable().Where(x => x.UserId.Equals(id)).FirstOrDefault();
            if(userAccount == null || userAccount.Id.IsNullOrEmpty())
            {
                userAccount = new BasicAccount()
                {
                    Id = IdHelper.GetId(),
                    CreateTime = DateTime.Now,
                    CreatorId = _operator.UserId,
                    Deleted = false,
                    UserId = id,
                    Balance = 0
                };
                await InsertAsync(userAccount);
            }
            if (type == ((int)OrderType.提现).ToString())
            {
                if (userAccount.Balance < amount)
                {
                    throw new BusException("该账户余额不足");
                }
                userAccount.Balance -= amount;
            }
            else if(type == ((int)OrderType.充值).ToString())
            {
                userAccount.Balance += amount;
            }
            await UpdateAsync(userAccount);
        }


        #region

        ///// <summary>
        ///// 分页查询
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async Task<PageResult<AccountOutDto>> GetDataListAsync(PageInput<AccountSearchDto> input)
        //{
        //    var q = GetIQueryable();
        //    var where = LinqHelper.True<BasicAccount>();
        //    var search = input.Search;

        //    //筛选
        //    /*if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
        //    {
        //        var newWhere = DynamicExpressionParser.ParseLambda<BasicAccount, bool>(
        //            ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
        //        where = where.And(newWhere);
        //    }*/

        //    return await q.Where(where).GetPageResultAsync(input);
        //}

        ///// <summary>
        ///// 单条查询
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task<AccountOutDto> GetTheDataAsync(string id)
        //{
        //    return await GetEntityAsync(id);
        //}

        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public async Task AddDataAsync(BasicAccount data)
        //{
        //    await InsertAsync(data);
        //}

        ///// <summary>
        ///// 修改
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public async Task UpdateDataAsync(BasicAccount data)
        //{
        //    await UpdateAsync(data);
        //}

        ///// <summary>
        ///// 批量删除
        ///// </summary>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //public async Task DeleteDataAsync(List<string> ids)
        //{
        //    await DeleteAsync(ids);
        //}

        #endregion

        #endregion

        #region 私有成员

        #endregion
    }
}