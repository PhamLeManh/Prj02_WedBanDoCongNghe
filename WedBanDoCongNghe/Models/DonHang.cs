using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WedBanDoCongNghe.Models
{
    public class DonHang
    {
        [Key]
        public int DonHangId { get; set; }

        [Required]
        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser KhachHang { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public string TrangThai { get; set; } = "Pending";

        public ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; }
    }
}
