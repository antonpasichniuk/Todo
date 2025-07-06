using DataInjection = Todo.Data.DependencyInjection;
using ApplicationInjection = Todo.Application.DependencyInjection;
using Todo.Application.Services.Interfaces;
using Todo.Web.Infrastructure;
using Microsoft.AspNetCore.Authentication;


var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;

// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

DataInjection.AddDbContext(services, configuration);
DataInjection.AddRepositories(services);

ApplicationInjection.AddServices(services);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserContext, FakeUserContext>();

services.AddAuthentication("FakeScheme")
    .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>("FakeScheme", options => { });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
