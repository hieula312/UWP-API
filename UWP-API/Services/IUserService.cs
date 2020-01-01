using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_API.Model;

namespace UWP_API.Services
{
    interface IUserService
    {
        Task<User> Create(User user);
        Task<List<User>> GetList();
        Task<User> GetDetail(string id);
        Task<User> Update(User user);
        Task<bool> Delete(string rollNumber);

    }
}
