using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DemoPractical.DataAccessLayer.Migrations
{
	/// <inheritdoc />
	public partial class SeedingData : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "PhonenNumber",
				table: "Employees",
				newName: "PhoneNumber");

			migrationBuilder.InsertData(
				table: "Departments",
				columns: new[] { "Id", "DepartmentName" },
				values: new object[,]
				{
					{ 1, "HR" },
					{ 2, "JAVA" },
					{ 3, "DOTNET" },
					{ 4, "IT" }
				});

			migrationBuilder.InsertData(
				table: "EmployeeTypes",
				columns: new[] { "Id", "Type" },
				values: new object[,]
				{
					{ 1, "Permanent" },
					{ 2, "Contract" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "Departments",
				keyColumn: "Id",
				keyValue: 1);

			migrationBuilder.DeleteData(
				table: "Departments",
				keyColumn: "Id",
				keyValue: 2);

			migrationBuilder.DeleteData(
				table: "Departments",
				keyColumn: "Id",
				keyValue: 3);

			migrationBuilder.DeleteData(
				table: "Departments",
				keyColumn: "Id",
				keyValue: 4);

			migrationBuilder.DeleteData(
				table: "EmployeeTypes",
				keyColumn: "Id",
				keyValue: 1);

			migrationBuilder.DeleteData(
				table: "EmployeeTypes",
				keyColumn: "Id",
				keyValue: 2);

			migrationBuilder.RenameColumn(
				name: "PhoneNumber",
				table: "Employees",
				newName: "PhonenNumber");
		}
	}
}
