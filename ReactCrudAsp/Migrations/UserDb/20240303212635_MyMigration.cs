using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReactCrudAsp.Migrations.UserDb
{
    /// <inheritdoc />
    public partial class MyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    surname = table.Column<string>(type: "nvarchar(60)", nullable: false),
                    phonenumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.UniqueConstraint("UN_Email", x => x.email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
