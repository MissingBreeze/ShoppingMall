﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.ShoppingMall.Account
{
    /// <summary>
    /// BasicAccount
    /// </summary>
    [Table("BasicAccount")]
    public class BasicAccount
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
        /// 余额
        /// </summary>
        public Decimal Balance { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public String UserId { get; set; }

    }
}