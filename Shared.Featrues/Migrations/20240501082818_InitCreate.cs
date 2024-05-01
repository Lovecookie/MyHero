using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Shared.Featrues.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "account");

            migrationBuilder.CreateTable(
                name: "user_basics",
                schema: "account",
                columns: table => new
                {
                    user_uid = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    encrypted_uid = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    user_id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    password = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    picture_url = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    date_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_basics", x => x.user_uid);
                });

            migrationBuilder.CreateTable(
                name: "user_patronages",
                schema: "account",
                columns: table => new
                {
                    user_uid = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    follower_count = table.Column<long>(type: "bigint", nullable: false),
                    following_count = table.Column<long>(type: "bigint", nullable: false),
                    style_count = table.Column<long>(type: "bigint", nullable: false),
                    favorite_count = table.Column<long>(type: "bigint", nullable: false),
                    date_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_patronages", x => x.user_uid);
                });

            migrationBuilder.CreateTable(
                name: "user_recognitions",
                schema: "account",
                columns: table => new
                {
                    user_uid = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    famous_value = table.Column<long>(type: "bigint", nullable: false),
                    date_created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    date_modified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_recognitions", x => x.user_uid);
                });

            migrationBuilder.CreateIndex(
                name: "UIX_UserBasic_Email",
                schema: "account",
                table: "user_basics",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UIX_UserBasic_UserId",
                schema: "account",
                table: "user_basics",
                column: "user_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_basics",
                schema: "account");

            migrationBuilder.DropTable(
                name: "user_patronages",
                schema: "account");

            migrationBuilder.DropTable(
                name: "user_recognitions",
                schema: "account");
        }
    }
}
