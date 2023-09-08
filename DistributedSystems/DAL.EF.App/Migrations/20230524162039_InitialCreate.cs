using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    LastName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CollectionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollectionTypeName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventTypeName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MonthNr = table.Column<int>(type: "integer", nullable: false),
                    MonthName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PestSeverities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PestSeverityName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PestSeverities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PestTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PestTypeName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PestTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SizeCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SizeName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SizeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagColors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ColorName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ColorHex = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RefreshToken = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ExpirationDT = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PreviousRefreshToken = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    PreviousExpirationDT = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppRefreshTokens_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantCollections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CollectionName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CollectionTypeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantCollections_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlantCollections_CollectionTypes_CollectionTypeId",
                        column: x => x.CollectionTypeId,
                        principalTable: "CollectionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEntryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryTypeName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EventTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEntryTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEntryTypes_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReminderTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReminderTypeName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    EventTypeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReminderTypes_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    PlantFamily = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    ScientificName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SizeCategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plants_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plants_SizeCategories_SizeCategoryId",
                        column: x => x.SizeCategoryId,
                        principalTable: "SizeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TagLabel = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    TagColorId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tags_TagColors_TagColorId",
                        column: x => x.TagColorId,
                        principalTable: "TagColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntryComment = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EntryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    HistoryEntryTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoryEntries_HistoryEntryTypes_HistoryEntryTypeId",
                        column: x => x.HistoryEntryTypeId,
                        principalTable: "HistoryEntryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoryEntries_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PestComment = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    PestDiscoveryTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    PestTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PestSeverityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pests_PestSeverities_PestSeverityId",
                        column: x => x.PestSeverityId,
                        principalTable: "PestSeverities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pests_PestTypes_PestTypeId",
                        column: x => x.PestTypeId,
                        principalTable: "PestTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pests_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    ImageDescription = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlantsInCollections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantCollectionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantsInCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantsInCollections_PlantCollections_PlantCollectionId",
                        column: x => x.PlantCollectionId,
                        principalTable: "PlantCollections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantsInCollections_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReminderFrequency = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReminderMessage = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReminderTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminders_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reminders_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reminders_ReminderTypes_ReminderTypeId",
                        column: x => x.ReminderTypeId,
                        principalTable: "ReminderTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlantTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlantId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlantTags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlantTags_Plants_PlantId",
                        column: x => x.PlantId,
                        principalTable: "Plants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlantTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReminderActiveMonths",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReminderId = table.Column<Guid>(type: "uuid", nullable: false),
                    MonthId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderActiveMonths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReminderActiveMonths_Months_MonthId",
                        column: x => x.MonthId,
                        principalTable: "Months",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReminderActiveMonths_Reminders_ReminderId",
                        column: x => x.ReminderId,
                        principalTable: "Reminders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRefreshTokens_AppUserId",
                table: "AppRefreshTokens",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_HistoryEntryTypeId",
                table: "HistoryEntries",
                column: "HistoryEntryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntries_PlantId",
                table: "HistoryEntries",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEntryTypes_EventTypeId",
                table: "HistoryEntryTypes",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pests_PestSeverityId",
                table: "Pests",
                column: "PestSeverityId");

            migrationBuilder.CreateIndex(
                name: "IX_Pests_PestTypeId",
                table: "Pests",
                column: "PestTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Pests_PlantId",
                table: "Pests",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PlantId",
                table: "Photos",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantCollections_AppUserId",
                table: "PlantCollections",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantCollections_CollectionTypeId",
                table: "PlantCollections",
                column: "CollectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_AppUserId",
                table: "Plants",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Plants_SizeCategoryId",
                table: "Plants",
                column: "SizeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantsInCollections_PlantCollectionId",
                table: "PlantsInCollections",
                column: "PlantCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantsInCollections_PlantId",
                table: "PlantsInCollections",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantTags_PlantId",
                table: "PlantTags",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_PlantTags_TagId",
                table: "PlantTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ReminderActiveMonths_MonthId",
                table: "ReminderActiveMonths",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_ReminderActiveMonths_ReminderId",
                table: "ReminderActiveMonths",
                column: "ReminderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_AppUserId",
                table: "Reminders",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_PlantId",
                table: "Reminders",
                column: "PlantId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_ReminderTypeId",
                table: "Reminders",
                column: "ReminderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReminderTypes_EventTypeId",
                table: "ReminderTypes",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_AppUserId",
                table: "Tags",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagColorId",
                table: "Tags",
                column: "TagColorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRefreshTokens");

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
                name: "HistoryEntries");

            migrationBuilder.DropTable(
                name: "Pests");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "PlantsInCollections");

            migrationBuilder.DropTable(
                name: "PlantTags");

            migrationBuilder.DropTable(
                name: "ReminderActiveMonths");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "HistoryEntryTypes");

            migrationBuilder.DropTable(
                name: "PestSeverities");

            migrationBuilder.DropTable(
                name: "PestTypes");

            migrationBuilder.DropTable(
                name: "PlantCollections");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "CollectionTypes");

            migrationBuilder.DropTable(
                name: "TagColors");

            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "ReminderTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SizeCategories");

            migrationBuilder.DropTable(
                name: "EventTypes");
        }
    }
}
