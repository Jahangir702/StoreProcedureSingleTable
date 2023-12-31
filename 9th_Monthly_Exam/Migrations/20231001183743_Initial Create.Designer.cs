﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using _9th_Monthly_Exam.Models;

#nullable disable

namespace _9th_Monthly_Exam.Migrations
{
    [DbContext(typeof(DoctorDbContext))]
    [Migration("20231001183743_Initial Create")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("_9th_Monthly_Exam.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTime?>("AppointmentDate")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int?>("TotalPatient")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DoctorId");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            AppointmentId = 1,
                            AppointmentDate = new DateTime(2023, 2, 21, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1505),
                            DoctorId = 1,
                            TotalPatient = 80
                        },
                        new
                        {
                            AppointmentId = 2,
                            AppointmentDate = new DateTime(2022, 6, 28, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1549),
                            DoctorId = 2,
                            TotalPatient = 84
                        },
                        new
                        {
                            AppointmentId = 3,
                            AppointmentDate = new DateTime(2023, 1, 13, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1564),
                            DoctorId = 3,
                            TotalPatient = 10
                        },
                        new
                        {
                            AppointmentId = 4,
                            AppointmentDate = new DateTime(2022, 9, 17, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1578),
                            DoctorId = 4,
                            TotalPatient = 43
                        },
                        new
                        {
                            AppointmentId = 5,
                            AppointmentDate = new DateTime(2023, 2, 3, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1591),
                            DoctorId = 5,
                            TotalPatient = 85
                        },
                        new
                        {
                            AppointmentId = 6,
                            AppointmentDate = new DateTime(2022, 7, 19, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1607),
                            DoctorId = 1,
                            TotalPatient = 93
                        },
                        new
                        {
                            AppointmentId = 7,
                            AppointmentDate = new DateTime(2022, 10, 29, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1620),
                            DoctorId = 2,
                            TotalPatient = 81
                        },
                        new
                        {
                            AppointmentId = 8,
                            AppointmentDate = new DateTime(2023, 3, 7, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1634),
                            DoctorId = 3,
                            TotalPatient = 59
                        });
                });

            modelBuilder.Entity("_9th_Monthly_Exam.Models.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"));

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Fees")
                        .HasColumnType("money");

                    b.Property<bool?>("OnAvilable")
                        .HasColumnType("bit");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Specialist")
                        .HasColumnType("int");

                    b.HasKey("DoctorId");

                    b.ToTable("Doctors");

                    b.HasData(
                        new
                        {
                            DoctorId = 1,
                            DoctorName = "Doctor1",
                            Fees = 1693.00m,
                            OnAvilable = true,
                            Picture = "1.jpg",
                            Specialist = 1
                        },
                        new
                        {
                            DoctorId = 2,
                            DoctorName = "Doctor2",
                            Fees = 1591.00m,
                            OnAvilable = true,
                            Picture = "2.jpg",
                            Specialist = 2
                        },
                        new
                        {
                            DoctorId = 3,
                            DoctorName = "Doctor3",
                            Fees = 1939.00m,
                            OnAvilable = true,
                            Picture = "3.jpg",
                            Specialist = 1
                        },
                        new
                        {
                            DoctorId = 4,
                            DoctorName = "Doctor4",
                            Fees = 1410.00m,
                            OnAvilable = true,
                            Picture = "4.jpg",
                            Specialist = 3
                        },
                        new
                        {
                            DoctorId = 5,
                            DoctorName = "Doctor5",
                            Fees = 1759.00m,
                            OnAvilable = true,
                            Picture = "5.jpg",
                            Specialist = 1
                        });
                });

            modelBuilder.Entity("_9th_Monthly_Exam.Models.Appointment", b =>
                {
                    b.HasOne("_9th_Monthly_Exam.Models.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("_9th_Monthly_Exam.Models.Doctor", b =>
                {
                    b.Navigation("Appointments");
                });
#pragma warning restore 612, 618
        }
    }
}
