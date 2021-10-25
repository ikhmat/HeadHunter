using Microsoft.EntityFrameworkCore.Migrations;

namespace HeadHunter.Migrations
{
    public partial class ChangedChatAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_ReceiverId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_SenderId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Chats",
                newName: "SecondUserId");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "Chats",
                newName: "FirstUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_SenderId",
                table: "Chats",
                newName: "IX_Chats_SecondUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_ReceiverId",
                table: "Chats",
                newName: "IX_Chats_FirstUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_FirstUserId",
                table: "Chats",
                column: "FirstUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_SecondUserId",
                table: "Chats",
                column: "SecondUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_FirstUserId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_AspNetUsers_SecondUserId",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "SecondUserId",
                table: "Chats",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "FirstUserId",
                table: "Chats",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_SecondUserId",
                table: "Chats",
                newName: "IX_Chats_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Chats_FirstUserId",
                table: "Chats",
                newName: "IX_Chats_ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_ReceiverId",
                table: "Chats",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_AspNetUsers_SenderId",
                table: "Chats",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
