using Coldairarrow.Entity.ShoppingMall.ProfitSet;
using Coldairarrow.Entity.ShoppingMall.ProfitSet.Dto;
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

namespace Coldairarrow.Business.ShoppingMall.ProfitSet
{
    public class BasicProfitSetBusiness : BaseBusiness<BasicProfitSet>, IBasicProfitSetBusiness, ITransientDependency
    {
        public BasicProfitSetBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口
        /// <summary>
        /// 获取所有上级
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ProfitSetOutDto>> GetParentAsync(ProfitSetSearchDto input)
        {
            if (input.id.IsNullOrEmpty())
            {
                throw new BusException("id不可为空");
            }

            List<ProfitSetOutDto> result = new List<ProfitSetOutDto>();
            ProfitSetOutDto current = new ProfitSetOutDto();

            Expression<Func<BasicProfitSet, ProfitSetOutDto>> select = (a) => new ProfitSetOutDto
            {
            };

            while (true)
            {
                current = await GetIQueryable().Where(x => x.UserId.Equals(input.id)).Select(select).FirstOrDefaultAsync();
                if(current == null || current.Id.IsNullOrEmpty())
                {
                    break;
                }
                result.Add(current);
            }

            return result;

        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<ProfitSetOutDto>> GetDataListAsync(PageInput<ProfitSetSearchDto> input)
        {
            Expression<Func<BasicProfitSet, ProfitSetOutDto>> select = (a) => new ProfitSetOutDto
            {
            };
            var q = GetIQueryable().Where(x=>x.ParentId.Equals(input.Search.id)).Select(select);

            return await q.GetPageResultAsync(input);
        }

        /// <summary>
        /// 单条查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProfitSetOutDto> GetTheDataAsync(string id)
        {
            PageInput<ProfitSetSearchDto> input = new PageInput<ProfitSetSearchDto>()
            {
                Search = new ProfitSetSearchDto()
                {
                    id = id,
                }
            };
            return (await GetDataListAsync(input)).Data.FirstOrDefault();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task AddDataAsync(BasicProfitSet data)
        {
            await InsertAsync(data);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task UpdateDataAsync(BasicProfitSet data)
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