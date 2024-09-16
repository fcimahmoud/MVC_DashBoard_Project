﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoDataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeDepartmentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentiD",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentiD",
                table: "Employees",
                column: "DepartmentiD");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentiD",
                table: "Employees",
                column: "DepartmentiD",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentiD",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_DepartmentiD",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "DepartmentiD",
                table: "Employees");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
