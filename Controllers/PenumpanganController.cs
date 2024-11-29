using Microsoft.AspNetCore.Mvc;
using crudapp.Models;  // Ganti dengan namespace model Anda
using crudapp.Data;    // Ganti dengan namespace DbContext Anda
using Microsoft.EntityFrameworkCore; // Untuk DbContext

namespace crudapp.Controllers
{
    public class PenumpanganController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PenumpanganController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Penumpangan/Index
        public IActionResult Index()
        {
            var penumpangs = _context.Penumpangs.ToList();
            return View(penumpangs);
        }

        // GET: Penumpangan/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdUnit, Nama, NIK, Tujuan, JamBerangkat, TempatDuduk")] Penumpang penumpang)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(penumpang);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Data penumpang berhasil ditambahkan.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Terjadi kesalahan saat menambahkan data penumpang: " + ex.Message;
                    return View(penumpang);
                }
            }

            // Jika model tidak valid, tampilkan kembali form dengan pesan error
            TempData["ErrorMessage"] = "Data yang dimasukkan tidak valid.";
            return View(penumpang);
        }

        public IActionResult Edit(int id)
        {
            var penumpang = _context.Penumpangs.Find(id);
            if (penumpang == null)
            {
                return NotFound(); // Jika penumpang tidak ditemukan
            }
            return View(penumpang); // Menampilkan form untuk edit data penumpang
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id, Nama, NIK, Tujuan, JamBerangkat, TempatDuduk")] Penumpang penumpang)
        {
            if (id != penumpang.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penumpang);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Penumpangs.Any(p => p.Id == penumpang.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index)); // Kembali setelah edit berhasil
            }
            return View(penumpang); // Jika ada error, tampilkan kembali form dengan data yang sudah diisi
        }

        // GET: Penumpangan/Delete/5
        public IActionResult Delete(int id)
        {
            var penumpang = _context.Penumpangs.Find(id);
            if (penumpang != null)
            {
                _context.Penumpangs.Remove(penumpang);
                _context.SaveChanges();
            }
            return RedirectToAction("Index", "Penumpangan");
        }

        // POST: Penumpangan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var penumpang = _context.Penumpangs.Find(id);
            if (penumpang != null)
            {
                _context.Penumpangs.Remove(penumpang); // Menghapus data penumpang dari database
                _context.SaveChanges(); // Menyimpan perubahan
            }
            return RedirectToAction(nameof(Index)); // Kembali setelah penghapusan data
        }
    }
}
