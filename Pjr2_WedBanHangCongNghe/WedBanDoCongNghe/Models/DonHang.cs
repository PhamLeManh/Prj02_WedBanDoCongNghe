using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WedBanDoCongNghe.Models;

public partial class DonHang
{
    [Key] // Khóa chính
    public int DonHangId { get; set; }

    [Required] // Bắt buộc có UserId
    public int UserId { get; set; }

    [DataType(DataType.Date)]
    public DateTime? NgayDat { get; set; } = DateTime.Now;

    [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền phải >= 0")]
    public decimal? TongTien { get; set; }

    [StringLength(50)]
    public string? TrangThai { get; set; } = "Chờ xử lý"; // mặc định khi tạo đơn

    // Quan hệ 1-nhiều: 1 đơn hàng có nhiều chi tiết đơn hàng
    public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();

    // Quan hệ nhiều-1: 1 đơn hàng thuộc về 1 user
    public virtual User User { get; set; } = null!;
}
