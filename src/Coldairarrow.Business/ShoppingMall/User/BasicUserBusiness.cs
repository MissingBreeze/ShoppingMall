using Coldairarrow.Entity.ShoppingMall.User;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.User
{
    /// <summary>
    /// 用户相关
    /// </summary>
    public class BasicUserBusiness : BaseBusiness<BasicUser>, IBasicUserBusiness, ITransientDependency
    {
        public BasicUserBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<BasicUser>> GetDataListAsync(PageInput<ConditionDTO> input)
        {
            var q = GetIQueryable();
            var where = LinqHelper.True<BasicUser>();
            var search = input.Search;

            //筛选
            if (!search.Condition.IsNullOrEmpty() && !search.Keyword.IsNullOrEmpty())
            {
                var newWhere = DynamicExpressionParser.ParseLambda<BasicUser, bool>(
                    ParsingConfig.Default, false, $@"{search.Condition}.Contains(@0)", search.Keyword);
                where = where.And(newWhere);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        /// <summary>
        /// 单条查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BasicUser> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task AddDataAsync(BasicUser data)
        {
            await InsertAsync(data);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task UpdateDataAsync(BasicUser data)
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