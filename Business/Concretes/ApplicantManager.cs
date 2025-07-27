using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.Applicant;
using Business.Dtos.Responses.Applicant;
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
    public class ApplicantManager : IApplicantService
    {
        private readonly IGenericRepository<Applicant> _repository;
        private readonly ApplicantBusinessRules _businessRules;
        private readonly IMapper _mapper;

        public ApplicantManager(IGenericRepository<Applicant> repository,
                                ApplicantBusinessRules businessRules,
                                IMapper mapper)
        {
            _repository = repository;
            _businessRules = businessRules;
            _mapper = mapper;
        }

        public async Task<GetApplicantResponse> CreateAsync(CreateApplicantRequest request)
        {
            await _businessRules.CheckIfApplicantExists(request.NationalityIdentity, request.Email);

            var entity = _mapper.Map<Applicant>(request);
            try
            {
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException?.Message ?? ex.Message);
            }
            await _repository.SaveChangesAsync();

            return _mapper.Map<GetApplicantResponse>(entity);
        }

        public async Task<GetApplicantResponse> UpdateAsync(UpdateApplicantRequest request)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null) throw new Exception("Applicant not found");

            _mapper.Map(request, entity);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<GetApplicantResponse>(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new Exception("Applicant not found");
            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<GetApplicantResponse> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return _mapper.Map<GetApplicantResponse>(entity);
        }

        public async Task<IEnumerable<GetApplicantResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<GetApplicantResponse>>(entities);
        }

    }
}
