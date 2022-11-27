using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace studentriskhero.Migrations
{
    /// <inheritdoc />
    public partial class RiskProfileRelationsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RiskProfiles_StudentId",
                table: "RiskProfiles",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskProfileEvidences_RiskProfileId",
                table: "RiskProfileEvidences",
                column: "RiskProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskProfileEntries_RiskProfileId",
                table: "RiskProfileEntries",
                column: "RiskProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentStudents_AssignmentId",
                table: "AssignmentStudents",
                column: "AssignmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentStudents_Assignments_AssignmentId",
                table: "AssignmentStudents",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RiskProfileEntries_RiskProfiles_RiskProfileId",
                table: "RiskProfileEntries",
                column: "RiskProfileId",
                principalTable: "RiskProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RiskProfileEvidences_RiskProfiles_RiskProfileId",
                table: "RiskProfileEvidences",
                column: "RiskProfileId",
                principalTable: "RiskProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RiskProfiles_Students_StudentId",
                table: "RiskProfiles",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentStudents_Assignments_AssignmentId",
                table: "AssignmentStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskProfileEntries_RiskProfiles_RiskProfileId",
                table: "RiskProfileEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskProfileEvidences_RiskProfiles_RiskProfileId",
                table: "RiskProfileEvidences");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskProfiles_Students_StudentId",
                table: "RiskProfiles");

            migrationBuilder.DropIndex(
                name: "IX_RiskProfiles_StudentId",
                table: "RiskProfiles");

            migrationBuilder.DropIndex(
                name: "IX_RiskProfileEvidences_RiskProfileId",
                table: "RiskProfileEvidences");

            migrationBuilder.DropIndex(
                name: "IX_RiskProfileEntries_RiskProfileId",
                table: "RiskProfileEntries");

            migrationBuilder.DropIndex(
                name: "IX_AssignmentStudents_AssignmentId",
                table: "AssignmentStudents");
        }
    }
}
