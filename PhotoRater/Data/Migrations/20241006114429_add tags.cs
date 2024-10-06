using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhotoRater.Data.Migrations
{
    /// <inheritdoc />
    public partial class addtags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FeedbackTag",
                columns: table => new
                {
                    FeedbacksId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackTag", x => new { x.FeedbacksId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_FeedbackTag_Feedbacks_FeedbacksId",
                        column: x => x.FeedbacksId,
                        principalTable: "Feedbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedbackTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Tired" },
                    { 2, "Uncomfortable" },
                    { 3, "Bad Lighting" },
                    { 4, "Forced smile" },
                    { 5, "Unnatural" },
                    { 6, "Bad expression" },
                    { 7, "Too far away" },
                    { 8, "Too close up" },
                    { 9, "Too bright" },
                    { 10, "Too dark" },
                    { 11, "Overused filters" },
                    { 12, "Skin tone" },
                    { 13, "Too intense" },
                    { 14, "Too serious" },
                    { 15, "Cant see face" },
                    { 16, "Look great!" },
                    { 17, "Bad outfit" },
                    { 18, "Great outfit" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackTag_TagsId",
                table: "FeedbackTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeedbackTag");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
