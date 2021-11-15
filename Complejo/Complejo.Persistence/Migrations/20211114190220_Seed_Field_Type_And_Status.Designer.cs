﻿// <auto-generated />
using System;
using Complejo.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Complejo.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211114190220_Seed_Field_Type_And_Status")]
    partial class Seed_Field_Type_And_Status
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Complejo.Domain.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Dni")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<string>("FullNameSearch")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)")
                        .HasComputedColumnSql("upper([FullName])");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.Property<bool>("Removed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("Removed")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Complejo.Domain.Entities.Field", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DescriptionSearch")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComputedColumnSql("upper([Description])");

                    b.Property<Guid>("IdFieldStatus")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdFieldType")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastUpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("Removed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("Removed")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.HasIndex("IdFieldStatus");

                    b.HasIndex("IdFieldType");

                    b.ToTable("Field");
                });

            modelBuilder.Entity("Complejo.Domain.Entities.FieldStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DescriptionSearch")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComputedColumnSql("upper([Description])");

                    b.Property<int>("IdFieldStatusGroup")
                        .HasColumnType("int")
                        .HasColumnName("IdFieldStatusGroup");

                    b.Property<bool>("Removed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("Removed")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.ToTable("DEF_FieldStatus");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9a026ee8-4306-4a83-8e9e-84a2a73514c3"),
                            Description = "Disponible",
                            IdFieldStatusGroup = 1,
                            Removed = false
                        },
                        new
                        {
                            Id = new Guid("9b26959f-6dfa-4502-b363-aa5ac75aa2af"),
                            Description = "En Mantenimiento",
                            IdFieldStatusGroup = 2,
                            Removed = false
                        });
                });

            modelBuilder.Entity("Complejo.Domain.Entities.FieldType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DescriptionSearch")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComputedColumnSql("upper([Description])");

                    b.Property<int>("IdFieldTypeGroup")
                        .HasColumnType("int")
                        .HasColumnName("IdFieldTypeGroup");

                    b.Property<bool>("Removed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("Removed")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.ToTable("DEF_FieldType");

                    b.HasData(
                        new
                        {
                            Id = new Guid("49eb8281-5f3d-4460-a513-01fdc323547b"),
                            Description = "Fútbol 5",
                            IdFieldTypeGroup = 1,
                            Removed = false
                        },
                        new
                        {
                            Id = new Guid("c6f6df98-7a9a-4820-9d78-fc2f89346e3a"),
                            Description = "Fútbol 8",
                            IdFieldTypeGroup = 2,
                            Removed = false
                        },
                        new
                        {
                            Id = new Guid("277d069d-b1b7-4513-8c32-d40e3d3ae77a"),
                            Description = "Fútbol 11",
                            IdFieldTypeGroup = 3,
                            Removed = false
                        });
                });

            modelBuilder.Entity("Complejo.Domain.Entities.Turn", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("varchar(7)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<Guid>("IdClient")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdField")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdTurnStatus")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LastUpdatedBy")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("LastUpdatedDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("Removed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("Removed")
                        .HasDefaultValueSql("0");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.HasIndex("IdClient");

                    b.HasIndex("IdField");

                    b.HasIndex("IdTurnStatus");

                    b.ToTable("Turn");
                });

            modelBuilder.Entity("Complejo.Domain.Entities.TurnStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("DescriptionSearch")
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComputedColumnSql("upper([Description])");

                    b.Property<int>("IdTurnStatusGroup")
                        .HasColumnType("int")
                        .HasColumnName("IdTurnStatusGroup");

                    b.Property<bool>("Removed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("Removed")
                        .HasDefaultValueSql("0");

                    b.HasKey("Id");

                    b.ToTable("DEF_TurnStatus");
                });

            modelBuilder.Entity("Complejo.Domain.Entities.Field", b =>
                {
                    b.HasOne("Complejo.Domain.Entities.FieldStatus", "FieldStatus")
                        .WithMany()
                        .HasForeignKey("IdFieldStatus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Complejo.Domain.Entities.FieldType", "FieldType")
                        .WithMany()
                        .HasForeignKey("IdFieldType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FieldStatus");

                    b.Navigation("FieldType");
                });

            modelBuilder.Entity("Complejo.Domain.Entities.Turn", b =>
                {
                    b.HasOne("Complejo.Domain.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("IdClient");

                    b.HasOne("Complejo.Domain.Entities.Field", "Field")
                        .WithMany()
                        .HasForeignKey("IdField")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Complejo.Domain.Entities.TurnStatus", "TurnStatus")
                        .WithMany()
                        .HasForeignKey("IdTurnStatus")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Field");

                    b.Navigation("TurnStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
