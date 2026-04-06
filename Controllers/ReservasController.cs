using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebExamen.Models;
using System.Security.Claims;

namespace WebExamen.Controllers
{
    [Authorize]
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReservasController(ApplicationDbContext context) { _context = context; }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var misReservas = _context.Reservas.Where(r => r.UsuarioId == userId).ToList();
            return View(misReservas);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            // Validar: fecha fin > fecha inicio
            if (reserva.FechaFin <= reserva.FechaInicio)
                ModelState.AddModelError("FechaFin", "La fecha de fin debe ser posterior a la de inicio.");

            // Validar: no permitir fechas pasadas
            if (reserva.FechaInicio < DateTime.Today)
                ModelState.AddModelError("FechaInicio", "No puedes reservar en fechas que ya pasaron.");

            if (ModelState.IsValid)
            {
                reserva.UsuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reserva);
        }
    }
}