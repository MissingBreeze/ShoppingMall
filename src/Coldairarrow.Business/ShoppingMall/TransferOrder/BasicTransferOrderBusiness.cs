using Coldairarrow.Entity.ShoppingMall.TransferOrder;
using Coldairarrow.Entity.ShoppingMall.TransferOrder.Dto;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Coldairarrow.Entity.Enum;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity;
using Coldairarrow.IBusiness;

namespace Coldairarrow.Business.ShoppingMall.TransferOrder
{
    public class BasicTransferOrderBusiness : BaseBusiness<BasicTransferOrder>, IBasicTransferOrderBusiness, ITransientDependency
    {
        readonly IOperator _operator;
        public BasicTransferOrderBusiness(IDbAccessor db, IOperator @operator)
            : base(db)
        {
            _operator = @operator;
        }

        #region 外部接口
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<TransferOrderOutDto>> GetDataListAsync(PageInput<TransferOrderSearchDto> input)
        {
            var search = input.Search;

            Expression<Func<BasicTransferOrder, Base_User, TransferOrderOutDto>> select = (a, b) => new TransferOrderOutDto
            {
                UserName = b.UserName
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                    join b in Db.GetIQueryable<Base_User>() on a.UserId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);
            if (!search.id.IsNullOrEmpty())
            {
                q = q.Where(x => x.Id.Equals(search.id));
            }
            if (!search.type.IsNullOrEmpty())
            {
                q = q.Where(x => x.Type.Equals(search.type));
            }
            if (!search.state.IsNullOrEmpty())
            {
                q = q.Where(x => x.State.Equals(search.state));
            }
            if (!search.userName.IsNullOrEmpty())
            {
                var userName = $"%{search.userName}%";
                q = q.Where(x =>
                      EF.Functions.Like(x.UserName, userName));
            }

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

            await UpdateAsync(data);
        }

        #endregion

        #region 私有成员

        #endregion
    }
}