using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SystemRezerwacjiLotow.Infrastruktura.Migrations
{
    /// <inheritdoc />
    public partial class mig01 : Migration
    {
        /// <inheritdoc />
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
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataUrodzenia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantKind = table.Column<int>(type: "int", nullable: false),
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
                name: "Flights",
                columns: table => new
                {
                    FlightId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TrasaOd = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrasaDo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    GodzinaWylotu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataDodania = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightId);
                    table.ForeignKey(
                        name: "FK_Flights_AspNetUsers_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DniWylotow",
                columns: table => new
                {
                    DzieWylotuId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Dzien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DniWylotow", x => x.DzieWylotuId);
                    table.ForeignKey(
                        name: "FK_DniWylotow_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId");
                });

            migrationBuilder.CreateTable(
                name: "FlightBuys",
                columns: table => new
                {
                    FlightBuyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IloscBiletow = table.Column<int>(type: "int", nullable: false),
                    PriceSuma = table.Column<double>(type: "float", nullable: false),
                    DataZakupu = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightBuys", x => x.FlightBuyId);
                    table.ForeignKey(
                        name: "FK_FlightBuys_AspNetUsers_TenantId",
                        column: x => x.TenantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FlightBuys_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "FlightId");
                });

            migrationBuilder.CreateTable(
                name: "KryteriaZnizek",
                columns: table => new
                {
                    KryteriaZnizkiId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightBuyId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KryteriaZnizek", x => x.KryteriaZnizkiId);
                    table.ForeignKey(
                        name: "FK_KryteriaZnizek_FlightBuys_FlightBuyId",
                        column: x => x.FlightBuyId,
                        principalTable: "FlightBuys",
                        principalColumn: "FlightBuyId");
                });

            migrationBuilder.CreateTable(
                name: "FlightBuysKryteriaZnizkis",
                columns: table => new
                {
                    FlightBuyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KryteriaZnizkiId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightBuysKryteriaZnizkis", x => new { x.FlightBuyId, x.KryteriaZnizkiId });
                    table.ForeignKey(
                        name: "FK_FlightBuysKryteriaZnizkis_FlightBuys_FlightBuyId",
                        column: x => x.FlightBuyId,
                        principalTable: "FlightBuys",
                        principalColumn: "FlightBuyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightBuysKryteriaZnizkis_KryteriaZnizek_KryteriaZnizkiId",
                        column: x => x.KryteriaZnizkiId,
                        principalTable: "KryteriaZnizek",
                        principalColumn: "KryteriaZnizkiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DataUrodzenia", "Email", "EmailConfirmed", "Imie", "LockoutEnabled", "LockoutEnd", "Nazwisko", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TenantKind", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "32f1c1ee-54ef-49ed-85a6-2790b7c362dd", 0, "58bc973f-9b2a-4764-85c6-94d20875641e", "22.10.1983 12:12:12", "ImieNazwisko_9@gmail.com", true, "Imie_9", false, null, "Nazwisko_9", "IMIENAZWISKO_9@GMAIL.COM", "IMIENAZWISKO_9@GMAIL.COM", "AQAAAAIAAYagAAAAEFZqTNum8NUV5fY8C5qkKvRQVkWa9Bm/Fa+JvAoAo218ziV9uYruEUqZwSP8L4bgwA==", null, false, "d77ea6fd-97aa-416a-9e21-dc479c5c4b1c", 0, false, "ImieNazwisko_9@gmail.com" },
                    { "79f216ae-171a-44e2-961c-65eeaae68f42", 0, "cd7c426d-a64d-4365-8d8e-c983318dbcc9", "22.10.1983 12:12:12", "ImieNazwisko_8@gmail.com", true, "Imie_8", false, null, "Nazwisko_8", "IMIENAZWISKO_8@GMAIL.COM", "IMIENAZWISKO_8@GMAIL.COM", "AQAAAAIAAYagAAAAELXGKm5Wgvsgd6gPJu0EWTUtiCFj4bIrC2qkvMLMc+BAMTVbrZLmp7sjoenGGiKulQ==", null, false, "7ff676fd-f433-461f-aff5-c255cf3b3354", 0, false, "ImieNazwisko_8@gmail.com" },
                    { "85b8e2b7-0cec-4c0e-a5f3-30c10772242f", 0, "2bf465f7-dfd3-4b08-9f07-288158979da6", "22.10.1983 12:12:12", "admin@admin.pl", true, "Jan", false, null, "Kowalski", "ADMIN@ADMIN.PL", "ADMIN@ADMIN.PL", "AQAAAAIAAYagAAAAEKicrpMlC784mAEPnrEWbqhmF4pEp5HVDZfObVkE4wFBT0dxBGze/dAsLz+cnjtarw==", null, false, "3239ffa3-3474-4fb4-9aaa-ff8fb6d0ada3", 0, false, "admin@admin.pl" },
                    { "86d21392-9c66-45d4-957e-941830aafeec", 0, "82ce31b5-57c7-46b9-96f5-a5c40962c510", "22.10.1983 12:12:12", "ImieNazwisko_4@gmail.com", true, "Imie_4", false, null, "Nazwisko_4", "IMIENAZWISKO_4@GMAIL.COM", "IMIENAZWISKO_4@GMAIL.COM", "AQAAAAIAAYagAAAAECsAvfHQ4iRircDL9PzckXPfdzZ7cwK6a+F7jcS1WE5c1js434akrHnVkp9SGsceKg==", null, false, "fa88a45e-7329-4c89-8342-16ca08b71615", 0, false, "ImieNazwisko_4@gmail.com" },
                    { "acff8f55-e5cf-4984-9ef5-fc5c1455e2a3", 0, "186508cf-626a-4cb9-b015-45fe5244d654", "22.10.1983 12:12:12", "ImieNazwisko_2@gmail.com", true, "Imie_2", false, null, "Nazwisko_2", "IMIENAZWISKO_2@GMAIL.COM", "IMIENAZWISKO_2@GMAIL.COM", "AQAAAAIAAYagAAAAEEgbUg24r0I0lJuQAPq2Z0TslW6xQgR5y5Wdxbwr+ULd2Bu7Ka+wytg0qjQJ+mE3nQ==", null, false, "9d0bc8d9-f9e9-4144-a2ea-52bd7f0c9cc0", 0, false, "ImieNazwisko_2@gmail.com" },
                    { "b0c25e86-a029-4c25-9449-65409d7547ed", 0, "1ed30eca-a7c9-4fd7-8a42-8c41c4d969ef", "22.10.1983 12:12:12", "ImieNazwisko_5@gmail.com", true, "Imie_5", false, null, "Nazwisko_5", "IMIENAZWISKO_5@GMAIL.COM", "IMIENAZWISKO_5@GMAIL.COM", "AQAAAAIAAYagAAAAEFeSn9lSm3P1/Gu8Jvlp3RRpfpUZSh1whC/U3L6JQ1cI7pKh5WmDxth2k0jlDd9j5Q==", null, false, "b4d1c15e-0fdf-4947-9505-6b753cb452aa", 0, false, "ImieNazwisko_5@gmail.com" },
                    { "cfc9e4bb-17dd-40fa-8fd1-7aad8e101f39", 0, "6bfd555e-d592-4301-b50d-18412d2d1df7", "22.10.1983 12:12:12", "ImieNazwisko_1@gmail.com", true, "Imie_1", false, null, "Nazwisko_1", "IMIENAZWISKO_1@GMAIL.COM", "IMIENAZWISKO_1@GMAIL.COM", "AQAAAAIAAYagAAAAEKMRccO2Verkfk3RPxARlpQAqt3gsBdwdJ5RMuGKSINMM3CUMVUmXajMF0p+N1zxmw==", null, false, "757f926c-a3bd-4f61-9b28-0b23906c791e", 0, false, "ImieNazwisko_1@gmail.com" },
                    { "d4fbdd82-4c81-4f99-83db-37f6e97951d9", 0, "0ba2f4ed-94d9-4800-ad4f-fe7055fb3b3f", "22.10.1983 12:12:12", "ImieNazwisko_3@gmail.com", true, "Imie_3", false, null, "Nazwisko_3", "IMIENAZWISKO_3@GMAIL.COM", "IMIENAZWISKO_3@GMAIL.COM", "AQAAAAIAAYagAAAAECBn5FnaRLyrjFn0PTL9tg+6rPvz7KtU00fzx26ngervnnKDFyqjEgiJKrVZZxOhSg==", null, false, "2cb6ed89-91eb-4f30-8333-d2c7cba1eaa9", 0, false, "ImieNazwisko_3@gmail.com" },
                    { "e9ecc7ac-dd2b-42b3-aab5-389611ab9f9c", 0, "1e0d0a7c-5cbc-4db2-a7e3-21c5bcc459c6", "22.10.1983 12:12:12", "ImieNazwisko_6@gmail.com", true, "Imie_6", false, null, "Nazwisko_6", "IMIENAZWISKO_6@GMAIL.COM", "IMIENAZWISKO_6@GMAIL.COM", "AQAAAAIAAYagAAAAEBCvXaeCdueh4Q1suPTlRFQY5ovZrl8Gy11bpPXFqiZw0Sqqfx0sxOVIbf9FP9Rmmw==", null, false, "112bace3-7694-4aeb-8372-3ebcbe340aeb", 0, false, "ImieNazwisko_6@gmail.com" },
                    { "ea8300de-b6bd-4e41-b50e-4d9134cf31e3", 0, "1e993dcf-b714-405d-9d0d-a159bddb7139", "22.10.1983 12:12:12", "ImieNazwisko_7@gmail.com", true, "Imie_7", false, null, "Nazwisko_7", "IMIENAZWISKO_7@GMAIL.COM", "IMIENAZWISKO_7@GMAIL.COM", "AQAAAAIAAYagAAAAENd4AZN3wXW4O/Jzu7UJIt4z6utBKJz0Ua+dfn31q7LTORACOzvJ5l+GsTC2o+GR/Q==", null, false, "d2b7a37f-9ee6-4a3a-8b90-ebebd5e6c2d9", 0, false, "ImieNazwisko_7@gmail.com" },
                    { "ec27c334-95bd-4048-9aa6-b9edec6fe3f6", 0, "4cbadc74-bdcc-4b3a-b782-8e900de6e344", "22.10.1983 12:12:12", "manager@manager.pl", true, "Janina", false, null, "Kowalska", "MANAGER@MANAGER.PL", "MANAGER@MANAGER.PL", "AQAAAAIAAYagAAAAEO492as/RxenzInzNTVXubFxmppBXTbopdbFxss72zRjfXocNRXl7lYLSMTN+3uq/w==", null, false, "bd556c89-f3b7-4510-a640-98a5f297f608", 0, false, "manager@manager.pl" },
                    { "f5e9921a-12dc-479b-822f-04f19e25f99c", 0, "f7389fa0-c5ba-4f29-976d-7d69a29bce5a", "22.10.1983 12:12:12", "ImieNazwisko_0@gmail.com", true, "Imie_0", false, null, "Nazwisko_0", "IMIENAZWISKO_0@GMAIL.COM", "IMIENAZWISKO_0@GMAIL.COM", "AQAAAAIAAYagAAAAEMvwynr3ksHdRaEdFCRcC9Oo3yAQ/8aAT8+JDXU7B0WaIqs4/bDbXtfi17JoMT0I5Q==", null, false, "73a90720-b4c3-42d2-96da-01861c855498", 0, false, "ImieNazwisko_0@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "KryteriaZnizek",
                columns: new[] { "KryteriaZnizkiId", "FlightBuyId", "Name" },
                values: new object[,]
                {
                    { "0dcab969-52e4-46f5-9d83-4310020be8cb", null, "Lot do Afryki" },
                    { "2414cf7d-948d-407c-851c-5195461d84fc", null, "Urodziny kupującego" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "FlightId", "DataDodania", "GodzinaWylotu", "Price", "TenantId", "TrasaDo", "TrasaOd" },
                values: new object[,]
                {
                    { "KLM 13946 BCA", "22.10.2024 04:02:07", "12:48", 155.0, "32f1c1ee-54ef-49ed-85a6-2790b7c362dd", "Afryka", "Rosja" },
                    { "KLM 15357 BCA", "22.10.2024 04:02:07", "8:59", 123.0, "32f1c1ee-54ef-49ed-85a6-2790b7c362dd", "Londyn", "Rosja" },
                    { "KLM 27173 BCA", "22.10.2024 04:02:07", "21:55", 193.0, "acff8f55-e5cf-4984-9ef5-fc5c1455e2a3", "Londyn", "Rosja" },
                    { "KLM 40323 BCA", "22.10.2024 04:02:07", "8:59", 147.0, "acff8f55-e5cf-4984-9ef5-fc5c1455e2a3", "Afryka", "Rosja" },
                    { "KLM 41351 BCA", "22.10.2024 04:02:07", "11:3", 175.0, "e9ecc7ac-dd2b-42b3-aab5-389611ab9f9c", "Londyn", "Rosja" },
                    { "KLM 43332 BCA", "22.10.2024 04:02:07", "5:36", 166.0, "cfc9e4bb-17dd-40fa-8fd1-7aad8e101f39", "Londyn", "Chiny" },
                    { "KLM 43334 BCA", "22.10.2024 04:02:07", "22:56", 165.0, "79f216ae-171a-44e2-961c-65eeaae68f42", "Afryka", "Hiszpania" },
                    { "KLM 50380 BCA", "22.10.2024 04:02:07", "20:13", 174.0, "f5e9921a-12dc-479b-822f-04f19e25f99c", "Afryka", "Chiny" },
                    { "KLM 50389 BCA", "22.10.2024 04:02:07", "6:40", 154.0, "acff8f55-e5cf-4984-9ef5-fc5c1455e2a3", "Londyn", "Rosja" },
                    { "KLM 53599 BCA", "22.10.2024 04:02:07", "12:27", 145.0, "d4fbdd82-4c81-4f99-83db-37f6e97951d9", "Afryka", "Chiny" },
                    { "KLM 54974 BCA", "22.10.2024 04:02:07", "5:42", 126.0, "b0c25e86-a029-4c25-9449-65409d7547ed", "Londyn", "Rosja" },
                    { "KLM 55113 BCA", "22.10.2024 04:02:07", "21:32", 184.0, "32f1c1ee-54ef-49ed-85a6-2790b7c362dd", "Afryka", "Chiny" },
                    { "KLM 60768 BCA", "22.10.2024 04:02:07", "15:47", 149.0, "f5e9921a-12dc-479b-822f-04f19e25f99c", "Afryka", "Chiny" },
                    { "KLM 72750 BCA", "22.10.2024 04:02:07", "18:40", 159.0, "32f1c1ee-54ef-49ed-85a6-2790b7c362dd", "Afryka", "Rosja" },
                    { "KLM 73784 BCA", "22.10.2024 04:02:07", "7:8", 111.0, "86d21392-9c66-45d4-957e-941830aafeec", "Afryka", "Chiny" },
                    { "KLM 77214 BCA", "22.10.2024 04:02:07", "9:44", 133.0, "86d21392-9c66-45d4-957e-941830aafeec", "Londyn", "Horwacja" },
                    { "KLM 79187 BCA", "22.10.2024 04:02:07", "6:51", 124.0, "b0c25e86-a029-4c25-9449-65409d7547ed", "Afryka", "Chiny" },
                    { "KLM 81832 BCA", "22.10.2024 04:02:07", "17:28", 100.0, "ea8300de-b6bd-4e41-b50e-4d9134cf31e3", "Londyn", "Hiszpania" },
                    { "KLM 84468 BCA", "22.10.2024 04:02:07", "6:9", 169.0, "b0c25e86-a029-4c25-9449-65409d7547ed", "Londyn", "Chiny" },
                    { "KLM 84603 BCA", "22.10.2024 04:02:07", "9:17", 169.0, "86d21392-9c66-45d4-957e-941830aafeec", "USA", "Hiszpania" },
                    { "KLM 88857 BCA", "22.10.2024 04:02:07", "2:53", 103.0, "79f216ae-171a-44e2-961c-65eeaae68f42", "Afryka", "Hiszpania" },
                    { "KLM 90535 BCA", "22.10.2024 04:02:07", "10:59", 110.0, "b0c25e86-a029-4c25-9449-65409d7547ed", "USA", "Rosja" },
                    { "KLM 90625 BCA", "22.10.2024 04:02:07", "10:43", 189.0, "32f1c1ee-54ef-49ed-85a6-2790b7c362dd", "Londyn", "Horwacja" },
                    { "KLM 98100 BCA", "22.10.2024 04:02:07", "13:32", 155.0, "86d21392-9c66-45d4-957e-941830aafeec", "Londyn", "Horwacja" },
                    { "KLM 99254 BCA", "22.10.2024 04:02:07", "6:7", 172.0, "d4fbdd82-4c81-4f99-83db-37f6e97951d9", "Londyn", "Chiny" }
                });

            migrationBuilder.InsertData(
                table: "DniWylotow",
                columns: new[] { "DzieWylotuId", "Dzien", "FlightId" },
                values: new object[,]
                {
                    { "07c18216-0f5a-461b-8c0f-c95500b09762", "Czwartek", "KLM 50380 BCA" },
                    { "0b8fb085-893d-499c-89d8-025fea274ad2", "Piątek", "KLM 55113 BCA" },
                    { "12788435-633e-4c87-ab55-84a644b949c8", "Czwartek", "KLM 90535 BCA" },
                    { "16ff2652-a93e-4fcd-805a-f7bbb417570b", "Środa", "KLM 60768 BCA" },
                    { "1991766e-9d55-4eb5-9193-acdf7029b5d8", "Poniedziałek", "KLM 84603 BCA" },
                    { "1b231f02-fa2b-4479-9920-a90989c5a59c", "Środa", "KLM 41351 BCA" },
                    { "1c4668e5-e009-47e0-844a-7bf2fd8a9a72", "Sobota", "KLM 15357 BCA" },
                    { "2558ec10-531c-4706-bf37-3818b1829d66", "Poniedziałek", "KLM 15357 BCA" },
                    { "26ad54b1-a39f-4297-a726-bc419d66ee16", "Środa", "KLM 27173 BCA" },
                    { "27fd9c68-ba82-4f2e-ac15-a531f3499e72", "Czwartek", "KLM 53599 BCA" },
                    { "2e9c918d-8e74-4f27-8461-255f419a5ee0", "Środa", "KLM 72750 BCA" },
                    { "399ae75b-95b5-4b76-82ee-4426d4b41493", "Sobota", "KLM 43332 BCA" },
                    { "3ad0debf-2d19-4e99-bca6-60760b411a13", "Piątek", "KLM 88857 BCA" },
                    { "3b5bfcd1-12e7-4d97-bbfe-d918c18c7089", "Piątek", "KLM 98100 BCA" },
                    { "3c537762-400d-4cde-87b1-4c3c1744db6d", "Piątek", "KLM 73784 BCA" },
                    { "3f0267da-ee84-4654-be08-063992d21824", "Sobota", "KLM 84468 BCA" },
                    { "3f8c45f4-dd88-4936-8ebd-83b1ae83b06f", "Sobota", "KLM 13946 BCA" },
                    { "4a33a206-9343-4532-b8b5-b816aa22226d", "Sobota", "KLM 88857 BCA" },
                    { "4a4b9370-b100-418f-83e8-c0faee7c8049", "Piątek", "KLM 54974 BCA" },
                    { "522fa43f-3153-4b4c-b783-92ba2b02f140", "Wtorek", "KLM 79187 BCA" },
                    { "53baea35-e8e9-4f03-9018-8bccd57571db", "Poniedziałek", "KLM 27173 BCA" },
                    { "63380c81-e187-473d-937e-ea85cde0bebb", "Czwartek", "KLM 40323 BCA" },
                    { "63cdf232-1d1e-44a5-935a-4c0138df7721", "Środa", "KLM 84468 BCA" },
                    { "684ab6d7-e39a-4fa1-9016-b0a27acdc167", "Piątek", "KLM 84603 BCA" },
                    { "69f986b2-27e6-4097-96fa-0e115b8db587", "Środa", "KLM 77214 BCA" },
                    { "6c432ad3-c94b-44f9-9bf4-b69203a6aca2", "Poniedziałek", "KLM 90535 BCA" },
                    { "6cbdd3bc-c081-41e6-b60f-1c1bab8c06f5", "Piątek", "KLM 43334 BCA" },
                    { "7658a761-91c3-4cc5-acdf-d7a89fd21817", "Środa", "KLM 40323 BCA" },
                    { "85c1b333-988b-4e29-b1b1-96f7e3b4d75e", "Sobota", "KLM 50389 BCA" },
                    { "8a14388c-ade4-489d-a860-91ea62623f87", "Piątek", "KLM 43334 BCA" },
                    { "8cc27acb-d434-4fef-ab60-1faa35804ece", "Wtorek", "KLM 54974 BCA" },
                    { "9d4ea8c9-24e2-4981-9248-7bb895866527", "Sobota", "KLM 60768 BCA" },
                    { "a6684911-9ad2-4aa8-89ff-ed8b8a2b6085", "Poniedziałek", "KLM 90625 BCA" },
                    { "a66bb424-7c63-42d3-ab1b-8422edcd0d0f", "Piątek", "KLM 50380 BCA" },
                    { "ab9f5839-c885-4abf-9717-1265422843a6", "Poniedziałek", "KLM 55113 BCA" },
                    { "b25ba05d-4d3e-4879-88c1-f30508a50bb5", "Sobota", "KLM 72750 BCA" },
                    { "b61ba099-42ff-4e3d-bbd9-5fc775189db1", "Czwartek", "KLM 73784 BCA" },
                    { "b9568a8c-60db-440b-a29e-0b853ce572b7", "Wtorek", "KLM 90625 BCA" },
                    { "bf995364-eba2-43ff-9b35-4359f2edf914", "Wtorek", "KLM 77214 BCA" },
                    { "c04bd78c-66c8-4757-9545-e813c7ee4e58", "Sobota", "KLM 98100 BCA" },
                    { "c0f024de-5c4d-4e35-8d67-c5f3e7556b61", "Wtorek", "KLM 81832 BCA" },
                    { "c8f1be26-92e1-479f-a8ef-3a4626759d3d", "Czwartek", "KLM 50389 BCA" },
                    { "ca0305b0-967e-49af-abc2-2c6488ad64a0", "Środa", "KLM 13946 BCA" },
                    { "ce87ae1d-0666-41d1-a896-b8cf277d06fb", "Sobota", "KLM 41351 BCA" },
                    { "d3afd974-62a4-4b3f-bdbe-1c8d393a0603", "Czwartek", "KLM 99254 BCA" },
                    { "dbcc8115-d5fb-48b0-b858-d2d8e7fba483", "Sobota", "KLM 43332 BCA" },
                    { "e27a2133-c920-49c3-9825-4216539ef268", "Czwartek", "KLM 81832 BCA" },
                    { "e9d797a8-a033-44c7-99aa-c1ea9d74a1ff", "Sobota", "KLM 79187 BCA" },
                    { "fae72a46-a115-40e9-bea5-64b2957a88ce", "Poniedziałek", "KLM 53599 BCA" },
                    { "fb4bfc24-bf5f-488b-849b-66733081223e", "Piątek", "KLM 99254 BCA" }
                });

            migrationBuilder.InsertData(
                table: "FlightBuys",
                columns: new[] { "FlightBuyId", "DataZakupu", "FlightId", "IloscBiletow", "PriceSuma", "TenantId" },
                values: new object[,]
                {
                    { "12bdd68d-eb4b-4ea3-92c4-b58ba8df2e6c", "22.10.2024 04:02:07", "KLM 43332 BCA", 1, 125.0, "f5e9921a-12dc-479b-822f-04f19e25f99c" },
                    { "21ca2fa0-936b-46a4-90a3-e910b28e0733", "22.10.2024 04:02:07", "KLM 41351 BCA", 1, 125.0, "79f216ae-171a-44e2-961c-65eeaae68f42" },
                    { "3dfe86f5-f959-4fce-a050-01d6564407ef", "22.10.2024 04:02:07", "KLM 50389 BCA", 1, 125.0, "79f216ae-171a-44e2-961c-65eeaae68f42" },
                    { "42ed7b91-7643-4c18-9b38-512da25a9a07", "22.10.2024 04:02:07", "KLM 13946 BCA", 1, 125.0, "b0c25e86-a029-4c25-9449-65409d7547ed" },
                    { "5886de59-dee7-4b0c-969f-c0bfedaa6287", "22.10.2024 04:02:07", "KLM 53599 BCA", 1, 125.0, "f5e9921a-12dc-479b-822f-04f19e25f99c" },
                    { "8caef6f3-c42a-45f0-88db-ea1cae40cb95", "22.10.2024 04:02:07", "KLM 84603 BCA", 1, 125.0, "b0c25e86-a029-4c25-9449-65409d7547ed" },
                    { "98e35ffa-0e28-4c12-8b1c-597333255f79", "22.10.2024 04:02:07", "KLM 84603 BCA", 1, 125.0, "79f216ae-171a-44e2-961c-65eeaae68f42" },
                    { "af5b9f91-0b03-4210-b260-50fe273d7399", "22.10.2024 04:02:07", "KLM 79187 BCA", 1, 125.0, "cfc9e4bb-17dd-40fa-8fd1-7aad8e101f39" },
                    { "c96e3db6-5a95-4c5a-b5b6-9441511378e7", "22.10.2024 04:02:07", "KLM 73784 BCA", 1, 125.0, "cfc9e4bb-17dd-40fa-8fd1-7aad8e101f39" },
                    { "f49f97e9-1da2-49f6-ab71-abdc60681784", "22.10.2024 04:02:07", "KLM 98100 BCA", 1, 125.0, "acff8f55-e5cf-4984-9ef5-fc5c1455e2a3" }
                });

            migrationBuilder.InsertData(
                table: "FlightBuysKryteriaZnizkis",
                columns: new[] { "FlightBuyId", "KryteriaZnizkiId" },
                values: new object[,]
                {
                    { "21ca2fa0-936b-46a4-90a3-e910b28e0733", "2414cf7d-948d-407c-851c-5195461d84fc" },
                    { "3dfe86f5-f959-4fce-a050-01d6564407ef", "2414cf7d-948d-407c-851c-5195461d84fc" },
                    { "42ed7b91-7643-4c18-9b38-512da25a9a07", "2414cf7d-948d-407c-851c-5195461d84fc" },
                    { "5886de59-dee7-4b0c-969f-c0bfedaa6287", "2414cf7d-948d-407c-851c-5195461d84fc" },
                    { "c96e3db6-5a95-4c5a-b5b6-9441511378e7", "0dcab969-52e4-46f5-9d83-4310020be8cb" }
                });

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
                name: "IX_DniWylotow_FlightId",
                table: "DniWylotow",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBuys_FlightId",
                table: "FlightBuys",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBuys_TenantId",
                table: "FlightBuys",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_FlightBuysKryteriaZnizkis_KryteriaZnizkiId",
                table: "FlightBuysKryteriaZnizkis",
                column: "KryteriaZnizkiId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_TenantId",
                table: "Flights",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_KryteriaZnizek_FlightBuyId",
                table: "KryteriaZnizek",
                column: "FlightBuyId");
        }

        /// <inheritdoc />
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
                name: "DniWylotow");

            migrationBuilder.DropTable(
                name: "FlightBuysKryteriaZnizkis");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "KryteriaZnizek");

            migrationBuilder.DropTable(
                name: "FlightBuys");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
