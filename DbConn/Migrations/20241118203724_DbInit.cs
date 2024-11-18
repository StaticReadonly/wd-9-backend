using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.DbConn.Migrations
{
    /// <inheritdoc />
    public partial class DbInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    Unit = table.Column<string>(type: "varchar", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    URL = table.Column<string>(type: "varchar", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Time = table.Column<TimeSpan>(type: "Time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Name = table.Column<string>(type: "varchar", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Email = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "varchar", maxLength: 20, nullable: false),
                    First_name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    Last_Name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Recipe_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Ingredient_ID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => new { x.Recipe_ID, x.Ingredient_ID });
                    table.ForeignKey(
                        name: "FK_Components_Ingredients_Ingredient_ID",
                        column: x => x.Ingredient_ID,
                        principalTable: "Ingredients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Components_Recipes_Recipe_ID",
                        column: x => x.Recipe_ID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Recipe_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Dishes_Recipes_Recipe_ID",
                        column: x => x.Recipe_ID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cooking",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Recipe_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Step_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Step_Number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cooking", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cooking_Recipes_Recipe_ID",
                        column: x => x.Recipe_ID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cooking_Steps_Step_ID",
                        column: x => x.Step_ID,
                        principalTable: "Steps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Recipe_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    User_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "varchar", maxLength: 500, nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_Recipes_Recipe_ID",
                        column: x => x.Recipe_ID,
                        principalTable: "Recipes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dishes_Tags",
                columns: table => new
                {
                    Dish_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Tag_ID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes_Tags", x => new { x.Dish_ID, x.Tag_ID });
                    table.ForeignKey(
                        name: "FK_Dishes_Tags_Dishes_Dish_ID",
                        column: x => x.Dish_ID,
                        principalTable: "Dishes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dishes_Tags_Tags_Tag_ID",
                        column: x => x.Tag_ID,
                        principalTable: "Tags",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus_Dishes",
                columns: table => new
                {
                    Dish_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Menu_ID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus_Dishes", x => new { x.Dish_ID, x.Menu_ID });
                    table.ForeignKey(
                        name: "FK_Menus_Dishes_Dishes_Dish_ID",
                        column: x => x.Dish_ID,
                        principalTable: "Dishes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Menus_Dishes_Menus_Menu_ID",
                        column: x => x.Menu_ID,
                        principalTable: "Menus",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cookings_Materials",
                columns: table => new
                {
                    Cooking_ID = table.Column<Guid>(type: "uuid", nullable: false),
                    Material_ID = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cookings_Materials", x => new { x.Cooking_ID, x.Material_ID });
                    table.ForeignKey(
                        name: "FK_Cookings_Materials_Cooking_Cooking_ID",
                        column: x => x.Cooking_ID,
                        principalTable: "Cooking",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cookings_Materials_Materials_Material_ID",
                        column: x => x.Material_ID,
                        principalTable: "Materials",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Recipe_ID",
                table: "Comments",
                column: "Recipe_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_User_ID",
                table: "Comments",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Components_Ingredient_ID",
                table: "Components",
                column: "Ingredient_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Cooking_Recipe_ID_Step_ID_Step_Number",
                table: "Cooking",
                columns: new[] { "Recipe_ID", "Step_ID", "Step_Number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cooking_Step_ID",
                table: "Cooking",
                column: "Step_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cookings_Materials_Material_ID",
                table: "Cookings_Materials",
                column: "Material_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_Name",
                table: "Dishes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_Recipe_ID",
                table: "Dishes",
                column: "Recipe_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_Tags_Tag_ID",
                table: "Dishes_Tags",
                column: "Tag_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Name",
                table: "Ingredients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Name",
                table: "Menus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Dishes_Menu_ID",
                table: "Menus_Dishes",
                column: "Menu_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Cookings_Materials");

            migrationBuilder.DropTable(
                name: "Dishes_Tags");

            migrationBuilder.DropTable(
                name: "Menus_Dishes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Cooking");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
