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
    public class ApprovalStatusService : IApprovalStatusService
    {
        private readonly IApprovalStatusQuery _query;

        public ApprovalStatusService(IApprovalStatusQuery query)
        {
            _query = query;
        }

        public async Task<List<GenericResponse>> GetAllAsync()
        {
            var statuses = await _query.GetAllAsync();
            return statuses.Select(s => new GenericResponse
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }
    }
}
