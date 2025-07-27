using Business.Dtos.Requests.Instructor;
using Business.Dtos.Responses.Instructor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IInstructorService
    {
        Task<GetInstructorResponse> GetByIdAsync(int id);
        Task<IEnumerable<GetInstructorResponse>> GetAllAsync();
        Task<GetInstructorResponse> CreateAsync(CreateInstructorRequest request);
        Task UpdateAsync(int id, UpdateInstructorRequest request);
        Task DeleteAsync(int id);
    }
}
