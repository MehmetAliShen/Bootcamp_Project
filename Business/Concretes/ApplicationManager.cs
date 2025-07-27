using Business.Abstracts;
using Business.Dtos.Requests.Application;
using Business.Dtos.Responses.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.BusinessRules;
using Entities;
using Repositories;

namespace Business.Concretes
{
    public class ApplicationManager : IApplicationService
    {
        private readonly IGenericRepository<Application> _repository;
        private readonly ApplicationBusinessRules _businessRules;
        private readonly IMapper _mapper;

        public ApplicationManager(IGenericRepository<Application> repository,
                                  ApplicationBusinessRules businessRules,
                                  IMapper mapper)
        {
            _repository = repository;
            _businessRules = businessRules;
            _mapper = mapper;
        }

        public async Task<GetApplicationResponse> CreateAsync(CreateApplicationRequest request)
        {
            await _businessRules.CheckIfApplicantCanApply(request.ApplicantId, request.BootcampId);

            var entity = _mapper.Map<Application>(request);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<GetApplicationResponse>(entity);
        }

        public async Task<GetApplicationResponse> UpdateAsync(UpdateApplicationRequest request)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) throw new Exception("Application not found");

            _mapper.Map(request, entity);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<GetApplicationResponse>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Application not found");
            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<GetApplicationResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetApplicationResponse>(entity);
        }

        public async Task<IEnumerable<GetApplicationResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetApplicationResponse>>(entities);
        }
        public async Task UpdateAsync(int id, UpdateApplicationRequest request)
        {
            var application = await _repository.GetByIdAsync(id);
            if (application == null)
                throw new Exception("Application not found.");

            _mapper.Map(request, application);
            _repository.Update(application);
            await _repository.SaveChangesAsync();
        }
    }
}
