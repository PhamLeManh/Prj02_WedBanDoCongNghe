using System.ComponentModel.DataAnnotations;

namespace WedBanDoCongNghe.Models
{
    public class SanPham
    {
        [Key]
        public int SanPhamId { get; set; }

        [Required, StringLength(200)]
        public string TenSanPham { get; set; }

        public string? HinhAnh { get; set; }

        [Required]
        public decimal Gia { get; set; }

        public bool Active { get; set; } = true;

        public int SoLuongTon { get; set; } = 0;

        [StringLength(100)]
        public string? LoaiSanPham { get; set; }  

        public string? MoTa { get; set; } 
    }
}
