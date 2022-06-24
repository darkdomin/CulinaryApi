using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using CulinaryApi.Core.Entieties;
using CulinaryApi.Core.Repositories;
using CulinaryApi.Infrastructure.Authorization;
using CulinaryApi.Infrastructure.DTO.Recipes;
using CulinaryApi.Infrastructure.DTO.Users;
using CulinaryApi.Infrastructure.Repositories;
using CulinaryApi.Infrastructure.Services;
using CulinaryApi.Infrastructure.Services.Recipes;
using CulinaryApi.Infrastructure.Services.Users;
using CulinaryApi.Infrastructure.Validators;
using CulinaryApi.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CulinaryApi;
using System.Reflection;
using CulinaryApi.Infrastructure.Services.FavoriteCollection;
using System;
using CulinaryApi.Infrastructure.Services.Filters;

var builder = WebApplication.CreateBuilder();

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddAzureWebAppDiagnostics();

//Configure Service


var jwtSettings = new JwtSettings();
builder.Configuration.GetSection("Authentication").Bind(jwtSettings);

builder.Services.AddSingleton(jwtSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//  "Bearer";
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;//"Bearer";
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//"Bearer";

}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSettings.JwtIssuer,
        ValidAudience = jwtSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.JwtKey)),
    };
});

builder.Services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<CulinaryDbContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyLocalDb"))
    );
builder.Services.AddTransient(typeof(IFilterRepository<>), typeof(FilterRepository<>));
builder.Services.AddTransient(typeof(IFilterService<>), typeof(FilterService<>));

builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

builder.Services.AddScoped<IFavoriteCollection, FavoriteCollection>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<CulinarySeeder>();
builder.Services.AddScoped<IJwtHandler, JwtHandler>();

builder.Services.AddScoped<IUserContextService, UserContextService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<RecipeQuery>, RecipeQueryValidator>();
builder.Services.AddSwaggerGen();

var testEmail = new TestEmail();
builder.Configuration.GetSection("Tester").Bind(testEmail);
builder.Services.AddSingleton(testEmail);

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("LimitedAccess", builder => builder.AddRequirements(
        new LoginRequirement(testEmail.Email)
        ));
});
builder.Services.AddScoped<IAuthorizationHandler, LoginRequirementHandler>();
builder.Services.AddCors(option =>
{
    option.AddPolicy("FrontendClient", policyBuilder =>
    policyBuilder.AllowAnyMethod()
    .AllowAnyHeader()
    .WithOrigins(builder.Configuration["AllowedOrigins"])
    );
});

// ----- Configure ----- //

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<CulinarySeeder>();

app.UseCors("FrontendClient");

await seeder.SeedAsync();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Culinary Api");
});

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();





      
  