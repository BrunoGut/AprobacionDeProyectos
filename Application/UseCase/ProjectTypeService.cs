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
    public class ProjectTypeService : IProjectTypeService
    {
        private readonly IProjectTypeQuery _typeQuery;

        public ProjectTypeService(IProjectTypeQuery typeQuery)
        {
            _typeQuery = typeQuery;
        }

        public async Task<List<GenericResponse>> GetAllAsync()
        {
            var projectTypes = await _typeQuery.GetAllAsync();

            return projectTypes.Select(p => new GenericResponse
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }
    }
}
