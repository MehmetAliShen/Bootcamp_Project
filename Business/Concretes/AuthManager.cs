using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests.Applicant;
using Business.Dtos.Requests.Login;
using Business.Dtos.Responses.Applicant;
using Business.Security;
using Core.Security;
using Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;


namespace Business.Concretes
{
    public class AuthManager : IAuthService
    {
        private readonly IGenericRepository<Applicant> _applicantRepository;
        private readonly IMapper _mapper;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(
            IGenericRepository<Applicant> applicantRepository,
            IMapper mapper,
            ITokenHelper tokenHelper)
        {
            _applicantRepository = applicantRepository;
            _mapper = mapper;
            _tokenHelper = tokenHelper;
        }

        public async Task<Applicant> RegisterAsync(CreateApplicantRequest request, string password)
        {
            
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var applicant = _mapper.Map<Applicant>(request);
            applicant.PasswordHash = System.Text.Encoding.UTF8.GetBytes(passwordHash);
            applicant.PasswordSalt = Array.Empty<byte>();

            await _applicantRepository.AddAsync(applicant);
            try
            {
                await _applicantRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Inner exception varsa onun mesajını alalım
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                throw new Exception(innerMessage);
            }


            return applicant;
        }

        public async Task<Applicant> LoginAsync(LoginRequest request)
        {
            var applicants = await _applicantRepository.FindAsync(a => a.Email == request.Email);
            var applicant = applicants.FirstOrDefault();

            if (applicant == null || !BCrypt.Net.BCrypt.Verify(request.Password, System.Text.Encoding.UTF8.GetString(applicant.PasswordHash)))
                throw new Exception("Invalid email or password.");

            return applicant;
        }

        public string CreateAccessToken(Applicant applicant)
        {
            var tokenUser = new TokenUserModel
            {
                Id = applicant.Id,
                Email = applicant.Email,
                FullName = $"{applicant.FirstName} {applicant.LastName}"
            };

            return _tokenHelper.CreateToken(tokenUser);
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
