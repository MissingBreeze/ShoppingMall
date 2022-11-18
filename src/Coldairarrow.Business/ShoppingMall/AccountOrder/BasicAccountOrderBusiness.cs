using Coldairarrow.Business.ShoppingMall.Account;
using Coldairarrow.Entity;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.Enum;
using Coldairarrow.Entity.ShoppingMall.AccountOrder;
using Coldairarrow.Entity.ShoppingMall.AccountOrder.Dto;
using Coldairarrow.IBusiness;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.AccountOrder
{
    public class BasicAccountOrderBusiness : BaseBusiness<BasicAccountOrder>, IBasicAccountOrderBusiness, ITransientDependency
    {

        readonly IOperator _operator;
        readonly IBasicAccountBusiness _accountBusiness;

        public BasicAccountOrderBusiness(IDbAccessor db, IOperator @operator, IBasicAccountBusiness basicAccountBusiness)
            : base(db)
        {
            _operator = @operator;
            _accountBusiness = basicAccountBusiness;
        }

        #region 外部接口
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<AccountOrderOutDto>> GetDataListAsync(PageInput<AccountOrderSearchDto> input)
        {
            var where = LinqHelper.True<BasicAccountOrder>();
            var search = input.Search;
            Expression<Func<BasicAccountOrder, Base_User, AccountOrderOutDto>> select = (a, b) => new AccountOrderOutDto
            {
                UserName = b.UserName,
            };
            select = select.BuildExtendSelectExpre();

            var q = from a in GetIQueryable().AsExpandable()
                    join b in Db.GetIQueryable<Base_User>() on a.UserId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);

            if (!search.userName.IsNullOrEmpty())
            {
                var keyword = $"%{search.userName}%";
                q = q.Where(x =>
                      EF.Functions.Like(x.UserName, keyword));
            }
            q = q.WhereIf(!search.state.IsNullOrEmpty(), x => x.State.Equals(search.state));

            // 客服只获取归属于自己的数据
            if (_operator.Property.RoleName == RoleTypes.客服.ToString())
            {
                var UserIds = Db.GetIQueryable<Base_User>().Where(x => x.BelongId.Equals(_operator.UserId)).Select(x => x.Id).ToList();
                if (UserIds != null && UserIds.Count > 0)
                {
                    q = q.Where(x => UserIds.Contains(x.UserId));
                }
            }

            var result = await q.GetPageResultAsync(input);
            foreach (var item in result.Data)
            {
                var stateEnum = (OrderState)Enum.Parse(typeof(OrderState), item.State);
                item.StateName = stateEnum.ToString();

                var typeEnum = (OrderType)Enum.Parse(typeof(OrderType), item.Type);
                item.TypeName = typeEnum.ToString();
            }
            return result;
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="state">状态</param>
        /// <returns></returns>
        [Transactional]
        public async Task ChangeStateAsync(string id, string state)
        {
            var data = await GetIQueryable().Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            if (data == null || data.Id.IsNullOrEmpty())
            {
                throw new BusException("查无数据");
            }
            var stateEnum = (OrderState)Enum.Parse(typeof(OrderState), state);
            if (data.State == state)
            {
                throw new BusException("该订单已经是 " + stateEnum.ToString());
            }
            data.State = state;
            data.OperateId = _operator.UserId;
            await UpdateAsync(data);
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task AddDataAsync(BasicAccountOrder data)
        {
            await _accountBusiness.ChangeAccountAsync(data.UserId, data.Amount, data.Type);
            data.State = OrderState.已通过.ToString();
            await InsertAsync(data);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task UpdateDataAsync(BasicAccountOrder data)
        {
            await UpdateAsync(data);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        #endregion

        #region 私有成员

        #endregion
    }
}