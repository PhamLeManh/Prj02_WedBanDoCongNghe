using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WedBanDoCongNghe.Models;

namespace WedBanDoCongNghe.Controllers
{
    public class SanPhamsController : Controller
    {
        private readonly WedBanDoCongNgheContext _context;
        private readonly IWebHostEnvironment _environment;

        public SanPhamsController(WedBanDoCongNgheContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: SanPhams
        public async Task<IActionResult> Index()
        {
            return View(await _context.SanPhams.ToListAsync());
        }

        // GET: SanPhams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var sp = await _context.SanPhams.FirstOrDefaultAsync(m => m.SanPhamId == id);
            if (sp == null) return NotFound();

            return View(sp);
        }

        // GET: SanPhams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SanPhams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPham sp, IFormFile? HinhAnhFile)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnhFile != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(HinhAnhFile.FileName);
                    var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await HinhAnhFile.CopyToAsync(stream);
                    }

                    sp.HinhAnh = "/images/" + fileName;
                }

                _context.Add(sp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sp);
        }

        // GET: SanPhams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sp = await _context.SanPhams.FindAsync(id);
            if (sp == null) return NotFound();

            return View(sp);
        }

        // POST: SanPhams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SanPham sp, IFormFile? HinhAnhFile)
        {
            if (id != sp.SanPhamId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var spCu = await _context.SanPhams.AsNoTracking().FirstOrDefaultAsync(x => x.SanPhamId == id);
                    if (spCu == null) return NotFound();

                    if (HinhAnhFile != null)
                    {
                        // Xóa ảnh cũ
                        if (!string.IsNullOrEmpty(spCu.HinhAnh))
                        {
                            var oldPath = Path.Combine(_environment.WebRootPath, spCu.HinhAnh.TrimStart('/'));
                            if (System.IO.File.Exists(oldPath))
                                System.IO.File.Delete(oldPath);
                        }

                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(HinhAnhFile.FileName);
                        var filePath = Path.Combine(_environment.WebRootPath, "images", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await HinhAnhFile.CopyToAsync(stream);
                        }

                        sp.HinhAnh = "/images/" + fileName;
                    }
                    else
                    {
                        sp.HinhAnh = spCu.HinhAnh; // giữ nguyên ảnh cũ
                    }

                    _context.Update(sp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.SanPhams.Any(e => e.SanPhamId == sp.SanPhamId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sp);
        }

        // GET: SanPhams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var sp = await _context.SanPhams.FirstOrDefaultAsync(m => m.SanPhamId == id);
            if (sp == null) return NotFound();

            return View(sp);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sp = await _context.SanPhams.FindAsync(id);
            if (sp != null)
            {
                if (!string.IsNullOrEmpty(sp.HinhAnh))
                {
                    var path = Path.Combine(_environment.WebRootPath, sp.HinhAnh.TrimStart('/'));
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                _context.SanPhams.Remove(sp);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // Toggle Active
        [HttpGet]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var sp = await _context.SanPhams.FindAsync(id);
            if (sp == null) return NotFound();

            sp.Active = !sp.Active;
            _context.Update(sp);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
