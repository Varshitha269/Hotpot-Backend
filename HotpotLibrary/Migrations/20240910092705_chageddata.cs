using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotpotLibrary.Migrations
{
    /// <inheritdoc />
    public partial class chageddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameColumn(
                name: "ImageURL",
                table: "MenuItems",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "FeedbackRatings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackRatings_UserID",
                table: "FeedbackRatings",
                newName: "IX_FeedbackRatings_UserId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Cart",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_UserID",
                table: "Cart",
                newName: "IX_Cart_UserId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryAddress",
                table: "Orders",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                newName: "IX_Orders_UserID");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "MenuItems",
                newName: "ImageURL");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FeedbackRatings",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_FeedbackRatings_UserId",
                table: "FeedbackRatings",
                newName: "IX_FeedbackRatings_UserID");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Cart",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_UserId",
                table: "Cart",
                newName: "IX_Cart_UserID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeliveryAddress",
                table: "Orders",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200);
        }
    }
}
