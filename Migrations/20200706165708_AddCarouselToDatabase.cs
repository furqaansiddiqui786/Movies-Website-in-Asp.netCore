using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDB.Migrations
{
    public partial class AddCarouselToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Banner",
                table: "MoviesDb",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.CreateTable(
                name: "CarouselDb",
                columns: table => new
                {
                    bannerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Banners = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarouselDb", x => x.bannerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarouselDb");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Banner",
                table: "MoviesDb",
                type: "varbinary(max)",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }
    }
}
