using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Shared.Featrues.Migrations.HowlDB
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "howl");

            migrationBuilder.CreateTable(
                name: "howl_messages",
                schema: "howl",
                columns: table => new
                {
                    unique_key = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_uid = table.Column<long>(type: "bigint", nullable: false),
                    message = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    create_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_howl_messages", x => new { x.unique_key, x.user_uid });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "howl_messages",
                schema: "howl");
        }
    }
}
