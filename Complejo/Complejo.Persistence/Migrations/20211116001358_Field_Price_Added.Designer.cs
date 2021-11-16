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
    [Migration("20211116001358_Field_Price_Added")]
    partial class Field_Price_Added
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

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

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
                            Id = new Guid("76492c2c-2e4a-4fc8-8b0b-eda1604810bd"),
                            Description = "Disponible",
                            IdFieldStatusGroup = 1,
                            Removed = false
                        },
                        new
                        {
                            Id = new Guid("f1c024dc-cbcb-45d2-a834-a19ab865b84a"),
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
                            Id = new Guid("e1b27fad-18c5-45b2-bba2-3c355f8597f9"),
                            Description = "Fútbol 5",
                            IdFieldTypeGroup = 1,
                            Removed = false
                        },
                        new
                        {
                            Id = new Guid("040498cc-92d6-4952-8d38-451435a28e4c"),
                            Description = "Fútbol 8",
                            IdFieldTypeGroup = 2,
                            Removed = false
                        },
                        new
                        {
                            Id = new Guid("6461109c-0c03-4e29-9a85-b9183c7ba546"),
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

                    b.ToTable("Turn");
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

                    b.Navigation("Client");

                    b.Navigation("Field");
                });
#pragma warning restore 612, 618
        }
    }
}
