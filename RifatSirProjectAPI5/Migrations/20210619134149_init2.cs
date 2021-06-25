using Microsoft.EntityFrameworkCore.Migrations;

namespace RifatSirProjectAPI5.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isApprove",
                table: "StudentBasicInfoTable");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "StudentBasicInfoTable",
                newName: "HallName");

            migrationBuilder.AddColumn<string>(
                name: "BatchNo",
                table: "StudentBasicInfoTable",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "StudentAuthTable",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BatchNo",
                table: "StudentBasicInfoTable");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "StudentAuthTable");

            migrationBuilder.RenameColumn(
                name: "HallName",
                table: "StudentBasicInfoTable",
                newName: "password");

            migrationBuilder.AddColumn<bool>(
                name: "isApprove",
                table: "StudentBasicInfoTable",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
