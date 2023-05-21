using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace Shipping.Infrastructure.Migrations
{
    public partial class Init_Project : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Port",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Geolocation = table.Column<Point>(type: "geography", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Port", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ship",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Geolocation = table.Column<Point>(type: "geography", nullable: false),
                    Velocity = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ship", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Port",
                columns: new[] { "Id", "CreatedDate", "Deleted", "DeletedDate", "Geolocation", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("57043e9b-a688-4cfa-b624-059453b734fe"), new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7586), false, null, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (9.34 36.34)"), "Port 6", null },
                    { new Guid("5c57a509-75ef-4472-8670-5f6e2ce81f6d"), new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7581), false, null, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (33.23 35.2)"), "Port 4", null },
                    { new Guid("60106e6f-5ac5-4a60-a42c-dc82dd2f17e1"), new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7548), false, null, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (76.2 75.2)"), "Port 2", null },
                    { new Guid("90255099-c9cf-4d51-ad2c-4dd000e8d555"), new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7577), false, null, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (32.23 33.4)"), "Port 3", null },
                    { new Guid("c7285312-6f71-4053-bca7-4e606909010a"), new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7583), false, null, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (1.97 32.2)"), "Port 5", null },
                    { new Guid("d7a50733-d4ce-4564-a4fd-4355d6479268"), new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7535), false, null, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (11 23.7)"), "Port 1", null }
                });

            migrationBuilder.InsertData(
                table: "Ship",
                columns: new[] { "Id", "CreatedDate", "Deleted", "DeletedDate", "Geolocation", "Name", "UpdatedDate", "Velocity" },
                values: new object[,]
                {
                    { new Guid("37524fb3-c2c9-414a-b683-e9c5d7ef5cf4"), new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7454), false, null, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (11.2 23.2)"), "Ship 3", null, 32L },
                    { new Guid("41ccf018-f498-41db-9b83-36b645f60afd"), new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7450), false, null, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (4.2 78.2)"), "Ship 2", null, 40L },
                    { new Guid("7b259406-2e49-413e-af5d-ef16015c876a"), new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7457), false, null, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (27.22 13.2)"), "Ship 4", null, 45L },
                    { new Guid("e83f16d2-e0fb-41c5-87b6-ec79a529b17a"), new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7377), false, null, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (68.2 88)"), "Ship 1", null, 30L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Port");

            migrationBuilder.DropTable(
                name: "Ship");
        }
    }
}
