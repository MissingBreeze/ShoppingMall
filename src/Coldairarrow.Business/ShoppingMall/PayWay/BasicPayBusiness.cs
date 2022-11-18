using Coldairarrow.Entity.Enum;
using Coldairarrow.Entity.ShoppingMall.PayWay;
using Coldairarrow.Entity.ShoppingMall.PayWay.Dto;
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

namespace Coldairarrow.Business.ShoppingMall.PayWay
{
    public class BasicPayBusiness : BaseBusiness<BasicPay>, IBasicPayBusiness, ITransientDependency
    {
        readonly IOperator _operator;
        readonly IConfiguration _configuration;
        public BasicPayBusiness(IDbAccessor db, IOperator @operator, IConfiguration configuration)
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
        public async Task<PageResult<PayWayOutDto>> GetDataListAsync(PageInput<PayWaySearchDto> input)
        {
            var search = input.Search;
            Expression<Func<BasicPay, PayWayOutDto>> select = (a) => new PayWayOutDto
            {

            };
            select = select.BuildExtendSelectExpre();
            var q = GetIQueryable().WhereIf(!search.id.IsNullOrEmpty(), x => x.Id.Equals(search.id)).Select(select);
            if (!search.name.IsNullOrEmpty())
            {
                var keyword = $"%{search.name}%";
                q = q.Where(x =>
                      EF.Functions.Like(x.Name, keyword));
            }
            var result = await q.GetPageResultAsync(input);

            if (result != null && result.Data != null && result.Data.Count > 0)
            {
                result.Data.ForEach(x =>
                {
                    var a = (PayType)Enum.Parse(typeof(PayType), x.Type);
                    var b = a.ToString();
                    x.TypeName = ((PayType)Enum.Parse(typeof(PayType), x.Type)).ToString();
                });
            }

            return result;
        }

        /// <summary>
        /// 单条查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PayWayOutDto> GetTheDataAsync(string id)
        {
            Expression<Func<BasicPay, PayWayOutDto>> select = (a) => new PayWayOutDto
            {
            };
            select = select.BuildExtendSelectExpre();
            var q = GetIQueryable().WhereIf(!id.IsNullOrEmpty(), x => x.Id.Equals(id)).Select(select);
            var result = await q.FirstOrDefaultAsync();
            if (result != null && !result.Id.IsNullOrEmpty())
            {
                if(result.Type == ((int)PayType.二维码).ToString())
                {
                    var Pictures = await Db.GetIQueryable<PayPicture>().Where(x => x.PayId.Equals(id)).ToListAsync();
                    if (Pictures != null && Pictures.Count > 0)
                    {
                        result.QRPictures = new List<string>();
                        foreach (var item in Pictures)
                        {
                            result.QRPictures.Add(_configuration["WebRootUrl"] + item.PicturePath);
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task AddDataAsync(PayWayOutDto data)
        {
            await InsertAsync(data);
            await SetPictureAsync(data.Id, data.QRPictures, data.RootPath);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task UpdateDataAsync(PayWayOutDto data)
        {
            await UpdateAsync(data);
            await SetPictureAsync(data.Id, data.QRPictures, data.RootPath);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        ///  <param name="RootPath"></param>
        /// <returns></returns>
        public async Task DeleteDataAsync(List<string> ids, string RootPath)
        {
            await DeleteAsync(ids);
            foreach (var id in ids)
            {
                await SetPictureAsync(id, null, RootPath);
            }
           
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// 添加二维码图片
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pictures"></param>
        /// <param name="rootPath"></param>
        /// <returns></returns>
        private async Task SetPictureAsync(string id, List<string> pictures, string rootPath)
        {
            var iconPic = await Db.GetIQueryable<PayPicture>().Where(x => x.PayId.Equals(id)).ToListAsync();
            if (iconPic != null && iconPic.Count > 0)
            {
                var deleteIds = iconPic.Select(x => x.Id).ToList();
                await Db.DeleteAsync<PayPicture>(deleteIds);

                // 删除物理盘上的图片
                foreach (var item in iconPic)
                {
                    if(pictures.Find(x=> x.Replace(_configuration["WebRootUrl"],"") == item.PicturePath) == null)
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

            if (pictures != null && pictures.Count > 0)
            {
                List<PayPicture> insertData = new List<PayPicture>();
                foreach (var item in pictures)
                {
                    insertData.Add(new PayPicture()
                    {
                        Id = IdHelper.GetId(),
                        CreateTime = DateTime.Now,
                        CreatorId = _operator.UserId,
                        Deleted = false,
                        PayId = id,
                        PicturePath = item,
                    });
                }
                await Db.InsertAsync(insertData);
            }

        }

        #endregion
    }
}