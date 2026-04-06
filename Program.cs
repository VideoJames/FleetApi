using WorkflowApi.Contracts;
using WorkflowApi.Middleware;
using WorkflowApi.Services;
using Microsoft.EntityFrameworkCore;
using WorkflowApi.Data;
using FluentValidation;
using FluentValidation.AspNetCore;
using WorkflowApi.Validators;
using WorkflowApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddValidatorsFromAssemblyContaining<UserCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserUpdateDtoValidator>();
builder.Services.AddFluentValidationAutoValidation();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Workflow API V1");
    });
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();


app.Run();

