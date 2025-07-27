using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;

namespace Business.BusinessRules
{
    public class ApplicantBusinessRules
    {
        private readonly IGenericRepository<Applicant> _applicantRepository;
        private readonly IGenericRepository<Blacklist> _blacklistRepository;

        public ApplicantBusinessRules(
            IGenericRepository<Applicant> applicantRepository,
            IGenericRepository<Blacklist> blacklistRepository)
        {
            _applicantRepository = applicantRepository;
            _blacklistRepository = blacklistRepository;
        }

        public async Task CheckIfApplicantExists(string nationalityId, string email)
        {
            var existing = await _applicantRepository.FindAsync(a =>
                a.NationalityIdentity == nationalityId || a.Email == email);
            if (existing.Any())
                throw new Exception("Applicant with the same Nationality ID or Email already exists.");
        }

        public async Task CheckIfNotBlacklisted(int applicantId)
        {
            var blacklisted = await _blacklistRepository.FindAsync(b => b.ApplicantId == applicantId);
            if (blacklisted.Any())
                throw new Exception("Applicant is blacklisted and cannot apply.");
        }
    }
}
