using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Library.Migrations
{
    public partial class Populate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Welding");

            migrationBuilder.EnsureSchema(
                name: "DEV");

            migrationBuilder.EnsureSchema(
                name: "Document");

            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.EnsureSchema(
                name: "DEVTest1");

            migrationBuilder.EnsureSchema(
                name: "DEVTest2");

            migrationBuilder.CreateTable(
                name: "Masters",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    App = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Controller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavController = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NavRoute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Javascript = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AjaxUpdate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Style = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sort = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ModelItem",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NDEs",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NDEs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NDEStages",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NDEStages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "States",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tenant",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StyleSheet = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Test",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Upload",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upload", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeldStatuses",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeldStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Masters_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "Masters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Consumables",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consumables_Masters_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "Masters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Drawings",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drawings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drawings_Masters_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "Masters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fitters",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fitters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fitters_Masters_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "Masters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sheets",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sheets_Masters_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "Masters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Welders",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Welders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Welders_Masters_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "Masters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WPSs",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WPSs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WPSs_Masters_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "Masters",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NDEReports",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NDEId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NDEReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NDEReports_Masters_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "Masters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NDEReports_NDEs_NDEId",
                        column: x => x.NDEId,
                        principalSchema: "Welding",
                        principalTable: "NDEs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NDERequests",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NDEId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NDERequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NDERequests_Masters_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "Masters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NDERequests_NDEs_NDEId",
                        column: x => x.NDEId,
                        principalSchema: "Welding",
                        principalTable: "NDEs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoles_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "DEV",
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Module",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Module_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "DEV",
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "DEV",
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Scan",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lat = table.Column<double>(type: "float", nullable: true),
                    Lng = table.Column<double>(type: "float", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scan_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "DEV",
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MainItem",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    myDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainItem_Type_Type",
                        column: x => x.Type,
                        principalSchema: "DEV",
                        principalTable: "Type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SheetProperties",
                schema: "Document",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Revision = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheetProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheetProperties_Classes_ClassId",
                        column: x => x.ClassId,
                        principalSchema: "Welding",
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SheetProperties_Drawings_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Document",
                        principalTable: "Drawings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SheetProperties_Sheets_Id",
                        column: x => x.Id,
                        principalSchema: "Document",
                        principalTable: "Sheets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentProject = table.Column<int>(type: "int", nullable: true),
                    CurrentTenant = table.Column<int>(type: "int", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tenants = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Projects = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Project_CurrentProject",
                        column: x => x.CurrentProject,
                        principalSchema: "DEV",
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Tenant_CurrentTenant",
                        column: x => x.CurrentTenant,
                        principalSchema: "DEV",
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Test1MainItem",
                schema: "DEVTest1",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tobeDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test1MainItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test1MainItem_MainItem_Id",
                        column: x => x.Id,
                        principalSchema: "DEV",
                        principalTable: "MainItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Test2MainItem",
                schema: "DEVTest2",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tobeDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test2MainItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Test2MainItem_MainItem_Id",
                        column: x => x.Id,
                        principalSchema: "DEV",
                        principalTable: "MainItem",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Welds",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    WPSId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Welds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Welds_Masters_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "Masters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Welds_SheetProperties_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Document",
                        principalTable: "SheetProperties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Welds_WPSs_WPSId",
                        column: x => x.WPSId,
                        principalSchema: "Welding",
                        principalTable: "WPSs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PushEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PushP256DH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PushAuth = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Master",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true),
                    Global = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CU = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    MU = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MD = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Master", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Master_AspNetUsers_CU",
                        column: x => x.CU,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Master_AspNetUsers_MU",
                        column: x => x.MU,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Master_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "DEV",
                        principalTable: "Project",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Master_Tenant_TenantId",
                        column: x => x.TenantId,
                        principalSchema: "DEV",
                        principalTable: "Tenant",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserSettings",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CU = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSettings_AspNetUsers_CU",
                        column: x => x.CU,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeldDetails",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    FitterId = table.Column<int>(type: "int", nullable: true),
                    WelderId = table.Column<int>(type: "int", nullable: true),
                    Consumable1Id = table.Column<int>(type: "int", nullable: true),
                    WeldStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeldDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeldDetails_Consumables_Consumable1Id",
                        column: x => x.Consumable1Id,
                        principalSchema: "Welding",
                        principalTable: "Consumables",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WeldDetails_Fitters_FitterId",
                        column: x => x.FitterId,
                        principalSchema: "Welding",
                        principalTable: "Fitters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WeldDetails_Masters_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "Masters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WeldDetails_Welders_WelderId",
                        column: x => x.WelderId,
                        principalSchema: "Welding",
                        principalTable: "Welders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WeldDetails_Welds_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Welding",
                        principalTable: "Welds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WeldDetails_WeldStatuses_WeldStatusId",
                        column: x => x.WeldStatusId,
                        principalSchema: "Welding",
                        principalTable: "WeldStatuses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Drawing",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Bool1 = table.Column<bool>(type: "bit", nullable: true),
                    Bool2 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drawing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drawing_Master_Id",
                        column: x => x.Id,
                        principalSchema: "DEV",
                        principalTable: "Master",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeldDetailNDEs",
                schema: "Welding",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    MyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    RequestId = table.Column<int>(type: "int", nullable: true),
                    ReportId = table.Column<int>(type: "int", nullable: true),
                    ViewStateId = table.Column<int>(type: "int", nullable: true),
                    NDEStageId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeldDetailNDEs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeldDetailNDEs_Masters_Id",
                        column: x => x.Id,
                        principalSchema: "Core",
                        principalTable: "Masters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WeldDetailNDEs_NDEReports_ReportId",
                        column: x => x.ReportId,
                        principalSchema: "Welding",
                        principalTable: "NDEReports",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WeldDetailNDEs_NDERequests_RequestId",
                        column: x => x.RequestId,
                        principalSchema: "Welding",
                        principalTable: "NDERequests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WeldDetailNDEs_NDEStages_NDEStageId",
                        column: x => x.NDEStageId,
                        principalSchema: "Welding",
                        principalTable: "NDEStages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WeldDetailNDEs_States_ViewStateId",
                        column: x => x.ViewStateId,
                        principalSchema: "Core",
                        principalTable: "States",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WeldDetailNDEs_WeldDetails_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "Welding",
                        principalTable: "WeldDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sheet",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sheet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sheet_Drawing_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "DEV",
                        principalTable: "Drawing",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sheet_Master_Id",
                        column: x => x.Id,
                        principalSchema: "DEV",
                        principalTable: "Master",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Weld",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weld", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weld_Master_Id",
                        column: x => x.Id,
                        principalSchema: "DEV",
                        principalTable: "Master",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Weld_Sheet_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "DEV",
                        principalTable: "Sheet",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WeldInfo",
                schema: "DEV",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeldInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeldInfo_Weld_Id",
                        column: x => x.Id,
                        principalSchema: "DEV",
                        principalTable: "Weld",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_TenantId",
                table: "AspNetRoles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrentProject",
                table: "AspNetUsers",
                column: "CurrentProject");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CurrentTenant",
                table: "AspNetUsers",
                column: "CurrentTenant");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId",
                schema: "DEV",
                table: "Devices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MainItem_Type",
                schema: "DEV",
                table: "MainItem",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_Master_CU",
                schema: "DEV",
                table: "Master",
                column: "CU");

            migrationBuilder.CreateIndex(
                name: "IX_Master_MU",
                schema: "DEV",
                table: "Master",
                column: "MU");

            migrationBuilder.CreateIndex(
                name: "IX_Master_ProjectId",
                schema: "DEV",
                table: "Master",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Master_TenantId",
                schema: "DEV",
                table: "Master",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Module_TenantId",
                schema: "DEV",
                table: "Module",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_NDEReports_NDEId",
                schema: "Welding",
                table: "NDEReports",
                column: "NDEId");

            migrationBuilder.CreateIndex(
                name: "IX_NDERequests_NDEId",
                schema: "Welding",
                table: "NDERequests",
                column: "NDEId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_TenantId",
                schema: "DEV",
                table: "Project",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Scan_TenantId",
                schema: "DEV",
                table: "Scan",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Sheet_ParentId",
                schema: "DEV",
                table: "Sheet",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_SheetProperties_ClassId",
                schema: "Document",
                table: "SheetProperties",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_SheetProperties_ParentId",
                schema: "Document",
                table: "SheetProperties",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSettings_CU",
                schema: "DEV",
                table: "UserSettings",
                column: "CU");

            migrationBuilder.CreateIndex(
                name: "IX_Weld_ParentId",
                schema: "DEV",
                table: "Weld",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_WeldDetailNDEs_NDEStageId",
                schema: "Welding",
                table: "WeldDetailNDEs",
                column: "NDEStageId");

            migrationBuilder.CreateIndex(
                name: "IX_WeldDetailNDEs_ParentId",
                schema: "Welding",
                table: "WeldDetailNDEs",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_WeldDetailNDEs_ReportId",
                schema: "Welding",
                table: "WeldDetailNDEs",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_WeldDetailNDEs_RequestId",
                schema: "Welding",
                table: "WeldDetailNDEs",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_WeldDetailNDEs_ViewStateId",
                schema: "Welding",
                table: "WeldDetailNDEs",
                column: "ViewStateId");

            migrationBuilder.CreateIndex(
                name: "IX_WeldDetails_Consumable1Id",
                schema: "Welding",
                table: "WeldDetails",
                column: "Consumable1Id");

            migrationBuilder.CreateIndex(
                name: "IX_WeldDetails_FitterId",
                schema: "Welding",
                table: "WeldDetails",
                column: "FitterId");

            migrationBuilder.CreateIndex(
                name: "IX_WeldDetails_ParentId",
                schema: "Welding",
                table: "WeldDetails",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_WeldDetails_WelderId",
                schema: "Welding",
                table: "WeldDetails",
                column: "WelderId");

            migrationBuilder.CreateIndex(
                name: "IX_WeldDetails_WeldStatusId",
                schema: "Welding",
                table: "WeldDetails",
                column: "WeldStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Welds_ParentId",
                schema: "Welding",
                table: "Welds",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Welds_WPSId",
                schema: "Welding",
                table: "Welds",
                column: "WPSId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Devices",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "Menu",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "ModelItem",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "Module",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "Scan",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "Test",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "Test1MainItem",
                schema: "DEVTest1");

            migrationBuilder.DropTable(
                name: "Test2MainItem",
                schema: "DEVTest2");

            migrationBuilder.DropTable(
                name: "Upload",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "UserSettings",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "WeldDetailNDEs",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "WeldInfo",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "MainItem",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "NDEReports",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "NDERequests",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "NDEStages",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "States",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "WeldDetails",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "Weld",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "Type",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "NDEs",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "Consumables",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "Fitters",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "Welders",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "Welds",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "WeldStatuses",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "Sheet",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "SheetProperties",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "WPSs",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "Drawing",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "Classes",
                schema: "Welding");

            migrationBuilder.DropTable(
                name: "Drawings",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "Sheets",
                schema: "Document");

            migrationBuilder.DropTable(
                name: "Master",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "Masters",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "DEV");

            migrationBuilder.DropTable(
                name: "Tenant",
                schema: "DEV");
        }
    }
}
