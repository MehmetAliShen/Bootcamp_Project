using Entities;
using Entities.Enums;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRules
{
    public class ApplicationBusinessRules
    {
        private readonly IGenericRepository<Application> _applicationRepository;
        private readonly IGenericRepository<Blacklist> _blacklistRepository;
        private readonly IGenericRepository<Bootcamp> _bootcampRepository;

        public ApplicationBusinessRules(
            IGenericRepository<Application> applicationRepository,
            IGenericRepository<Blacklist> blacklistRepository,
            IGenericRepository<Bootcamp> bootcampRepository)
        {
            _applicationRepository = applicationRepository;
            _blacklistRepository = blacklistRepository;
            _bootcampRepository = bootcampRepository;
        }

        public async Task CheckIfApplicantCanApply(int applicantId, int bootcampId)
        {
            
            var isBlacklisted = await _blacklistRepository.FindAsync(b => b.ApplicantId == applicantId);
            if (isBlacklisted.Any())
                throw new Exception("Applicant is blacklisted.");

            
            var bootcamp = await _bootcampRepository.GetByIdAsync(bootcampId);
            if (bootcamp == null || bootcamp.BootcampState != BootcampState.OPEN_FOR_APPLICATION)
                throw new Exception("Bootcamp is not open for application.");

            
            var existing = await _applicationRepository.FindAsync(a => a.ApplicantId == applicantId && a.BootcampId == bootcampId);
            if (existing.Any())
                throw new Exception("Applicant already applied to this bootcamp.");
        }

        public void CheckValidStatusTransition(ApplicationState oldStatus, ApplicationState newStatus)
        {
            if (oldStatus == ApplicationState.CANCELLED && newStatus == ApplicationState.PENDING)
                throw new Exception("Cannot change status from CANCELLED to PENDING.");
        }
    }
}
