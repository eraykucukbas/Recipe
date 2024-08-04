using Autofac;
using Autofac.Extensions.DependencyInjection;
using Recipe.Api;
using Recipe.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(Bootstrapper.ConfigureContainer);

// Add services to the container.
Bootstrapper.InitializeServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomException();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();