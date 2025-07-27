using Business.Dtos.Requests.Applicant;
using Business.Dtos.Requests.Application;
using Business.Dtos.Responses.Applicant;
using Business.Dtos.Responses.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IApplicationService
    {
        Task<GetApplicationResponse> GetByIdAsync(int id);
        Task<IEnumerable<GetApplicationResponse>> GetAllAsync();
        Task<GetApplicationResponse> CreateAsync(CreateApplicationRequest request);
        Task UpdateAsync(int id, UpdateApplicationRequest request);
        Task DeleteAsync(int id);
    }
}
