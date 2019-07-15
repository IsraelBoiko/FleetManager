﻿// <auto-generated />
using FleetManager.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FleetManager.Data.Migrations
{
    [DbContext(typeof(FleetContext))]
    [Migration("20190715121307_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("FleetManager.Model.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Chassi")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Color")
                        .HasMaxLength(100);

                    b.Property<int>("Type");

                    b.HasKey("Id")
                        .HasName("VehiclePk");

                    b.HasIndex("Chassi")
                        .IsUnique()
                        .HasName("VehicleNk");

                    b.ToTable("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}
