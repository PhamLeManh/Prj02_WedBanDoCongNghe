using System.ComponentModel.DataAnnotations;

namespace WedBanDoCongNghe.Models
{
    public class ChiTietDonHang
    {
        [Key]
        public int ChiTietDonHangId { get; set; }

        public int DonHangId { get; set; }
        public DonHang DonHang { get; set; }

        public int SanPhamId { get; set; }
        public SanPham SanPham { get; set; }

        [Required]
        public int SoLuong { get; set; } = 1;
    }
}
