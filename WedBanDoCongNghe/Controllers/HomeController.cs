using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WedBanDoCongNghe.Models;

namespace WedBanDoCongNghe.Controllers
{
    public class HomeController : Controller
    {
        private readonly WedBanDoCongNgheContext _context;
        public HomeController(WedBanDoCongNgheContext context)
        {
            _context = context;
        }

        // Trang chủ: hiển thị sản phẩm + hỗ trợ tìm kiếm và lọc theo loại
        public async Task<IActionResult> Index(string? search, string? category)
        {
            var query = _context.SanPhams
                .Where(p => p.Active) // chỉ lấy sản phẩm đang bán
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.TenSanPham.Contains(search));

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.LoaiSanPham == category);

            var products = await query.OrderBy(p => p.TenSanPham).ToListAsync();

            ViewBag.CurrentCategory = category;
            ViewBag.Search = search;
            ViewBag.Categories = await _context.SanPhams
                                              .Select(p => p.LoaiSanPham)
                                              .Distinct()
                                              .ToListAsync();

            return View(products);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
