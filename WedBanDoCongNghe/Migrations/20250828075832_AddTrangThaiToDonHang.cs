using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WedBanDoCongNghe.Migrations
{
    
    public partial class AddTrangThaiToDonHang : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "DonHangs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "DonHangs");
        }
    }
}
