using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KollusSampleMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadFileKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaContentKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChannelKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UploadDate = table.Column<int>(type: "int", nullable: true),
                    TranscodingDate = table.Column<int>(type: "int", nullable: true),
                    ChannelAddedDate = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Content");
        }
    }
}
