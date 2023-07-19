using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lista7_zad1.Migrations
{
    /// <inheritdoc />
    public partial class nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Classifications_ClassificationID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_GradeVals_GradeValID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Professors_ProfessorID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Subjects_SubjectID",
                table: "Grades");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectID",
                table: "Grades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "Grades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProfessorID",
                table: "Grades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GradeValID",
                table: "Grades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClassificationID",
                table: "Grades",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Classifications_ClassificationID",
                table: "Grades",
                column: "ClassificationID",
                principalTable: "Classifications",
                principalColumn: "ClassificationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_GradeVals_GradeValID",
                table: "Grades",
                column: "GradeValID",
                principalTable: "GradeVals",
                principalColumn: "GradeValID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Professors_ProfessorID",
                table: "Grades",
                column: "ProfessorID",
                principalTable: "Professors",
                principalColumn: "ProfessorID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentID",
                table: "Grades",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Subjects_SubjectID",
                table: "Grades",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Classifications_ClassificationID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_GradeVals_GradeValID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Professors_ProfessorID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Students_StudentID",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Subjects_SubjectID",
                table: "Grades");

            migrationBuilder.AlterColumn<int>(
                name: "SubjectID",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentID",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProfessorID",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GradeValID",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClassificationID",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Classifications_ClassificationID",
                table: "Grades",
                column: "ClassificationID",
                principalTable: "Classifications",
                principalColumn: "ClassificationID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_GradeVals_GradeValID",
                table: "Grades",
                column: "GradeValID",
                principalTable: "GradeVals",
                principalColumn: "GradeValID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Professors_ProfessorID",
                table: "Grades",
                column: "ProfessorID",
                principalTable: "Professors",
                principalColumn: "ProfessorID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Students_StudentID",
                table: "Grades",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Subjects_SubjectID",
                table: "Grades",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
