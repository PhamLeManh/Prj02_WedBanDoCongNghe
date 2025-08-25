using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WedBanDoCongNghe.Models;

public partial class SanPham
{
    [Key] // Khóa chính
    public int SanPhamId { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [StringLength(100)]
    public string TenSp { get; set; } = null!;

    [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm phải >= 0")]
    public decimal Gia { get; set; }

    [StringLength(500)]
    public string? MoTa { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được âm")]
    public int SoLuong { get; set; }

    [StringLength(255)]
    public string? HinhAnh { get; set; }

    // Quan hệ 1-nhiều: 1 sản phẩm có nhiều chi tiết đơn hàng
    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
}
