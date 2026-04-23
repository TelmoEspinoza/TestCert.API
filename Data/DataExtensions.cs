using Microsoft.EntityFrameworkCore;
using TestCert.API.Models;
namespace TestCert.API.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TestCertContext>();
        dbContext.Database.Migrate();
    }
    public static void IniciateTestCertDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("TestCert");
        builder.Services.AddSqlServer<TestCertContext>(connString, optionsAction: options => options.UseSeeding((context, _) =>
        {
            if (!context.Set<Test>().Any())
            {
                context.Set<Test>().AddRange(
            new Test { KtReferenceNumber = "INV45215", Description = "MOCK INFO OF A TEST ", Total = 250.45M });

                context.SaveChanges();
            }
            if (!context.Set<Equipment>().Any())
            {
                context.Set<Equipment>().AddRange(
            new Equipment
            {
                EquipKtId = "K036543",
                EquipPlasticTagId = "RED34563",
                EquipDescription = "1L10MMG80 LOL-CON-EGH/CHN-CSLH",
                EquipActive = true,
                Price = 250M,
                TestId = 1
            });
                context.SaveChanges();
            }
        })
    );

    }
}
