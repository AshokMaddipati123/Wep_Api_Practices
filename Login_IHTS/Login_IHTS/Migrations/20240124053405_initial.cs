using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_TimeTracker.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TimePeriods");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "LOCATIONS");

            migrationBuilder.RenameColumn(
                name: "LocationName",
                table: "LOCATIONS",
                newName: "LOCATIONNAME");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "LOCATIONS",
                newName: "LOCATIONID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LOCATIONS",
                table: "LOCATIONS",
                column: "LOCATIONID");

            migrationBuilder.CreateTable(
                name: "PROJECTNAMES",
                columns: table => new
                {
                    PROJECTNAMEID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PROJECTNAME = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJECTNAMES", x => x.PROJECTNAMEID);
                });

            migrationBuilder.CreateTable(
                name: "TIMESLOTS",
                columns: table => new
                {
                    TIMESLOTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STARTTIME = table.Column<TimeSpan>(type: "time", nullable: false),
                    ENDTIME = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIMESLOTS", x => x.TIMESLOTID);
                });

            migrationBuilder.CreateTable(
                name: "USERDETAILS",
                columns: table => new
                {
                    USERID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERNAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PASSWORD = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERDETAILS", x => x.USERID);
                });

            migrationBuilder.CreateTable(
                name: "TASKDETAILS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USERID = table.Column<int>(type: "int", nullable: false),
                    USERSTORYORBUGNO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PROJECTNAMEID = table.Column<int>(type: "int", nullable: false),
                    TIMEPERIODID = table.Column<int>(type: "int", nullable: false),
                    LOCATIONID = table.Column<int>(type: "int", nullable: false),
                    TASKDESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CREATIONDATE = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASKDETAILS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TASKDETAILS_LOCATIONS_LOCATIONID",
                        column: x => x.LOCATIONID,
                        principalTable: "LOCATIONS",
                        principalColumn: "LOCATIONID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TASKDETAILS_PROJECTNAMES_PROJECTNAMEID",
                        column: x => x.PROJECTNAMEID,
                        principalTable: "PROJECTNAMES",
                        principalColumn: "PROJECTNAMEID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TASKDETAILS_TIMESLOTS_TIMEPERIODID",
                        column: x => x.TIMEPERIODID,
                        principalTable: "TIMESLOTS",
                        principalColumn: "TIMESLOTID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TASKDETAILS_USERDETAILS_USERID",
                        column: x => x.USERID,
                        principalTable: "USERDETAILS",
                        principalColumn: "USERID",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateIndex(
                name: "IX_TASKDETAILS_USERID",
                table: "TASKDETAILS",
                column: "USERID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TASKDETAILS");

            migrationBuilder.DropTable(
                name: "PROJECTNAMES");

            migrationBuilder.DropTable(
                name: "TIMESLOTS");

            migrationBuilder.DropTable(
                name: "USERDETAILS");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LOCATIONS",
                table: "LOCATIONS");

            migrationBuilder.RenameTable(
                name: "LOCATIONS",
                newName: "Locations");

            migrationBuilder.RenameColumn(
                name: "LOCATIONNAME",
                table: "Locations",
                newName: "LocationName");

            migrationBuilder.RenameColumn(
                name: "LOCATIONID",
                table: "Locations",
                newName: "LocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "LocationId");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    TaskDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimePeriodId = table.Column<int>(type: "int", nullable: false),
                    UserStoryBugNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                });

            migrationBuilder.CreateTable(
                name: "TimePeriods",
                columns: table => new
                {
                    TimePeriodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriods", x => x.TimePeriodId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordResetToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ResetTokenExpires = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }
    }
}
