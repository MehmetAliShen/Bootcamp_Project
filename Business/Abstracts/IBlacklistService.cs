using Business.Dtos.Requests.Blacklist;
using Business.Dtos.Responses.Blacklist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts
{
    public interface IBlacklistService
    {
            Task<GetBlacklistResponse> GetByIdAsync(int id);
            Task<IEnumerable<GetBlacklistResponse>> GetAllAsync();
            Task<GetBlacklistResponse> CreateAsync(CreateBlacklistRequest request);
            Task UpdateAsync(int id, UpdateBlacklistRequest request);
            Task DeleteAsync(int id);
    }
}
