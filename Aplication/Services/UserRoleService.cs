using Aplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserQuery _userQuery;

        public UserRoleService(IUserQuery userQuery)
        {
            _userQuery = userQuery;
        }

        public async Task<int> GetRoleByIdAsync(int userId)
        {
            return await _userQuery.GetRoleByIdAsync(userId);
        }
    }
}
