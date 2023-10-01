using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _9th_Monthly_Exam.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Specialist = table.Column<int>(type: "int", nullable: false),
                    Fees = table.Column<decimal>(type: "money", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OnAvilable = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDate = table.Column<DateTime>(type: "date", nullable: false),
                    TotalPatient = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "DoctorId", "DoctorName", "Fees", "OnAvilable", "Picture", "Specialist" },
                values: new object[,]
                {
                    { 1, "Doctor1", 1693.00m, true, "1.jpg", 1 },
                    { 2, "Doctor2", 1591.00m, true, "2.jpg", 2 },
                    { 3, "Doctor3", 1939.00m, true, "3.jpg", 1 },
                    { 4, "Doctor4", 1410.00m, true, "4.jpg", 3 },
                    { 5, "Doctor5", 1759.00m, true, "5.jpg", 1 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "AppointmentId", "AppointmentDate", "DoctorId", "TotalPatient" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 2, 21, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1505), 1, 80 },
                    { 2, new DateTime(2022, 6, 28, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1549), 2, 84 },
                    { 3, new DateTime(2023, 1, 13, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1564), 3, 10 },
                    { 4, new DateTime(2022, 9, 17, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1578), 4, 43 },
                    { 5, new DateTime(2023, 2, 3, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1591), 5, 85 },
                    { 6, new DateTime(2022, 7, 19, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1607), 1, 93 },
                    { 7, new DateTime(2022, 10, 29, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1620), 2, 81 },
                    { 8, new DateTime(2023, 3, 7, 0, 37, 43, 534, DateTimeKind.Local).AddTicks(1634), 3, 59 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");
            string sqlI = @"CREATE PROC InsertDoctor @n VARCHAR(50), @f MONEY, @s INT, @pi VARCHAR(50), @o BIT
             AS
             INSERT INTO Doctors (DoctorName, Fees, Specialist, Picture, OnAvilable)
             VALUES (@n, @f, @s, @pi, @o )
            
             GO";
            migrationBuilder.Sql(sqlI);
            string sqlU = @"CREATE PROC UpdateDoctor @i INT, @n VARCHAR(50), @f MONEY, @s INT, @pi VARCHAR(50), @o BIT
             AS
             UPDATE Doctors SET DoctorName=@n, Fees=@f, Specialist=@s, Picture=@pi, OnAvilable=@o
             WHERE DoctorId=@i
             GO";
            migrationBuilder.Sql(sqlU);
            string sqlD = @"CREATE PROC DeleteDoctor @i INT
                 AS
                 DELETE Doctors 
                 WHERE DoctorId=@i
                 GO";
            migrationBuilder.Sql(sqlD);


            string sqlS = @"CREATE PROC InsertAppointment @d DATE, @r INT, @did INT
             AS
             INSERT INTO Appointments ([AppointmentDate], TotalPatient, DoctorId)
             VALUES (@d, @r, @did )
             GO";
            migrationBuilder.Sql(sqlS);
            string sqlSU = @"CREATE PROC UpdateAppointment @id INT, @d DATE, @r INT, @did INT
             AS
             UPDATE Appointments SET [AppointmentDate]=@d, TotalPatient=@r, DoctorId=@did
             WHERE AppointmentId = @id
             GO";
            migrationBuilder.Sql(sqlSU);
            string sqlDU = @"CREATE PROC DeleteAppointment @id INT
             AS
             DELETE Appointments 
             WHERE AppointmentId = @id
             GO";
            migrationBuilder.Sql(sqlDU);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Doctors");
            migrationBuilder.Sql("DROP PROC InsertDoctor");
            migrationBuilder.Sql("DROP PROC UpdateDoctor");
            migrationBuilder.Sql("DROP PROC DeleteDoctor");
            migrationBuilder.Sql("DROP PROC InsertAppointment");
            migrationBuilder.Sql("DROP PROC UpdateAppointment");
            migrationBuilder.Sql("DROP PROC DeleteAppointment");
        }
    }
}
