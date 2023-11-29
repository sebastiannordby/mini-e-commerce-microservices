using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeperatedInvoiceAndDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PostalOffice",
                table: "Orders",
                newName: "InvoiceAddressPostalOffice");

            migrationBuilder.RenameColumn(
                name: "PostalCode",
                table: "Orders",
                newName: "InvoiceAddressPostalCode");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Orders",
                newName: "InvoiceAddressLine");

            migrationBuilder.RenameColumn(
                name: "AddressLine",
                table: "Orders",
                newName: "InvoiceAddressCountry");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddressCountry",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddressLine",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddressPostalCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddressPostalOffice",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryAddressCountry",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddressLine",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddressPostalCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddressPostalOffice",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "InvoiceAddressPostalOffice",
                table: "Orders",
                newName: "PostalOffice");

            migrationBuilder.RenameColumn(
                name: "InvoiceAddressPostalCode",
                table: "Orders",
                newName: "PostalCode");

            migrationBuilder.RenameColumn(
                name: "InvoiceAddressLine",
                table: "Orders",
                newName: "Country");

            migrationBuilder.RenameColumn(
                name: "InvoiceAddressCountry",
                table: "Orders",
                newName: "AddressLine");
        }
    }
}
