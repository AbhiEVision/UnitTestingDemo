using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoPractical.DataAccessLayer.Migrations
{
	/// <inheritdoc />
	public partial class AddBridgeTable : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "EmployeeRoles",
				columns: table => new
				{
					EmployeeId = table.Column<int>(type: "int", nullable: false),
					RoleId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EmployeeRoles", x => new { x.RoleId, x.EmployeeId });
					table.ForeignKey(
						name: "FK_EmployeeRoles_Employees_EmployeeId",
						column: x => x.EmployeeId,
						principalTable: "Employees",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_EmployeeRoles_Roles_RoleId",
						column: x => x.RoleId,
						principalTable: "Roles",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_EmployeeRoles_EmployeeId",
				table: "EmployeeRoles",
				column: "EmployeeId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "EmployeeRoles");
		}
	}
}
