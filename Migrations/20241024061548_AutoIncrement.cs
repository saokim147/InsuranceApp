using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceWebApp.Migrations
{
    public partial class AutoIncrement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
            name: "NewId",
            table: "YourEntities",
            nullable: false,
            defaultValue: 0);

            migrationBuilder.Sql("UPDATE YourEntities SET NewId = Id");

            migrationBuilder.DropPrimaryKey(
            name: "PK_YourEntities",
            table: "YourEntities");

            migrationBuilder.DropColumn(
            name: "Id",
            table: "YourEntities");

            migrationBuilder.RenameColumn(
            name: "NewId",
            table: "YourEntities",
            newName: "Id");

            migrationBuilder.AddPrimaryKey(
            name: "PK_YourEntities",
            table: "YourEntities",
            column: "Id");

            migrationBuilder.AlterColumn<int>(
            name: "Id",
            table: "YourEntities",
            nullable: false,
            defaultValue: 0)
            .Annotation("SqlServer:Identity", "1, 1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
