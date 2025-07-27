using Business.Dtos.Requests.Applicant;
using Business.Dtos.Responses.Applicant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IApplicantService
    {
        Task<GetApplicantResponse> CreateAsync(CreateApplicantRequest request);
        Task<GetApplicantResponse> UpdateAsync(UpdateApplicantRequest request);
        Task DeleteAsync(int id);
        Task<GetApplicantResponse> GetByIdAsync(int id);
        Task<IEnumerable<GetApplicantResponse>> GetAllAsync();
    }
}
