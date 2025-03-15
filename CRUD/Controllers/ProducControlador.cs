using CRUD.Db;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    public class ProducControlador : Controller
    {
        private readonly AppDbContext _context;

        public ProducControlador(AppDbContext context)
        {
            _context = context;
        }

        // GET: Productos (Index)
        public IActionResult Index()
        {
            var productos = _context.Productos.(p => p.Activo).ToList();
            return View(productos);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                producto.Activo = true;
                _context.Add(producto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var producto = _context.Productos.Find(id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Producto producto)
        {
            if (id != producto.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Productos.Any(e => e.Id == producto.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productos/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var producto = _context.Productos.Find(id);
            if (producto == null) return NotFound();
            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto != null)
            {
                producto.Activo = false; // Desactivar en lugar de eliminar
                _context.Update(producto);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
