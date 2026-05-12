using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestCert.API.data.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerTable4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Tests_TestId",
                table: "Equipments");

            migrationBuilder.DropIndex(
                name: "IX_Equipments_TestId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Equipments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TestId",
                table: "Equipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_TestId",
                table: "Equipments",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Tests_TestId",
                table: "Equipments",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
