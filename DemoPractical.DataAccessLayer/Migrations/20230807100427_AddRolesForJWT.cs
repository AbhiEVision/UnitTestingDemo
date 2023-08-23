using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DemoPractical.DataAccessLayer.Migrations
{
	/// <inheritdoc />
	public partial class AddRolesForJWT : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Roles",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RoleName = table.Column<string>(type: "nvarchar(50)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Roles", x => x.Id);
				});

			migrationBuilder.InsertData(
				table: "Roles",
				columns: new[] { "Id", "RoleName" },
				values: new object[,]
				{
					{ 1, "Admin" },
					{ 2, "User" }
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Roles");
		}
	}
}
