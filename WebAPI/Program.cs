using Business.Abstracts;
using Business.BusinessRules;
using Business.Concretes;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Repositories.Data;
using System;
using AutoMapper;
using Business.Profiles;


var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(ApplicantProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ApplicationProfile).Assembly);
builder.Services.AddAutoMapper(typeof(BlacklistProfile).Assembly);
builder.Services.AddAutoMapper(typeof(BootcampProfile).Assembly);
builder.Services.AddAutoMapper(typeof(EmployeeProfile).Assembly);
builder.Services.AddAutoMapper(typeof(InstructorProfile).Assembly);


builder.Services.AddDbContext<BootcampContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(Program));

// Business Rules
builder.Services.AddScoped<ApplicantBusinessRules>();
builder.Services.AddScoped<ApplicationBusinessRules>();
builder.Services.AddScoped<BootcampBusinessRules>();
builder.Services.AddScoped<BlacklistBusinessRules>();
builder.Services.AddScoped<InstructorBusinessRules>();
builder.Services.AddScoped<EmployeeBusinessRules>();
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<Core.Security.ITokenHelper, Core.Security.TokenHelper>();
builder.Services.AddScoped<Business.Abstracts.IAuthService, Business.Concretes.AuthManager>();
builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<IGenericRepository<Instructor>, GenericRepository<Instructor>>();
builder.Services.AddScoped<IInstructorService, InstructorManager>();
builder.Services.AddScoped<BootcampContext>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));




// Services
builder.Services.AddScoped<IApplicantService, ApplicantManager>();
builder.Services.AddScoped<IApplicationService, ApplicationManager>();
builder.Services.AddScoped<IBootcampService, BootcampManager>();
builder.Services.AddScoped<IInstructorService, InstructorManager>();
builder.Services.AddScoped<IEmployeeService, EmployeeManager>();
builder.Services.AddScoped<IBlacklistService, BlacklistManager>();

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Exception Middleware
app.UseMiddleware<Core.Middleware.ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
