using TestCert.API.Data;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddValidation();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.IniciateTestCertDb();
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

var app = builder.Build();

app.UseCors("AllowTestCertWeb");
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapScalarApiReference();
app.UseHttpsRedirection();
app.MigrateDb();

app.MapControllers();
app.Run();
