using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tsi.Template.Infrastructure.Migrations
{
    public partial class Initial_Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App_Departements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_Departements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Libelle = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Common_Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogLevelId = table.Column<int>(type: "int", nullable: false),
                    ShortMessage = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FullMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    PageUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ReferrerUrl = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Common_Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SystemName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Common_Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", maxLength: 6000, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_Settings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Common_UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Common_Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUsername = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    LockedOut = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    FailedAttempts = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "App_Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cin = table.Column<long>(type: "bigint", maxLength: 8, nullable: false),
                    DepartementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_App_Employees_App_Departements_DepartementId",
                        column: x => x.DepartementId,
                        principalTable: "App_Departements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Common_PermissionUserRoleMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_PermissionUserRoleMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Common_PermissionUserRoleMappings_Common_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Common_Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Common_PermissionUserRoleMappings_Common_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "Common_UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Common_UserRoleMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Common_UserRoleMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Common_UserRoleMappings_Common_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "Common_UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Common_UserRoleMappings_Common_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Common_Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_App_Departements_Code",
                table: "App_Departements",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_App_Employees_Cin",
                table: "App_Employees",
                column: "Cin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_App_Employees_DepartementId",
                table: "App_Employees",
                column: "DepartementId");

            migrationBuilder.CreateIndex(
                name: "IX_App_Products_Code",
                table: "App_Products",
                column: "Code",
                filter: " Deleted = 1 ");

            migrationBuilder.CreateIndex(
                name: "IX_Common_Permissions_SystemName",
                table: "Common_Permissions",
                column: "SystemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Common_PermissionUserRoleMappings_PermissionId",
                table: "Common_PermissionUserRoleMappings",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Common_PermissionUserRoleMappings_UserRoleId",
                table: "Common_PermissionUserRoleMappings",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Common_UserRoleMappings_UserId",
                table: "Common_UserRoleMappings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Common_UserRoleMappings_UserRoleId",
                table: "Common_UserRoleMappings",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Common_UserRoles_SystemName",
                table: "Common_UserRoles",
                column: "SystemName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Common_Users_Email",
                table: "Common_Users",
                column: "Email",
                unique: true,
                filter: " Email IS NULL ");

            migrationBuilder.CreateIndex(
                name: "IX_Common_Users_NormalizedEmail",
                table: "Common_Users",
                column: "NormalizedEmail",
                unique: true,
                filter: " NormalizedEmail IS NULL ");

            migrationBuilder.CreateIndex(
                name: "IX_Common_Users_NormalizedUsername",
                table: "Common_Users",
                column: "NormalizedUsername",
                unique: true,
                filter: " NormalizedUsername IS NULL ");

            migrationBuilder.CreateIndex(
                name: "IX_Common_Users_PhoneNumber",
                table: "Common_Users",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Common_Users_Username",
                table: "Common_Users",
                column: "Username",
                unique: true,
                filter: " Username IS NULL ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "App_Employees");

            migrationBuilder.DropTable(
                name: "App_Products");

            migrationBuilder.DropTable(
                name: "Common_Logs");

            migrationBuilder.DropTable(
                name: "Common_PermissionUserRoleMappings");

            migrationBuilder.DropTable(
                name: "Common_Settings");

            migrationBuilder.DropTable(
                name: "Common_UserRoleMappings");

            migrationBuilder.DropTable(
                name: "App_Departements");

            migrationBuilder.DropTable(
                name: "Common_Permissions");

            migrationBuilder.DropTable(
                name: "Common_UserRoles");

            migrationBuilder.DropTable(
                name: "Common_Users");
        }
    }
}
