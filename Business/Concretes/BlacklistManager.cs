using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.Blacklist;
using Business.Dtos.Responses.Blacklist;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.BusinessRules;
using Repositories;

namespace Business.Concretes
{
    public class BlacklistManager : IBlacklistService
    {
        private readonly IGenericRepository<Blacklist> _repository;
        private readonly BlacklistBusinessRules _businessRules;
        private readonly IMapper _mapper;

        public BlacklistManager(IGenericRepository<Blacklist> repository, BlacklistBusinessRules businessRules, IMapper mapper)
        {
            _repository = repository;
            _businessRules = businessRules;
            _mapper = mapper;
        }

        public async Task<GetBlacklistResponse> CreateAsync(CreateBlacklistRequest request)
        {
            await _businessRules.CheckIfCanBeBlacklisted(request.ApplicantId, request.Reason);

            var entity = _mapper.Map<Blacklist>(request);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<GetBlacklistResponse>(entity);
        }

        public async Task UpdateAsync(int id, UpdateBlacklistRequest request)
        {
            var blacklist = await _repository.GetByIdAsync(id);
            if (blacklist == null) throw new Exception("Blacklist record not found.");

            _mapper.Map(request, blacklist);
            _repository.Update(blacklist);
            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Blacklist record not found.");
            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<GetBlacklistResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetBlacklistResponse>(entity);
        }

        public async Task<IEnumerable<GetBlacklistResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetBlacklistResponse>>(entities);
        }
    }
}
