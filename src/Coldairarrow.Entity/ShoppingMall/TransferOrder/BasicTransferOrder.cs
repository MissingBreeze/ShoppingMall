using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ShoppingMall.TransferOrder
{
    /// <summary>
    /// BasicTransferOrder
    /// </summary>
    [Table("BasicTransferOrder")]
    public class BasicTransferOrder
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
        /// 用户id
        /// </summary>
        public String UserId { get; set; }

        /// <summary>
        /// 类型。1转入，2转出
        /// </summary>
        public String Type { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// 银行名
        /// </summary>
        public String BankName { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        public String OpenBank { get; set; }

        /// <summary>
        /// 开户名
        /// </summary>
        public String OpenName { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public String BankNo { get; set; }

        /// <summary>
        /// 二维码地址
        /// </summary>
        public String QRCodePath { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public String Link { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public String State { get; set; }

        /// <summary>
        /// 转账方式，银行卡、二维码、链接
        /// </summary>
        public String TransferType { get; set; }
    }
}