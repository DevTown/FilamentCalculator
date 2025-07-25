﻿// <auto-generated />
using FilamentCalculator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FilamentCalculator.Migrations
{
    [DbContext(typeof(FilamentCalcContext))]
    partial class FilamentCalcContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FilamentCalculator.Models.Filament", b =>
                {
                    b.Property<int>("FilamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FilamentId"));

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<float>("Diameter")
                        .HasColumnType("real");

                    b.Property<int>("FilamentTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("integer");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("PrintTempBed")
                        .HasColumnType("text");

                    b.Property<string>("PrintTempNozzle")
                        .HasColumnType("text");

                    b.Property<float>("SpoolWeight")
                        .HasColumnType("real");

                    b.HasKey("FilamentId");

                    b.HasIndex("FilamentTypeId");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Filaments");
                });

            modelBuilder.Entity("FilamentCalculator.Models.FilamentType", b =>
                {
                    b.Property<int>("FilamentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("FilamentTypeId"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<float>("WeightPerMM")
                        .HasColumnType("real");

                    b.HasKey("FilamentTypeId");

                    b.ToTable("FilamentTypes");
                });

            modelBuilder.Entity("FilamentCalculator.Models.Manufacturer", b =>
                {
                    b.Property<int>("ManufacturerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ManufacturerId"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("ManufacturerId");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("FilamentCalculator.Models.Printer", b =>
                {
                    b.Property<int>("PrinterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PrinterId"));

                    b.Property<decimal>("EnergyConsumptionW")
                        .HasColumnType("numeric");

                    b.Property<float>("FilamentDiameter")
                        .HasColumnType("real");

                    b.Property<string>("ManufacturerName")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("PeriotOfAmortisation")
                        .HasColumnType("numeric");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("PrinterId");

                    b.ToTable("Printers");
                });

            modelBuilder.Entity("FilamentCalculator.Models.Settings", b =>
                {
                    b.Property<int>("SettingsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SettingsId"));

                    b.Property<decimal>("Energiekosts")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Hourlywage")
                        .HasColumnType("numeric");

                    b.Property<int>("MissprintChance")
                        .HasColumnType("integer");

                    b.Property<decimal>("PrinterDepricationKostsPerHour")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Revenuepercentage")
                        .HasColumnType("numeric");

                    b.HasKey("SettingsId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("FilamentCalculator.Models.Shipment", b =>
                {
                    b.Property<int>("ShipmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ShipmentID"));

                    b.Property<decimal>("AddonItemPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("FillerPrice")
                        .HasColumnType("numeric");

                    b.Property<decimal>("LablePrice")
                        .HasColumnType("numeric");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Packagingprice")
                        .HasColumnType("numeric");

                    b.Property<string>("ShipmentOrg")
                        .HasColumnType("text");

                    b.Property<decimal>("ShippingPrice")
                        .HasColumnType("numeric");

                    b.HasKey("ShipmentID");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("FilamentCalculator.Models.Filament", b =>
                {
                    b.HasOne("FilamentCalculator.Models.FilamentType", "FilamentType")
                        .WithMany()
                        .HasForeignKey("FilamentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilamentCalculator.Models.Manufacturer", "Manufacturer")
                        .WithMany("Filaments")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FilamentType");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("FilamentCalculator.Models.Manufacturer", b =>
                {
                    b.Navigation("Filaments");
                });
#pragma warning restore 612, 618
        }
    }
}
