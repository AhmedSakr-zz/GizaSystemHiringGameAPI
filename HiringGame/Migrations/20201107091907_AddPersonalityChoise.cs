using Microsoft.EntityFrameworkCore.Migrations;

namespace HiringGame.Migrations
{
    public partial class AddPersonalityChoise : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Answers_SelectedAnswerId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SelectedAnswerId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SelectedAnswerId",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonalityAnswerChoiceId",
                table: "Transactions",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrectAnswer",
                table: "Answers",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Answers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PersonalityAnswerChoice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    ChoiceValue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityAnswerChoice", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PersonalityAnswerChoice",
                columns: new[] { "Id", "ChoiceValue", "Name" },
                values: new object[,]
                {
                    { 1, 1, "MOST" },
                    { 2, 0, "NOT SURE" },
                    { 3, -1, "LEAST" }
                });

            migrationBuilder.InsertData(
                table: "QuestionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Level 2" },
                    { 2, "Level 3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AnswerId",
                table: "Transactions",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PersonalityAnswerChoiceId",
                table: "Transactions",
                column: "PersonalityAnswerChoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Answers_AnswerId",
                table: "Transactions",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PersonalityAnswerChoice_PersonalityAnswerChoiceId",
                table: "Transactions",
                column: "PersonalityAnswerChoiceId",
                principalTable: "PersonalityAnswerChoice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Answers_AnswerId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PersonalityAnswerChoice_PersonalityAnswerChoiceId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "PersonalityAnswerChoice");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_AnswerId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PersonalityAnswerChoiceId",
                table: "Transactions");

            migrationBuilder.DeleteData(
                table: "QuestionTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "QuestionTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PersonalityAnswerChoiceId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Answers");

            migrationBuilder.AddColumn<int>(
                name: "SelectedAnswerId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsCorrectAnswer",
                table: "Answers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SelectedAnswerId",
                table: "Transactions",
                column: "SelectedAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Answers_SelectedAnswerId",
                table: "Transactions",
                column: "SelectedAnswerId",
                principalTable: "Answers",
                principalColumn: "Id");
        }
    }
}
