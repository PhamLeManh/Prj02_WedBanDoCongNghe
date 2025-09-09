using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WedBanDoCongNghe.Models;

namespace WedBanDoCongNghe.Controllers
{
    [Authorize(Roles = "Customer")]
    public class DonHangsController : Controller
    {
        private readonly WedBanDoCongNgheContext _context;
        private readonly UserManager<AppUser> _userManager;

        public DonHangsController(WedBanDoCongNgheContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var donHang = await _context.DonHangs
                .Include(d => d.ChiTietDonHangs)
                .FirstOrDefaultAsync(d => d.AppUserId == user.Id && d.TrangThai == "Pending");

            if (donHang == null)
            {
                donHang = new DonHang
                {
                    AppUserId = user.Id,
                    NgayTao = DateTime.Now,
                    TrangThai = "Pending"
                };
                _context.DonHangs.Add(donHang);
                await _context.SaveChangesAsync();
            }

            var chiTiet = await _context.ChiTietDonHangs
                .FirstOrDefaultAsync(c => c.DonHangId == donHang.DonHangId && c.SanPhamId == id);

            if (chiTiet != null)
            {
                chiTiet.SoLuong++;
                _context.Update(chiTiet);
            }
            else
            {
                _context.ChiTietDonHangs.Add(new ChiTietDonHang
                {
                    DonHangId = donHang.DonHangId,
                    SanPhamId = id,
                    SoLuong = 1
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Cart");
        }

        public async Task<IActionResult> Cart()
        {
            var user = await _userManager.GetUserAsync(User);

            var donHang = await _context.DonHangs
                .Include(d => d.ChiTietDonHangs)
                .ThenInclude(c => c.SanPham)
                .FirstOrDefaultAsync(d => d.AppUserId == user.Id && d.TrangThai == "Pending");

            return View(donHang);
        }

        public async Task<IActionResult> RemoveItem(int id)
        {
            var chiTiet = await _context.ChiTietDonHangs.FindAsync(id);
            if (chiTiet != null)
            {
                _context.ChiTietDonHangs.Remove(chiTiet);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> Checkout()
        {
            var user = await _userManager.GetUserAsync(User);

            var donHang = await _context.DonHangs
                .Include(d => d.ChiTietDonHangs)
                .ThenInclude(c => c.SanPham)
                .FirstOrDefaultAsync(d => d.AppUserId == user.Id && d.TrangThai == "Pending");

            if (donHang == null || !donHang.ChiTietDonHangs.Any())
                return RedirectToAction("Cart");

            foreach (var chiTiet in donHang.ChiTietDonHangs)
            {
                if (chiTiet.SanPham.SoLuongTon >= chiTiet.SoLuong)
                {
                    chiTiet.SanPham.SoLuongTon -= chiTiet.SoLuong;
                }
                else
                {
                    ModelState.AddModelError("", $"Sản phẩm {chiTiet.SanPham.TenSanPham} không đủ tồn kho");
                    return View("Cart", donHang);
                }
            }

            donHang.TrangThai = "Paid";
            donHang.NgayTao = DateTime.Now;
            await _context.SaveChangesAsync();

            ViewBag.Total = donHang.ChiTietDonHangs.Sum(c => c.SanPham.Gia * c.SoLuong);

            return View(donHang);
        }

        public async Task<IActionResult> OrderHistory()
        {
            var user = await _userManager.GetUserAsync(User);

            var orders = await _context.DonHangs
                .Include(d => d.ChiTietDonHangs)
                .ThenInclude(c => c.SanPham)
                .Where(d => d.AppUserId == user.Id && d.TrangThai == "Paid")
                .OrderByDescending(d => d.NgayTao)
                .ToListAsync();

            return View(orders);
        }
    }
}
