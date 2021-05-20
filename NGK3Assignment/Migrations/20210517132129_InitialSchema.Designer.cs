﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NGK3Assignment.Data;

namespace NGK3Assignment.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210517132129_InitialSchema")]
    partial class InitialSchema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NGK3Assignment.Models.WeatherStation", b =>
                {
                    b.Property<string>("PlaceId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Airpressure")
                        .HasColumnType("float");

                    b.Property<decimal>("Celcius")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Humidity")
                        .HasColumnType("float");

                    b.HasKey("PlaceId");

                    b.ToTable("WeatherStations");
                });
#pragma warning restore 612, 618
        }
    }
}
