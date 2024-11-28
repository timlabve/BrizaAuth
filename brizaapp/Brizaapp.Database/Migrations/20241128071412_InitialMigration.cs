using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Brizaapp.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.CreateTable(
                name: "identity_role",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "identity_user",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Disabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
                    ModifiedById = table.Column<int>(type: "integer", nullable: true, defaultValue: 1),
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
                    table.PrimaryKey("PK_identity_user", x => x.Id);
                    table.ForeignKey(
                        name: "FK_identity_user_identity_user_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "identity",
                        principalTable: "identity_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_identity_user_identity_user_ModifiedById",
                        column: x => x.ModifiedById,
                        principalSchema: "identity",
                        principalTable: "identity_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "identity_identityroleclaim<int>",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_identityroleclaim<int>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_identity_identityroleclaim<int>_identity_role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "identity",
                        principalTable: "identity_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_identityuserclaim<int>",
                schema: "identity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_identityuserclaim<int>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_identity_identityuserclaim<int>_identity_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "identity_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_identityuserlogin<int>",
                schema: "identity",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_identityuserlogin<int>", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_identity_identityuserlogin<int>_identity_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "identity_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_identityuserrole<int>",
                schema: "identity",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_identityuserrole<int>", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_identity_identityuserrole<int>_identity_role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "identity",
                        principalTable: "identity_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_identity_identityuserrole<int>_identity_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "identity_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_identityusertoken<int>",
                schema: "identity",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_identityusertoken<int>", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_identity_identityusertoken<int>_identity_user_UserId",
                        column: x => x.UserId,
                        principalSchema: "identity",
                        principalTable: "identity_user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_identity_identityroleclaim<int>_RoleId",
                schema: "identity",
                table: "identity_identityroleclaim<int>",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_identity_identityuserclaim<int>_UserId",
                schema: "identity",
                table: "identity_identityuserclaim<int>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_identity_identityuserlogin<int>_UserId",
                schema: "identity",
                table: "identity_identityuserlogin<int>",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_identity_identityuserrole<int>_RoleId",
                schema: "identity",
                table: "identity_identityuserrole<int>",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "identity",
                table: "identity_role",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "identity",
                table: "identity_user",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_identity_user_CreatedById",
                schema: "identity",
                table: "identity_user",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_identity_user_ModifiedById",
                schema: "identity",
                table: "identity_user",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "identity",
                table: "identity_user",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "identity_identityroleclaim<int>",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "identity_identityuserclaim<int>",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "identity_identityuserlogin<int>",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "identity_identityuserrole<int>",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "identity_identityusertoken<int>",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "identity_role",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "identity_user",
                schema: "identity");
        }
    }
}
