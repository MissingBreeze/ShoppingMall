using Coldairarrow.Entity;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Base_Manage
{
    public interface IBase_UserBusiness
    {
        Task<PageResult<Base_UserDTO>> GetDataListAsync(PageInput<Base_UsersInputDTO> input);
        Task<List<SelectOption>> GetOptionListAsync(OptionListInputDTO input);
        Task<Base_UserDTO> GetTheDataAsync(string id);
        Task AddDataAsync(UserEditInputDTO input);
        Task UpdateDataAsync(UserEditInputDTO input);
        Task DeleteDataAsync(List<string> ids);

        /// <summary>
        /// 供给缓存使用的获取用户数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Base_UserDTO> GetUser(string id);
    }

    [Map(typeof(Base_User))]
    public class UserEditInputDTO : Base_User
    {
        public string newPwd { get; set; }
        public string RoleId { get; set; }
    }

    public class Base_UsersInputDTO
    {
        public bool all { get; set; }
        public string userId { get; set; }
        public string keyword { get; set; }
        /// <summary>
        /// 要过滤的角色，1管理员，2客服，3用户
        /// </summary>
        public string roleType { get; set; }
    }
}