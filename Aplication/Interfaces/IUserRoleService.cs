﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces
{
    public interface IUserRoleService
    {
        Task<int> GetRoleByIdAsync(int userId);
    }
}
