using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoPractical.DataAccessLayer.Migrations
{
	/// <inheritdoc />
	public partial class InitialMigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Departments",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					DepartmentName = table.Column<string>(type: "nvarchar(50)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Departments", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "EmployeeTypes",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Type = table.Column<string>(type: "nvarchar(50)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EmployeeTypes", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Employees",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(50)", nullable: true),
					Email = table.Column<string>(type: "nvarchar(256)", nullable: true),
					Password = table.Column<string>(type: "nvarchar(500)", nullable: true),
					PhonenNumber = table.Column<string>(type: "nvarchar(50)", nullable: false),
					DepartmentId = table.Column<int>(type: "int", nullable: true),
					EmployeeTypeId = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Employees", x => x.Id);
					table.ForeignKey(
						name: "FK_Employees_Departments_DepartmentId",
						column: x => x.DepartmentId,
						principalTable: "Departments",
						principalColumn: "Id"
						);
					table.ForeignKey(
						name: "FK_Employees_EmployeeTypes_EmployeeTypeId",
						column: x => x.EmployeeTypeId,
						principalTable: "EmployeeTypes",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "ConractBaseEmployees",
				columns: table => new
				{
					EmployeeID = table.Column<int>(type: "int", nullable: false),
					HourlyPaid = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ConractBaseEmployees", x => x.EmployeeID);
					table.ForeignKey(
						name: "FK_ConractBaseEmployees_Employees_EmployeeID",
						column: x => x.EmployeeID,
						principalTable: "Employees",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "PermentEmployees",
				columns: table => new
				{
					EmployeeId = table.Column<int>(type: "int", nullable: false),
					Salary = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PermentEmployees", x => x.EmployeeId);
					table.ForeignKey(
						name: "FK_PermentEmployees_Employees_EmployeeId",
						column: x => x.EmployeeId,
						principalTable: "Employees",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Employees_DepartmentId",
				table: "Employees",
				column: "DepartmentId");

			migrationBuilder.CreateIndex(
				name: "IX_Employees_EmployeeTypeId",
				table: "Employees",
				column: "EmployeeTypeId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ConractBaseEmployees");

			migrationBuilder.DropTable(
				name: "PermentEmployees");

			migrationBuilder.DropTable(
				name: "Employees");

			migrationBuilder.DropTable(
				name: "Departments");

			migrationBuilder.DropTable(
				name: "EmployeeTypes");
		}
	}
}
