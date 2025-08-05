using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeatReservation.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    venue_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    event_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_events", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reservations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "venues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SeatLimit = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    prefix = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_venues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "event_details",
                columns: table => new
                {
                    event_id = table.Column<Guid>(type: "uuid", nullable: false),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event_details", x => x.event_id);
                    table.ForeignKey(
                        name: "FK_event_details_events_event_id",
                        column: x => x.event_id,
                        principalTable: "events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservation_seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReservationId = table.Column<Guid>(type: "uuid", nullable: false),
                    seat_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reservation_seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_reservation_seats_reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "reservations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RowNumber = table.Column<int>(type: "integer", nullable: false),
                    SeatNumber = table.Column<int>(type: "integer", nullable: false),
                    VenueId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_seats_venues_VenueId",
                        column: x => x.VenueId,
                        principalTable: "venues",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_reservation_seats_ReservationId",
                table: "reservation_seats",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_seats_VenueId",
                table: "seats",
                column: "VenueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "event_details");

            migrationBuilder.DropTable(
                name: "reservation_seats");

            migrationBuilder.DropTable(
                name: "seats");

            migrationBuilder.DropTable(
                name: "events");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "venues");
        }
    }
}
