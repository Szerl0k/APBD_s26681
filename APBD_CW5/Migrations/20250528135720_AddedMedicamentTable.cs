using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APBD_CW5.Migrations
{
    /// <inheritdoc />
    public partial class AddedMedicamentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicament",
                columns: table => new
                {
                    IdMedicament = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicament", x => x.IdMedicament);
                });

            migrationBuilder.InsertData(
                table: "Medicament",
                columns: new[] { "IdMedicament", "Description", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Typical painkiller", "Paracetamol", "Painkiller" },
                    { 2, "Reduces inflammation and pain", "Naproxen", "Anti-inflammatory" },
                    { 3, "Broad spectrum antibiotic", "Amoxicillin", "Antibiotic" },
                    { 4, "Lowers blood glucose", "Metformin", "Antidiabetic" },
                    { 5, "aaaa", "Aspirin", "Painkiller" },
                    { 6, "Some drug that does something", "Some drug", "Painkiller" },
                    { 7, "Does some 'a'", "aaaaaaaaaaaaa", "a blocker" },
                    { 8, "Does some 'b'", "bbbbbbbb", "b blocker" },
                    { 9, "Does some 'c'", "cccccccc", "c blocker" },
                    { 10, "Does some 'd'", "dddddddd", "d blocker" },
                    { 11, "Does some 'e'", "eeeeeeeeee", "ee blocker" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medicament");
        }
    }
}
