using Microsoft.EntityFrameworkCore.Migrations;

namespace FacilityMVCnew.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacilityStatuses",
                columns: table => new
                {
                    FacilityStatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacilityStatuses", x => x.FacilityStatusId);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    FacilityStatusId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facilities_FacilityStatuses_FacilityStatusId",
                        column: x => x.FacilityStatusId,
                        principalTable: "FacilityStatuses",
                        principalColumn: "FacilityStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_FacilityStatusId",
                table: "Facilities",
                column: "FacilityStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "FacilityStatuses");
        }
    }
}
