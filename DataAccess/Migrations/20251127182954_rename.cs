using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PokeTypeRelations");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "PokeTypes",
                newName: "Sprite");

            migrationBuilder.CreateTable(
                name: "DamageRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttackingTypeId = table.Column<int>(type: "int", nullable: false),
                    DefendingTypeId = table.Column<int>(type: "int", nullable: false),
                    Multiplier = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DamageRelations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DamageRelations");

            migrationBuilder.RenameColumn(
                name: "Sprite",
                table: "PokeTypes",
                newName: "Image");

            migrationBuilder.CreateTable(
                name: "PokeTypeRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttackingTypeId = table.Column<int>(type: "int", nullable: false),
                    DefendingTypeId = table.Column<int>(type: "int", nullable: false),
                    Multiplier = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PokeTypeRelations", x => x.Id);
                });
        }
    }
}
