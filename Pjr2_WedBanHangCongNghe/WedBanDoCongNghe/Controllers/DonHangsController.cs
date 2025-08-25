using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WedBanDoCongNghe.Models;

namespace WedBanDoCongNghe.Controllers
{
    public class DonHangsController : Controller
    {
        private readonly WedBanDoCongNgheContext _context;

        public DonHangsController(WedBanDoCongNgheContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var wedContext = _context.DonHangs.Include(d => d.User);
            return View(await wedContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var donHang = await _context.DonHangs.Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DonHangId == id);
            if (donHang == null) return NotFound();
            return View(donHang);
        }

        
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "TenDangNhap");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DonHangId,UserId,NgayDat,TongTien,TrangThai")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(donHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "TenDangNhap", donHang.UserId);
            return View(donHang);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var donHang = await _context.DonHangs.FindAsync(id);
            if (donHang == null) return NotFound();
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "TenDangNhap", donHang.UserId);
            return View(donHang);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DonHangId,UserId,NgayDat,TongTien,TrangThai")] DonHang donHang)
        {
            if (id != donHang.DonHangId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(donHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.DonHangs.Any(e => e.DonHangId == donHang.DonHangId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "TenDangNhap", donHang.UserId);
            return View(donHang);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var donHang = await _context.DonHangs.Include(d => d.User)
                .FirstOrDefaultAsync(m => m.DonHangId == id);
            if (donHang == null) return NotFound();
            return View(donHang);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donHang = await _context.DonHangs.FindAsync(id);
            if (donHang != null)
            {
                _context.DonHangs.Remove(donHang);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
