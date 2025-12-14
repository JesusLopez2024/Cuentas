using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cuentas.Data;
using Cuentas.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Cuentas.Controllers
{
    public class MovimientoController : Controller
    {
        private readonly CuentasContext _context;

        public MovimientoController(CuentasContext context)
        {
            _context = context;
        }

        // GET: Movimiento
        public async Task<IActionResult> Index()
        {
            var movimientos = await _context.Movimiento
                .Include(m => m.Cuenta)
                .ToListAsync();

            return View(movimientos);
        }

        // GET: Movimiento/Create
        public IActionResult Create()
        {
            CargarCuentas();
            return View();
        }

        // POST: Movimiento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movimiento movimiento)
        {
            CargarCuentas(); // 🔑 CLAVE

            if (!ModelState.IsValid)
            {
                return View(movimiento);
            }

            var cuenta = await _context.Cuenta.FindAsync(movimiento.CuentaId);

            if (cuenta == null)
            {
                ModelState.AddModelError("", "La cuenta no existe.");
                return View(movimiento);
            }

            if (cuenta.balance < 0)
            {
                ModelState.AddModelError("", "La cuenta está inactiva.");
                return View(movimiento);
            }

            if (movimiento.Tipo == 1)
            {
                cuenta.setCredito(movimiento.Monto);
            }
            else if (movimiento.Tipo == 2)
            {
                // ❗ VALIDACIÓN EXTRA (recomendado)
                if (movimiento.Monto > cuenta.balance)
                {
                    ModelState.AddModelError("", "Saldo insuficiente.");
                    return View(movimiento);
                }

                cuenta.setDebito(movimiento.Monto);
            }

            cuenta.setBalance();
            movimiento.Fecha = DateTime.Now;

            _context.Movimiento.Add(movimiento);
            _context.Cuenta.Update(cuenta);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // MÉTODO AUXILIAR
        private void CargarCuentas()
        {
            ViewBag.Cuentas = _context.Cuenta
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.numero + " - " + c.descripcion
                })
                .ToList();
        }
    }
}

