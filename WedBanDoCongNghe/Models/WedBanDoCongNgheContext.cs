using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WedBanDoCongNghe.Models;

public class WedBanDoCongNgheContext : IdentityDbContext<AppUser>
{
    public WedBanDoCongNgheContext(DbContextOptions<WedBanDoCongNgheContext> options) : base(options) { }

    public DbSet<SanPham> SanPhams { get; set; }
    public DbSet<DonHang> DonHangs { get; set; }
    public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; }
}
