using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IQuery
{
    public interface IAreaQuery
    {
        Task<List<Area>> GetAllAsync();
        Task<Area?> GetByIdAsync(int areaId);
    }
}
