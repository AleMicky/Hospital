using Hospital.Api.Extensions;
using Hospital.Api.Filters;
using Hospital.Api.Middlewares;
using FluentValidation;
using Hospital.Application.Validators;
using Hospital.Application.Validators.Pacientes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiResponseFilter>();
    options.Filters.Add<ValidationFilter>();
});
builder.Services.AddValidatorsFromAssemblyContaining<CreatePacienteValidator>();
builder.Services.AddHospitalApi(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("DefaultCors");
app.UseAuthentication();
app.UseCustomExceptionMiddleware();
app.UseAuthorization();
app.MapControllers();

await app.MigrateAndSeedAsync();

app.Run();