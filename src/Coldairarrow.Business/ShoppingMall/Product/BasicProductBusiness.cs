using Coldairarrow.Entity.Enum;
using Coldairarrow.Entity.ShoppingMall.Product;
using Coldairarrow.Entity.ShoppingMall.Product.Dto;
using Coldairarrow.Entity.ShoppingMall.ProductType;
using Coldairarrow.IBusiness;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Coldairarrow.Business.ShoppingMall.Product
{
    public class BasicProductBusiness : BaseBusiness<BasicProduct>, IBasicProductBusiness, ITransientDependency
    {
        readonly IOperator _operator;
        readonly IConfiguration _configuration;
        public BasicProductBusiness(IDbAccessor db, IOperator @operator, IConfiguration configuration)
            : base(db)
        {
            _operator = @operator;
            _configuration = configuration;
        }

        #region 外部接口
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PageResult<ProductOutDto>> GetDataListAsync(PageInput<ProductSearchDto> input)
        {
            var search = input.Search;
            Expression<Func<BasicProduct, BasicProductType, ProductOutDto>> select = (a, b) => new ProductOutDto
            {
                TypeName = b.Name
            };
            select = select.BuildExtendSelectExpre();
            var q = from a in GetIQueryable().AsExpandable()
                    join b in Db.GetIQueryable< BasicProductType >() on a.TypeId equals b.Id into ab
                    from b in ab.DefaultIfEmpty()
                    select @select.Invoke(a, b);
            if (!search.id.IsNullOrEmpty())
            {
                q = q.Where(x => x.Id.Equals(search.id));
            }
            if (!search.keyword.IsNullOrEmpty())
            {
                var keyword = $"%{search.keyword}%";
                q = q.Where(x =>
                      EF.Functions.Like(x.ProductName, keyword)
                      || EF.Functions.Like(x.NickName, keyword));
            }

            var result = await q.GetPageResultAsync(input);
            if(result.Data != null && result.Data.Count > 0)
            {
                List<string> ids = result.Data.Select(x => x.Id).ToList();

                var pics = await Db.GetIQueryable<ProductPicture>().Where(x => ids.Contains(x.ProductId) 
                                && (x.PictureType == (int)PictureType.缩略图 || x.PictureType == (int)PictureType.主图)).ToListAsync();

                result.Data.ForEach(product =>
                {
                    product.ServerUrl = _configuration["WebRootUrl"];
                    product.IconPath = "";
                    var pic = pics.Where(a => a.ProductId.Equals(product.Id)).ToList();
                    var icon = pic.Where(a => a.PictureType == (int)PictureType.缩略图).FirstOrDefault();
                    if(icon != null)
                    {
                        product.IconPath = product.ServerUrl + icon.PicturePath;
                    }
                    else
                    {
                        // 没有缩略图的拿主图来
                        icon = pic.Where(a => a.PictureType == (int)PictureType.主图).FirstOrDefault();
                        if (icon != null)
                        {
                            product.IconPath = product.ServerUrl + icon.PicturePath;
                        }
                    }
                });
            }

            return result;
        }

        /// <summary>
        /// 单条查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductOutDto> GetTheDataAsync(string id)
        {
            PageInput<ProductSearchDto> input = new PageInput<ProductSearchDto>()
            {
                Search = new ProductSearchDto() 
                {
                    id = id
                },
            };
            return (await GetDataListAsync(input)).Data.First();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transactional]
        public async Task AddDataAsync(ProductOutDto data)
        {
            await InsertAsync(data);
            await SetPictureAsync(data.Id, data.IconPath, data.RootPath);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [Transactional]
        public async Task UpdateDataAsync(ProductOutDto data)
        {
            await UpdateAsync(data);
            await SetPictureAsync(data.Id, data.IconPath, data.RootPath);
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="RootPath"></param>
        /// <returns></returns>
        [Transactional]
        public async Task DeleteDataAsync(List<string> ids, string RootPath)
        {
            await DeleteAsync(ids);
            // 删除图片
            foreach (var item in ids)
            {
                await SetPictureAsync(item, "", RootPath);
            }
            await Db.DeleteAsync<ProductPicture>(x=> ids.Contains(x.ProductId));

        }

        #endregion

        #region 私有成员

        /// <summary>
        /// 添加缩略图
        /// </summary>
        /// <param name="id"></param>
        /// <param name="IconPath"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        private async Task SetPictureAsync(string id, string IconPath, string rootPath)
        {
            if (!IconPath.IsNullOrEmpty())
                IconPath = IconPath.Replace(_configuration["WebRootUrl"], "");
            var iconPic = await Db.GetIQueryable<ProductPicture>().Where(x => x.ProductId.Equals(id)
                    && x.PictureType == (int)PictureType.缩略图).ToListAsync();
            if (iconPic != null && iconPic.Count > 0)
            {
                var deleteIds = iconPic.Select(x => x.Id).ToList();
                await Db.DeleteAsync<ProductPicture>(deleteIds);

                // 删除物理盘上的图片
                foreach (var item in iconPic)
                {
                    if (IconPath != item.PicturePath)
                    {
                        if (File.Exists(rootPath + item.PicturePath))
                        {
                            DirectoryInfo directoryInfo = new DirectoryInfo(rootPath + item.PicturePath);
                            directoryInfo.Parent.Delete(true);
                            //File.Delete(_configuration["WebRootUrl"] + item.PicturePath);
                        }
                    }
                }
            }

            if (!IconPath.IsNullOrEmpty())
            {
                ProductPicture productPicture = new ProductPicture()
                {
                    Id = IdHelper.GetId(),
                    CreateTime = DateTime.Now,
                    CreatorId = _operator.UserId,
                    Deleted = false,
                    ProductId = id,
                    PicturePath = IconPath,
                    PictureType = (int)PictureType.缩略图
                };
                await Db.InsertAsync(productPicture);
            }

        }

        #endregion
    }
}