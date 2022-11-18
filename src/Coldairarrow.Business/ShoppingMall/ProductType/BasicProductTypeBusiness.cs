using Coldairarrow.Entity.ShoppingMall.ProductType;
using Coldairarrow.Entity.ShoppingMall.ProductType.Dto;
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

namespace Coldairarrow.Business.ShoppingMall.ProductType
{
    public class BasicProductTypeBusiness : BaseBusiness<BasicProductType>, IBasicProductTypeBusiness, ITransientDependency
    {
        public BasicProductTypeBusiness(IDbAccessor db)
            : base(db)
        {
        }

        #region 外部接口
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<List<ProductTypeOutDto>> GetDataListAsync(ProductTypeSearchDto input)
        {
            var where = LinqHelper.True<BasicProductType>();
            if (!input.parentId.IsNullOrEmpty())
                where = where.And(x => x.ParentId == input.parentId);

            var list = await GetIQueryable().Where(where).ToListAsync();
            var treeList = list
                .Select(x => new ProductTypeOutDto
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Text = x.Name,
                    Value = x.Id
                }).ToList();

            return TreeHelper.BuildTree(treeList);
        }

        /// <summary>
        /// 单条查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BasicProductType> GetTheDataAsync(string id)
        {
            return await GetEntityAsync(id);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task AddDataAsync(BasicProductType data)
        {
            await InsertAsync(data);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task UpdateDataAsync(BasicProductType data)
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