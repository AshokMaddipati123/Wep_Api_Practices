using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TimeTracker.Migrations
{
    /// <inheritdoc />
    public partial class updatetablecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TASKDETAILS_USERDETAILS_USERID",
                table: "TASKDETAILS");

            migrationBuilder.DropIndex(
                name: "IX_TASKDETAILS_USERID",
                table: "TASKDETAILS");

            migrationBuilder.AddColumn<byte>(
                name: "PERMISSION",
                table: "USERDETAILS",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PERMISSION",
                table: "USERDETAILS");

            migrationBuilder.CreateIndex(
                name: "IX_TASKDETAILS_USERID",
                table: "TASKDETAILS",
                column: "USERID");

            migrationBuilder.AddForeignKey(
                name: "FK_TASKDETAILS_USERDETAILS_USERID",
                table: "TASKDETAILS",
                column: "USERID",
                principalTable: "USERDETAILS",
                principalColumn: "USERID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
