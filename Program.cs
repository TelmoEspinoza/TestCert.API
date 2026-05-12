using TestCert.API.Data;
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using TestCert.API.Services;
using TestCert.API.Repositories;
using TestCert.API.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddValidation();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
//Clean way to introduce data register
//builder.Services.AddDbContext<TestCertContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestCert")));
//Introduce data register contex plass seeding
builder.IniciateTestCertDb();

//Authenticating to Azure Ad
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("TestCertAPI.Read", policy =>
    {
        policy.RequireScope("TestCertAPI.Read");
    });
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowTestCertWeb", policy =>
    {
        policy.AllowAnyHeader()
              .AllowCredentials()
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:4200", "http://127.0.0.1:4200"); // Replace with your React app's URL;
    });
});
  builder.Services.AddScoped<ITestRepository, TestRepository>();
  builder.Services.AddScoped<IEquipmentRepository,EquipmentRepository>();
  builder.Services.AddScoped<ICustomerRepository,  CustomerRepository >();

  builder.Services.AddScoped<ITestService, TestService>();
  builder.Services.AddScoped<IEquipmentService, EquipmentService>();
  
  
var app = builder.Build();

app.UseCors("AllowTestCertWeb");
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapScalarApiReference();
app.UseHttpsRedirection();
app.MigrateDb();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.Run();
