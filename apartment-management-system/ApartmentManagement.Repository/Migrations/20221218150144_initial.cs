using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApartmentManagement.Repository.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentityNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlateNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
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
                name: "Apartment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApartmentNumber = table.Column<int>(type: "int", nullable: false),
                    BlockNumber = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apartment_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApartmentCost",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentCost", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApartmentCost_Apartment_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Apartment",
                columns: new[] { "Id", "ApartmentNumber", "BlockNumber", "CreatedTime", "Floor", "Status", "Type", "UpdatedTime", "UserId" },
                values: new object[,]
                {
                    { 7, 7, 5, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4876), 4, "EMPTY", "2+1", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4876), null },
                    { 8, 8, 5, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4878), 4, "EMPTY", "2+1", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4878), null },
                    { 9, 10, 5, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4880), 4, "EMPTY", "2+1", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4880), null },
                    { 10, 10, 5, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4882), 6, "EMPTY", "4+1", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4883), null },
                    { 11, 12, 5, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4884), 6, "EMPTY", "3+1", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4885), null }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "Admin", "ADMIN" },
                    { "34213123xxx0-asd2–42de-afas29k3X72cf6", "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "IdentityNumber", "LastName", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PlateNumber", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "02174cf0–9123xccfe-afbf-59f706d33cf6", 0, "a151ee57-f6de-4ef5-98a0-faaff33ff077", "User", "meltem@aps.com", true, "452256565623", "cumbuş", false, null, "meltem", "MELTEM@APS.COM", "USER5", "AQAAAAEAACcQAAAAEAeZh6EsXgOoz2QOT4hqGMhArqwwZzy8Y2+vP5i+fdvrA4VZafM6rSsStmCH0Yv/vA==", "5453500023", false, "34BOS45", "af3c5a73-e1da-4ee5-822b-4b4012d20ba5", false, "user5" },
                    { "02174cf0–9412–4cfe-afbf-53422d33cf6", 0, "9ce0879c-b618-4c7a-ae6c-e892400a9dff", "User", "luffytaro@aps.com", true, "452256565623", "AYDINORO", false, null, "LuffyTaro", "LUFFYTARO@APS.COM", "USER2", "AQAAAAEAACcQAAAAEHuLtDtp4GoP1+inZQKcCLRJBcgl0NxxUcRyUETzw+WLoCRcJijpkfZYNCQsslHvkQ==", "5453500023", false, "34BOS45", "2ba3a13d-f57c-4a94-9bbd-f4ab36a6e428", false, "user2" },
                    { "02174cf0–9412–4cfe-afbf-591231sd6d33cf6", 0, "64ebdb1c-c23c-4102-9c54-a354f8818a38", "User", "ahmet@aps.com", true, "452256565623", "deli", false, null, "ahmet", "AHMET@APS.COM", "USER4", "AQAAAAEAACcQAAAAENSr7aA+1psg54F61rjL/5UweqtsLSTKSU5hXLGeAWMPAAU/vl0RYKmW3a+nsvTt/A==", "5453500023", false, "34BOS45", "ec7145f4-4fa7-4d3e-b5d8-f07db0bf2135", false, "user4" },
                    { "02174cf0–9412–4cfe-afbf-59f706d72cf6", 0, "f030e844-f18f-4b4d-ae38-d2ed558ec38e", "User", "admin@aps.com", true, "4556565623", "Aydin", false, null, "Deniz", "ADMIN@APS.COM", "ADMIN", "AQAAAAEAACcQAAAAEL5eSuWxdCaISccI9NJLFPs3cQyukhERn5DASVMtxtPFvB1wnkc8dzl037KrjAtRPg==", "5453500023", false, "34BOS45", "6f0c1a7f-2dfe-4f14-8a10-f3fbf434921c", false, "admin" },
                    { "02174cf0–9412–4cfe-afbf-5fhdf6d33cf6", 0, "49948cfd-99f0-4e0d-84ea-53acaa83c19f", "User", "yokotoro@aps.com", true, "452256565623", "Baygın", false, null, "Yoko", "YOKOTORO@APS.COM", "USER3", "AQAAAAEAACcQAAAAEDPt1rl6kC4MIkN+wbIFLZIGqxVqY5SG7+Cf0YTUVPyuwC4ehKgdrTFK19L5pZetZA==", "5453500023", false, "34BOS45", "95e7de43-c800-472b-9c0b-859f6aa4c131", false, "user3" },
                    { "02174cf0–9cvbcds2-afbf-59f706d33cf6", 0, "ae4bba6e-c550-415e-a876-fc30ef1452d9", "User", "akin@aps.com", true, "452256565623", "Akmaz", false, null, "Akin", "AKIN@APS.COM", "USER6", "AQAAAAEAACcQAAAAENUKydDcLEg70KPvevzcdoarNg88LFvTp0RncxiCllO4+JeOM/KQV+6k3k+YTdkyOg==", "5453500023", false, "34BOS45", "dd58e365-b091-42d2-a0b5-5bdfedfba56a", false, "user6" },
                    { "02174cf0–xcvds2e-afbf-59f706d33cf6", 0, "9306ded7-c5f7-4fdd-8f1a-c687050d5976", "User", "mori@aps.com", true, "452256565623", "Morar", false, null, "Mori", "MORI@APS.COM", "USER7", "AQAAAAEAACcQAAAAEBF7NcoamgFW+0j9aQP7w/TXoyCKXF8J1abA0qyT8TjUZD/D2hQn/ZwDlHnv4Od5Rw==", "5453500023", false, "34BOS45", "78d189aa-d946-4ed0-99f4-81a02274013b", false, "user7" }
                });

            migrationBuilder.InsertData(
                table: "Apartment",
                columns: new[] { "Id", "ApartmentNumber", "BlockNumber", "CreatedTime", "Floor", "Status", "Type", "UpdatedTime", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 4, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4860), 2, "FULL", "2+1", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4861), "02174cf0–9412–4cfe-afbf-53422d33cf6" },
                    { 2, 3, 5, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4864), 7, "FULL", "1+1", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4865), "02174cf0–9412–4cfe-afbf-5fhdf6d33cf6" },
                    { 3, 3, 5, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4867), 7, "FULL", "4+1", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4867), "02174cf0–9412–4cfe-afbf-591231sd6d33cf6" },
                    { 4, 5, 5, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4869), 3, "FULL", "3+1", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4869), "02174cf0–9123xccfe-afbf-59f706d33cf6" },
                    { 5, 5, 5, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4871), 3, "FULL", "2+1", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4872), "02174cf0–9cvbcds2-afbf-59f706d33cf6" },
                    { 6, 7, 5, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4873), 3, "EMPTY", "5+1", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4874), "02174cf0–xcvds2e-afbf-59f706d33cf6" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "34213123xxx0-asd2–42de-afas29k3X72cf6", "02174cf0–9123xccfe-afbf-59f706d33cf6" },
                    { "34213123xxx0-asd2–42de-afas29k3X72cf6", "02174cf0–9412–4cfe-afbf-53422d33cf6" },
                    { "34213123xxx0-asd2–42de-afas29k3X72cf6", "02174cf0–9412–4cfe-afbf-591231sd6d33cf6" },
                    { "341743f0-asd2–42de-afbf-59kmkkmk72cf6", "02174cf0–9412–4cfe-afbf-59f706d72cf6" },
                    { "34213123xxx0-asd2–42de-afas29k3X72cf6", "02174cf0–9412–4cfe-afbf-5fhdf6d33cf6" },
                    { "34213123xxx0-asd2–42de-afas29k3X72cf6", "02174cf0–9cvbcds2-afbf-59f706d33cf6" },
                    { "34213123xxx0-asd2–42de-afas29k3X72cf6", "02174cf0–xcvds2e-afbf-59f706d33cf6" }
                });

            migrationBuilder.InsertData(
                table: "Message",
                columns: new[] { "Id", "CreatedTime", "Description", "Status", "UpdatedTime", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5194), "Apartman temizlenmemiş", "NEW", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5196), "02174cf0–9412–4cfe-afbf-53422d33cf6" },
                    { 2, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5199), "Faturaları ödedim", "NEW", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5200), "02174cf0–9412–4cfe-afbf-53422d33cf6" },
                    { 3, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5201), "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis", "NEW", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5202), "02174cf0–9412–4cfe-afbf-5fhdf6d33cf6" },
                    { 4, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5203), "Lorem Ipsum is simply dummy text of the printing and typesetting industry.", "NEW", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5204), "02174cf0–9412–4cfe-afbf-5fhdf6d33cf6" },
                    { 5, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5206), " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. ", "NEW", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5206), "02174cf0–9412–4cfe-afbf-591231sd6d33cf6" },
                    { 6, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5207), "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.", "NEW", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5208), "02174cf0–9412–4cfe-afbf-591231sd6d33cf6" },
                    { 7, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5209), "Apartmanda kapıya ayakkabı bırakılmasın..", "NEW", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5210), "02174cf0–9123xccfe-afbf-59f706d33cf6" },
                    { 8, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5211), "Aidatı ödedim", "NEW", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(5212), "02174cf0–xcvds2e-afbf-59f706d33cf6" }
                });

            migrationBuilder.InsertData(
                table: "ApartmentCost",
                columns: new[] { "Id", "Amount", "ApartmentId", "CostType", "CreatedTime", "IsPaid", "Month", "UpdatedTime" },
                values: new object[,]
                {
                    { 1, 250, 1, "ELECTRICITY", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4571), false, 12, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4580) },
                    { 2, 250, 1, "WATER", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4586), false, 12, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4587) },
                    { 3, 250, 1, "GAS", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4588), false, 12, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4589) },
                    { 4, 250, 2, "ELECTRICITY", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4590), false, 9, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4591) },
                    { 5, 250, 2, "GAS", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4592), false, 10, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4593) },
                    { 6, 250, 2, "GAS", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4594), true, 9, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4595) },
                    { 7, 250, 3, "GAS", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4597), true, 9, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4597) },
                    { 8, 250, 3, "GAS", new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4599), true, 9, new DateTime(2022, 12, 18, 18, 1, 43, 672, DateTimeKind.Local).AddTicks(4599) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_UserId",
                table: "Apartment",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentCost_ApartmentId",
                table: "ApartmentCost",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserId",
                table: "Message",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApartmentCost");

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
                name: "Message");

            migrationBuilder.DropTable(
                name: "Apartment");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
