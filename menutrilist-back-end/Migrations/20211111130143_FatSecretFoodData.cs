using Microsoft.EntityFrameworkCore.Migrations;

namespace Menutrilist.Migrations
{
    public partial class FatSecretFoodData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetLinkToken_AspNetUser_UserId",
                table: "AspNetLinkToken");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRefreshToken_AspNetUser_UserId",
                table: "AspNetRefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRefreshToken",
                table: "AspNetRefreshToken");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetLinkToken",
                table: "AspNetLinkToken");

            migrationBuilder.RenameTable(
                name: "AspNetRefreshToken",
                newName: "AspNetRefreshTokens");

            migrationBuilder.RenameTable(
                name: "AspNetLinkToken",
                newName: "AspNetLinkTokens");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRefreshToken_UserId",
                table: "AspNetRefreshTokens",
                newName: "IX_AspNetRefreshTokens_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetLinkToken_UserId",
                table: "AspNetLinkTokens",
                newName: "IX_AspNetLinkTokens_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRefreshTokens",
                table: "AspNetRefreshTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetLinkTokens",
                table: "AspNetLinkTokens",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    FoodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FoodNutritionServings",
                columns: table => new
                {
                    ServingId = table.Column<int>(type: "int", nullable: false),
                    Calcium = table.Column<int>(type: "int", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Carbohydrate = table.Column<float>(type: "real", nullable: false),
                    Cholesterol = table.Column<int>(type: "int", nullable: false),
                    Fat = table.Column<float>(type: "real", nullable: false),
                    Fiber = table.Column<float>(type: "real", nullable: false),
                    Iron = table.Column<float>(type: "real", nullable: false),
                    MonosaturatedFat = table.Column<float>(type: "real", nullable: false),
                    Sodium = table.Column<int>(type: "int", nullable: false),
                    Sugar = table.Column<float>(type: "real", nullable: false),
                    VitaminA = table.Column<int>(type: "int", nullable: false),
                    VitaminC = table.Column<float>(type: "real", nullable: false),
                    PolyunsaturatedFat = table.Column<float>(type: "real", nullable: false),
                    Pottasium = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<float>(type: "real", nullable: false),
                    SaturatedFat = table.Column<float>(type: "real", nullable: false),
                    ServingDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServingUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetricServingAmount = table.Column<float>(type: "real", nullable: false),
                    MetricServingUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfUnits = table.Column<float>(type: "real", nullable: false),
                    FoodId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodNutritionServings", x => x.ServingId);
                    table.ForeignKey(
                        name: "FK_FoodNutritionServings_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodNutritionServings_FoodId",
                table: "FoodNutritionServings",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetLinkTokens_AspNetUser_UserId",
                table: "AspNetLinkTokens",
                column: "UserId",
                principalTable: "AspNetUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRefreshTokens_AspNetUser_UserId",
                table: "AspNetRefreshTokens",
                column: "UserId",
                principalTable: "AspNetUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetLinkTokens_AspNetUser_UserId",
                table: "AspNetLinkTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRefreshTokens_AspNetUser_UserId",
                table: "AspNetRefreshTokens");

            migrationBuilder.DropTable(
                name: "FoodNutritionServings");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetRefreshTokens",
                table: "AspNetRefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetLinkTokens",
                table: "AspNetLinkTokens");

            migrationBuilder.RenameTable(
                name: "AspNetRefreshTokens",
                newName: "AspNetRefreshToken");

            migrationBuilder.RenameTable(
                name: "AspNetLinkTokens",
                newName: "AspNetLinkToken");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetRefreshTokens_UserId",
                table: "AspNetRefreshToken",
                newName: "IX_AspNetRefreshToken_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetLinkTokens_UserId",
                table: "AspNetLinkToken",
                newName: "IX_AspNetLinkToken_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetRefreshToken",
                table: "AspNetRefreshToken",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetLinkToken",
                table: "AspNetLinkToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetLinkToken_AspNetUser_UserId",
                table: "AspNetLinkToken",
                column: "UserId",
                principalTable: "AspNetUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRefreshToken_AspNetUser_UserId",
                table: "AspNetRefreshToken",
                column: "UserId",
                principalTable: "AspNetUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
