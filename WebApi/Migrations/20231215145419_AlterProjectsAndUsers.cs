using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AlterProjectsAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "Companies",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "ProjectUser",
                columns: table => new
                {
                    EmployeesId = table.Column<long>(type: "INTEGER", nullable: false),
                    ProjectsId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUser", x => new { x.EmployeesId, x.ProjectsId });
                    table.ForeignKey(
                        name: "FK_ProjectUser_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUser_Users_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUser_ProjectsId",
                table: "ProjectUser",
                column: "ProjectsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUser");

            migrationBuilder.AlterColumn<long>(
                name: "OwnerId",
                table: "Companies",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);
        }
    }
}
