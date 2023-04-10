using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Consumer.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "consumer");

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "consumer",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "consumer",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Users_Organizations_OrganizationGuid",
                        column: x => x.OrganizationGuid,
                        principalSchema: "consumer",
                        principalTable: "Organizations",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_OrganizationGuid",
                schema: "consumer",
                table: "Users",
                column: "OrganizationGuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "consumer");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "consumer");
        }
    }
}
