using Coldairarrow.Entity.ShoppingMall.PayWay;
using System.Collections.Generic;

namespace Coldairarrow.Entity.ShoppingMall.PayWay.Dto
{
    public class PayWayOutDto : BasicPay
    {
        /// <summary>
        /// 图片
        /// </summary>
        public List<string> QRPictures { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { get; set; }


        /// <summary>
        /// 物理地址
        /// </summary>
        public string RootPath { get; set; }
    }
}