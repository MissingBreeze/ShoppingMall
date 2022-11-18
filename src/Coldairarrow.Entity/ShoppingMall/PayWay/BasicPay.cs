using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ShoppingMall.PayWay
{
    /// <summary>
    /// BasicPay
    /// </summary>
    [Table("BasicPay")]
    public class BasicPay
    {

        /// <summary>
        /// 主键
        /// </summary>
        [Key, Column(Order = 1)]
        public String Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public String CreatorId { get; set; }

        /// <summary>
        /// 否已删除
        /// </summary>
        public Boolean Deleted { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public String Direction { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public String Link { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public String BankName { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        public String OpenBank { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public String BankNo { get; set; }

        /// <summary>
        /// 开户名
        /// </summary>
        public String UserName { get; set; }
    }
}