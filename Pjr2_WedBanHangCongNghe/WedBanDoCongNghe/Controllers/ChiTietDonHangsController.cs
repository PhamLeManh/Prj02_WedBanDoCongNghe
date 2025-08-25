using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WedBanDoCongNghe.Models;

namespace WedBanDoCongNghe.Controllers
{
    public class ChiTietDonHangsController : Controller
    {
        private readonly WedBanDoCongNgheContext _context;

        public ChiTietDonHangsController(WedBanDoCongNgheContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var wedContext = _context.ChiTietDonHangs
                .Include(c => c.DonHang)
                .Include(c => c.SanPham);
            return View(await wedContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var ctdh = await _context.ChiTietDonHangs
                .Include(c => c.DonHang)
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.CtdhId == id);
            if (ctdh == null) return NotFound();
            return View(ctdh);
        }

       
        public IActionResult Create()
        {
            ViewData["DonHangId"] = new SelectList(_context.DonHangs, "DonHangId", "DonHangId");
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "SanPhamId", "TenSp");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CtdhId,DonHangId,SanPhamId,SoLuong,GiaBan")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietDonHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonHangId"] = new SelectList(_context.DonHangs, "DonHangId", "DonHangId", chiTietDonHang.DonHangId);
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "SanPhamId", "TenSp", chiTietDonHang.SanPhamId);
            return View(chiTietDonHang);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var ctdh = await _context.ChiTietDonHangs.FindAsync(id);
            if (ctdh == null) return NotFound();
            ViewData["DonHangId"] = new SelectList(_context.DonHangs, "DonHangId", "DonHangId", ctdh.DonHangId);
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "SanPhamId", "TenSp", ctdh.SanPhamId);
            return View(ctdh);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CtdhId,DonHangId,SanPhamId,SoLuong,GiaBan")] ChiTietDonHang chiTietDonHang)
        {
            if (id != chiTietDonHang.CtdhId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietDonHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ChiTietDonHangs.Any(e => e.CtdhId == chiTietDonHang.CtdhId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DonHangId"] = new SelectList(_context.DonHangs, "DonHangId", "DonHangId", chiTietDonHang.DonHangId);
            ViewData["SanPhamId"] = new SelectList(_context.SanPhams, "SanPhamId", "TenSp", chiTietDonHang.SanPhamId);
            return View(chiTietDonHang);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var ctdh = await _context.ChiTietDonHangs
                .Include(c => c.DonHang)
                .Include(c => c.SanPham)
                .FirstOrDefaultAsync(m => m.CtdhId == id);
            if (ctdh == null) return NotFound();
            return View(ctdh);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ctdh = await _context.ChiTietDonHangs.FindAsync(id);
            if (ctdh != null)
            {
                _context.ChiTietDonHangs.Remove(ctdh);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
