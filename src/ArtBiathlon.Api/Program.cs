using System.Net;
using System.Text;
using ArtBiathlon.Api.ActionFilters;
using ArtBiathlon.Api.NamingPolicies;
using ArtBiathlon.Dal.Extensions;
using ArtBiathlon.Services.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

services
    .AddControllers(opt =>
    {
        opt.Filters.Add(new ExceptionFilterAttribute());
        opt.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.InternalServerError));
        opt.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.BadRequest));
        opt.Filters.Add(new ProducesResponseTypeAttribute((int)HttpStatusCode.OK));
    })
    .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy(); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
services.AddSwaggerGen(o => { o.CustomSchemaIds(x => x.FullName); });

services
    .AddDalInfrastructure(builder.Configuration)
    .AddDalRepositories()
    .AddServiceCollection();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader() // add the allowed origins  
                .WithOrigins("http://localhost:3000");
        });
});

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value!))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.MigrateUp();
app.Run();