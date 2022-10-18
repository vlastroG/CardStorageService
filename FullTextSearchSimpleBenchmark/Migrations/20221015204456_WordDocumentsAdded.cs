﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FullTextSearchSimpleBenchmark.Migrations
{
    public partial class WordDocumentsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WordDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordDocuments_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WordDocuments_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WordDocuments_DocumentId",
                table: "WordDocuments",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_WordDocuments_WordId",
                table: "WordDocuments",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_WordDocuments_WordId_DocumentId",
                table: "WordDocuments",
                columns: new[] { "WordId", "DocumentId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WordDocuments");
        }
    }
}
