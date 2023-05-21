﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Shipping.Infrastructure.Data;

#nullable disable

namespace Shipping.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Shipping.Domain.Model.Port", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Point>("Geolocation")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Port");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d7a50733-d4ce-4564-a4fd-4355d6479268"),
                            CreatedDate = new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7535),
                            Deleted = false,
                            Geolocation = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (11 23.7)"),
                            Name = "Port 1"
                        },
                        new
                        {
                            Id = new Guid("60106e6f-5ac5-4a60-a42c-dc82dd2f17e1"),
                            CreatedDate = new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7548),
                            Deleted = false,
                            Geolocation = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (76.2 75.2)"),
                            Name = "Port 2"
                        },
                        new
                        {
                            Id = new Guid("90255099-c9cf-4d51-ad2c-4dd000e8d555"),
                            CreatedDate = new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7577),
                            Deleted = false,
                            Geolocation = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (32.23 33.4)"),
                            Name = "Port 3"
                        },
                        new
                        {
                            Id = new Guid("5c57a509-75ef-4472-8670-5f6e2ce81f6d"),
                            CreatedDate = new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7581),
                            Deleted = false,
                            Geolocation = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (33.23 35.2)"),
                            Name = "Port 4"
                        },
                        new
                        {
                            Id = new Guid("c7285312-6f71-4053-bca7-4e606909010a"),
                            CreatedDate = new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7583),
                            Deleted = false,
                            Geolocation = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (1.97 32.2)"),
                            Name = "Port 5"
                        },
                        new
                        {
                            Id = new Guid("57043e9b-a688-4cfa-b624-059453b734fe"),
                            CreatedDate = new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7586),
                            Deleted = false,
                            Geolocation = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (9.34 36.34)"),
                            Name = "Port 6"
                        });
                });

            modelBuilder.Entity("Shipping.Domain.Model.Ship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<Point>("Geolocation")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<long>("Velocity")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Ship");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e83f16d2-e0fb-41c5-87b6-ec79a529b17a"),
                            CreatedDate = new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7377),
                            Deleted = false,
                            Geolocation = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (68.2 88)"),
                            Name = "Ship 1",
                            Velocity = 30L
                        },
                        new
                        {
                            Id = new Guid("41ccf018-f498-41db-9b83-36b645f60afd"),
                            CreatedDate = new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7450),
                            Deleted = false,
                            Geolocation = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (4.2 78.2)"),
                            Name = "Ship 2",
                            Velocity = 40L
                        },
                        new
                        {
                            Id = new Guid("37524fb3-c2c9-414a-b683-e9c5d7ef5cf4"),
                            CreatedDate = new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7454),
                            Deleted = false,
                            Geolocation = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (11.2 23.2)"),
                            Name = "Ship 3",
                            Velocity = 32L
                        },
                        new
                        {
                            Id = new Guid("7b259406-2e49-413e-af5d-ef16015c876a"),
                            CreatedDate = new DateTime(2023, 5, 21, 17, 7, 22, 854, DateTimeKind.Utc).AddTicks(7457),
                            Deleted = false,
                            Geolocation = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (27.22 13.2)"),
                            Name = "Ship 4",
                            Velocity = 45L
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
