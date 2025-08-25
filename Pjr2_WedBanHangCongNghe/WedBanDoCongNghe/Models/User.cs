using System.ComponentModel.DataAnnotations;

namespace WedBanDoCongNghe.Models;

public partial class User
{
    [Key] // Khóa chính
    public int UserId { get; set; }

    [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
    [StringLength(50, ErrorMessage = "Tên đăng nhập tối đa 50 ký tự")]
    public string TenDangNhap { get; set; } = null!;

    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    [StringLength(100, ErrorMessage = "Mật khẩu tối đa 100 ký tự")]
    public string MatKhau { get; set; } = null!;

    [Required(ErrorMessage = "Email không được để trống")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(20)]
    public string VaiTro { get; set; } = "KhachHang"; // mặc định là khách hàng

    // Một User có nhiều đơn hàng
    public virtual ICollection<DonHang> DonHangs { get; set; } = new List<DonHang>();
}
