using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerService.Migrations
{
    /// <inheritdoc />
    public partial class migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_CUSTOMER_ORDER",
                columns: table => new
                {
                    CUSTOMERORDERID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CUSTOMERID = table.Column<int>(type: "int", nullable: false),
                    ORDERRECEIVEDBY = table.Column<int>(type: "int", nullable: false),
                    ORDERDATE = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AMOUNT = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    DELIVERYADDRESS = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DELETED = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_CUSTOMER_ORDER", x => x.CUSTOMERORDERID);
                    table.ForeignKey(
                        name: "FK_TBL_CUSTOMER_ORDER_TBL_CUSTOMER_CUSTOMERID",
                        column: x => x.CUSTOMERID,
                        principalTable: "TBL_CUSTOMER",
                        principalColumn: "CUSTOMERID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_CUSTOMER_ORDER_CUSTOMERID",
                table: "TBL_CUSTOMER_ORDER",
                column: "CUSTOMERID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_CUSTOMER_ORDER");
        }
    }
}
