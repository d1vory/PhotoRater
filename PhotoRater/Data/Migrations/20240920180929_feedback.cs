using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhotoRater.Data.Migrations
{
    /// <inheritdoc />
    public partial class feedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewerId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    PhotoOnRateId = table.Column<int>(type: "int", nullable: false),
                    DigitalRating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_AspNetUsers_ReviewerId",
                        column: x => x.ReviewerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Feedbacks_PhotosOnRate_PhotoOnRateId",
                        column: x => x.PhotoOnRateId,
                        principalTable: "PhotosOnRate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_PhotoOnRateId",
                table: "Feedbacks",
                column: "PhotoOnRateId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ReviewerId",
                table: "Feedbacks",
                column: "ReviewerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");
        }
    }
}
