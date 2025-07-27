using Business.Dtos.Requests.Bootcamp;
using Business.Dtos.Responses.Bootcamp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IBootcampService
    {
        Task<GetBootcampResponse> GetByIdAsync(int id);
        Task<IEnumerable<GetBootcampResponse>> GetAllAsync();
        Task<GetBootcampResponse> CreateAsync(CreateBootcampRequest request);
        Task UpdateAsync(int id, UpdateBootcampRequest request);
        Task DeleteAsync(int id);
    }
}
