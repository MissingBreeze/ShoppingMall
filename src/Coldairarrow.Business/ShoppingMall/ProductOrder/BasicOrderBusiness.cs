using Coldairarrow.Entity;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.Enum;
using Coldairarrow.Entity.ShoppingMall.ProductOrder;
using Coldairarrow.Entity.ShoppingMall.ProductOrder.Dto;
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

namespace Coldairarrow.Business.ShoppingMall.ProductOrder
{
    public class BasicOrderBusiness : BaseBusiness<BasicOrder>, IBasicOrderBusiness, ITransientDependency
    {
        readonly IOperator _operator;

        public BasicOrderBusiness(IDbAccessor db, IOperator @operator)
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
        public async Task<PageResult<ProductOrderOutDto>> GetDataListAsync(PageInput<ProductOrderSearchDto> input)
        {
            var search = input.Search;

            Expression<Func<BasicOrder, Base_User, ProductOrderOutDto>> select = (a, b) => new ProductOrderOutDto
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

            if (!search.userName.IsNullOrEmpty())
            {
                var keyword = $"%{search.userName}%";
                q = q.Where(x =>
                      EF.Functions.Like(x.UserName, keyword));
            }

            q = q.WhereIf(!search.state.IsNullOrEmpty(), x => x.State.Equals(search.state));

            if (!search.productName.IsNullOrEmpty())
            {
                var keyword = $"%{search.productName}%";
                q = q.Where(x =>
                      EF.Functions.Like(x.ProductName, keyword));
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
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task AddDataAsync(BasicOrder data)
        {
            await InsertAsync(data);
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
            if(data == null || data.Id.IsNullOrEmpty())
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



        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task UpdateDataAsync(BasicOrder data)
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