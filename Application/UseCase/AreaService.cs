using Application.Dtos.Response;
using Application.Interfaces.IQuery;
using Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCase
{
    public class AreaService : IAreaService
    {
        private readonly IAreaQuery _areaQuery;

        public AreaService(IAreaQuery areaQuery)
        {
            _areaQuery = areaQuery;
        }

        public async Task<List<GenericResponse>> GetAllAsync()
        {
            var areas = await _areaQuery.GetAllAsync();

            return areas.Select(a => new GenericResponse
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
        }
    }
}
