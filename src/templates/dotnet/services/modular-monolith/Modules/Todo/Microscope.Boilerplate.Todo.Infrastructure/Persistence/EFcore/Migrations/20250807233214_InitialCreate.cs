using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microscope.Boilerplate.Todo.Infrastructure.Persistence.EFcore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "efcore");

            migrationBuilder.CreateTable(
                name: "TodoLists",
                schema: "efcore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorMail = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    TenantId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                schema: "efcore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    TodoListId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_TodoLists_TodoListId",
                        column: x => x.TodoListId,
                        principalSchema: "efcore",
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TodoItem",
                schema: "efcore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Label = table.Column<string>(type: "text", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    TodoListId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItem_TodoLists_TodoListId",
                        column: x => x.TodoListId,
                        principalSchema: "efcore",
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tag_TodoListId",
                schema: "efcore",
                table: "Tag",
                column: "TodoListId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_TodoListId",
                schema: "efcore",
                table: "TodoItem",
                column: "TodoListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tag",
                schema: "efcore");

            migrationBuilder.DropTable(
                name: "TodoItem",
                schema: "efcore");

            migrationBuilder.DropTable(
                name: "TodoLists",
                schema: "efcore");
        }
    }
}
