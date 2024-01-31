using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TimeTracker.Migrations
{
    /// <inheritdoc />
    public partial class updatetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TASKDETAILS_LOCATIONS_LOCATIONID",
                table: "TASKDETAILS");

            migrationBuilder.DropForeignKey(
                name: "FK_TASKDETAILS_PROJECTNAMES_PROJECTNAMEID",
                table: "TASKDETAILS");

            migrationBuilder.DropForeignKey(
                name: "FK_TASKDETAILS_TIMESLOTS_TIMEPERIODID",
                table: "TASKDETAILS");

            migrationBuilder.DropTable(
                name: "TIMESLOTS");

            migrationBuilder.DropIndex(
                name: "IX_TASKDETAILS_LOCATIONID",
                table: "TASKDETAILS");

            migrationBuilder.DropIndex(
                name: "IX_TASKDETAILS_PROJECTNAMEID",
                table: "TASKDETAILS");

            migrationBuilder.DropIndex(
                name: "IX_TASKDETAILS_TIMEPERIODID",
                table: "TASKDETAILS");

            migrationBuilder.DropColumn(
                name: "TIMEPERIODID",
                table: "TASKDETAILS");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "CREATIONDATE",
                table: "TASKDETAILS",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ENDTIME",
                table: "TASKDETAILS",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "STARTTIME",
                table: "TASKDETAILS",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ENDTIME",
                table: "TASKDETAILS");

            migrationBuilder.DropColumn(
                name: "STARTTIME",
                table: "TASKDETAILS");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CREATIONDATE",
                table: "TASKDETAILS",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<int>(
                name: "TIMEPERIODID",
                table: "TASKDETAILS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TIMESLOTS",
                columns: table => new
                {
                    TIMESLOTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ENDTIME = table.Column<TimeSpan>(type: "time", nullable: false),
                    STARTTIME = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIMESLOTS", x => x.TIMESLOTID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TASKDETAILS_LOCATIONID",
                table: "TASKDETAILS",
                column: "LOCATIONID");

            migrationBuilder.CreateIndex(
                name: "IX_TASKDETAILS_PROJECTNAMEID",
                table: "TASKDETAILS",
                column: "PROJECTNAMEID");

            migrationBuilder.CreateIndex(
                name: "IX_TASKDETAILS_TIMEPERIODID",
                table: "TASKDETAILS",
                column: "TIMEPERIODID");

            migrationBuilder.AddForeignKey(
                name: "FK_TASKDETAILS_LOCATIONS_LOCATIONID",
                table: "TASKDETAILS",
                column: "LOCATIONID",
                principalTable: "LOCATIONS",
                principalColumn: "LOCATIONID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TASKDETAILS_PROJECTNAMES_PROJECTNAMEID",
                table: "TASKDETAILS",
                column: "PROJECTNAMEID",
                principalTable: "PROJECTNAMES",
                principalColumn: "PROJECTNAMEID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TASKDETAILS_TIMESLOTS_TIMEPERIODID",
                table: "TASKDETAILS",
                column: "TIMEPERIODID",
                principalTable: "TIMESLOTS",
                principalColumn: "TIMESLOTID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
