using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciadorEscolar.Api.Migrations
{
    public partial class corrigeFkTurmaEscola : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turma_Escola_Id",
                table: "Turma");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_EscolaId",
                table: "Turma",
                column: "EscolaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turma_Escola_EscolaId",
                table: "Turma",
                column: "EscolaId",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turma_Escola_EscolaId",
                table: "Turma");

            migrationBuilder.DropIndex(
                name: "IX_Turma_EscolaId",
                table: "Turma");

            migrationBuilder.AddForeignKey(
                name: "FK_Turma_Escola_Id",
                table: "Turma",
                column: "Id",
                principalTable: "Escola",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
