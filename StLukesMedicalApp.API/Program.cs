using Microsoft.EntityFrameworkCore;
using StLukesMedicalApp.API.Data;
using StLukesMedicalApp.API.Repositories.Implementation;
using StLukesMedicalApp.API.Repositories.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add & Configure Swagger
builder.Services.AddSwaggerGen();

// inject DbContect Into Application
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StLukesAppConnectionString"));
});

// inject repository into Application
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<INurseRepository, NurseRepository>();
builder.Services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// Add & Setup CORS Policy
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
