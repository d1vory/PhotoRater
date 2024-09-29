using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoRater.Data.Migrations
{
    /// <inheritdoc />
    public partial class addstatustophoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "PhotosOnRate",
                type: "int",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_PhotosOnRate_StatusId",
                table: "PhotosOnRate",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_PhotosOnRate_Statuses_StatusId",
                table: "PhotosOnRate",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotosOnRate_Statuses_StatusId",
                table: "PhotosOnRate");

            migrationBuilder.DropIndex(
                name: "IX_PhotosOnRate_StatusId",
                table: "PhotosOnRate");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "PhotosOnRate");
        }
    }
}
