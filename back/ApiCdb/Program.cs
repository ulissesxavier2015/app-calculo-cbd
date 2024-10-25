using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API CDB para Obter CDI e Taxa Bancaria", Version = "v1" });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks().AddCheck("health", () => HealthCheckResult.Healthy());


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API CDB para Obter CDI e Taxa Bancaria"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/health", new HealthCheckOptions { Predicate = r => r.Name == "health" });

app.Run();
