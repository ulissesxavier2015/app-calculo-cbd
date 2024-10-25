using Api.Domain.Interfaces;
using Api.Infrastructure;
using Api.Infrastructure.ExternalApi;
using Api.Infrastructure.ExternalApi.Config;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configurando policy de timeout padrão
var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10);

// Add services
builder.Services.AddHttpClient<ICdbService, CdbService>()
                .AddPolicyHandler(HttpClientRetryPolicy.GetRetryPolicy())
.AddPolicyHandler(timeoutPolicy);

builder.Services.Configure<CdbConfig>(options => builder.Configuration.GetSection("CdbConfig").Bind(options));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API para Calculo CDB", Version = "v1", Contact = new OpenApiContact { Email = "ulisses_xavier@hotmail.com", Name = "Ulisses Xavier" } });
});

builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<Program>());
builder.Services.AddValidatorsFromAssemblyContaining<Program>(ServiceLifetime.Transient);
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks().AddCheck("health", () => HealthCheckResult.Healthy());
builder.Services.AddExceptionHandler<ErrorHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API para Calculo CDB"));
app.UseExceptionHandler();

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/health", new HealthCheckOptions { Predicate = r => r.Name == "health" });

app.Run();
