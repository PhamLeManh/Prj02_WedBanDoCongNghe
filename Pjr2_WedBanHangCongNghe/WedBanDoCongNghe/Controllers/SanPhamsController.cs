using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WedBanDoCongNghe.Models;

namespace WedBanDoCongNghe.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly WedBanDoCongNgheContext _context;

        public SanPhamsController(WedBanDoCongNgheContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.SanPhams.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var sanPham = await _context.SanPhams.FirstOrDefaultAsync(m => m.SanPhamId == id);
            if (sanPham == null) return NotFound();
            return View(sanPham);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SanPhamId,TenSp,Gia,MoTa,SoLuong,HinhAnh")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sanPham);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null) return NotFound();
            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SanPhamId,TenSp,Gia,MoTa,SoLuong,HinhAnh")] SanPham sanPham)
        {
            if (id != sanPham.SanPhamId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.SanPhams.Any(e => e.SanPhamId == sanPham.SanPhamId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sanPham);
        }

 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var sanPham = await _context.SanPhams.FirstOrDefaultAsync(m => m.SanPhamId == id);
            if (sanPham == null) return NotFound();
            return View(sanPham);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
