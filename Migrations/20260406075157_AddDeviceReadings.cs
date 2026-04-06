using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkflowApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceReadings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceReadings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BatteryVoltage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Temperature = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    RecordedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceReadings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceReadings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReadings_DeviceName",
                table: "DeviceReadings",
                column: "DeviceName");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReadings_RecordedAt",
                table: "DeviceReadings",
                column: "RecordedAt");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceReadings_UserId",
                table: "DeviceReadings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceReadings");
        }
    }
}
