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
    public class ApproverRoleService : IApproverRoleService
    {
        private readonly IApproverRoleQuery _query;

        public ApproverRoleService(IApproverRoleQuery query)
        {
            _query = query;
        }

        public async Task<List<GenericResponse>> GetAllAsync()
        {
            var roles = await _query.GetAllAsync();

            return roles.Select(r => new GenericResponse
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();
        }
    }
}
