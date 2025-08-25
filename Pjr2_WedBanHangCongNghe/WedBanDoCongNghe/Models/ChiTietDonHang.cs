using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WedBanDoCongNghe.Models;

public partial class ChiTietDonHang
{
    [Key] // Khóa chính
    public int CtdhId { get; set; }

    [Required]
    public int DonHangId { get; set; }

    [Required]
    public int SanPhamId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải >= 1")]
    public int SoLuong { get; set; }

    [Column(TypeName = "decimal(18,2)")] // Định nghĩa kiểu trong DB
    [Range(0, double.MaxValue, ErrorMessage = "Giá bán phải >= 0")]
    public decimal GiaBan { get; set; }

    // Quan hệ nhiều-1: Một chi tiết đơn hàng thuộc về một đơn hàng
    public virtual DonHang DonHang { get; set; } = null!;

    // Quan hệ nhiều-1: Một chi tiết đơn hàng thuộc về một sản phẩm
    public virtual SanPham SanPham { get; set; } = null!;
}
