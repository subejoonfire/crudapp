using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crudapp.Data;
using crudapp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Linq;

namespace CrudApp.Controllers
{
    public class UnitController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnitController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var units = _context.Units.ToList();
            return View(units);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, NamaUnit, JenisUnit, Kapasitas, NoPol, Sarana")] Unit unit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(unit);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = _context.Units.Find(id);
            if (unit == null)
            {
                return NotFound();
            }
            return View(unit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaUnit, JenisUnit, Kapasitas, NoPol, Sarana")] Unit unit)
        {
            if (id != unit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Units.Any(e => e.Id == unit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.StatusOptions = new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Tersedia" },
                new SelectListItem { Value = "false", Text = "Tidak Tersedia" }
            };

            return View(unit);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Units
                .FirstOrDefaultAsync(u => u.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unit = await _context.Units
                .FirstOrDefaultAsync(u => u.Id == id);
            if (unit == null)
            {
                return NotFound();
            }

            return View(unit);
        }

        // Menghapus unit
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unit = await _context.Units.FindAsync(id);
            if (unit != null)
            {
                _context.Units.Remove(unit);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult PilihPenumpang(int id)
        {
            var unit = _context.Units.Find(id);
            if (unit == null)
            {
                return NotFound();
            }

            // Mendapatkan daftar tempat duduk yang sudah terisi
            var occupiedSeats = _context.Penumpangs
                .Where(p => p.IdUnit == id && !string.IsNullOrEmpty(p.TempatDuduk))
                .Select(p => int.Parse(p.TempatDuduk))
                .ToList();

            // Menyimpan data kapasitas dan kursi terisi ke ViewBag
            ViewBag.Kapasitas = unit.Kapasitas;
            ViewBag.OccupiedSeats = occupiedSeats;
            ViewBag.UnitId = unit.Id;

            return View();
        }


    }
}
