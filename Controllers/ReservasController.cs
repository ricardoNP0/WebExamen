using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebExamen.Data;
using WebExamen.Models;
using WebExamen.ViewModels;

namespace WebExamen.Controllers
{
    [Authorize]
    public class ReservasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Administrador"))
            {
                var reservasAdmin = await _context.Reservas
                    .Include(r => r.Hotel)
                    .Include(r => r.Usuario)
                    .OrderByDescending(r => r.Id)
                    .ToListAsync();

                return View(reservasAdmin);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var reservas = await _context.Reservas
                .Include(r => r.Hotel)
                .Include(r => r.Usuario)
                .Where(r => r.UsuarioId == user.Id)
                .OrderByDescending(r => r.Id)
                .ToListAsync();

            return View(reservas);
        }

        public async Task<IActionResult> Create()
        {
            var vm = new ReservaCreateViewModel
            {
                FechaInicio = DateTime.Today,
                FechaFin = DateTime.Today.AddDays(1),
                Hoteles = await _context.Hoteles
                    .Select(h => new SelectListItem
                    {
                        Value = h.Id.ToString(),
                        Text = $"{h.Nombre} - Bs. {h.PrecioPorNoche}"
                    }).ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReservaCreateViewModel vm)
        {
            vm.Hoteles = await _context.Hoteles
                .Select(h => new SelectListItem
                {
                    Value = h.Id.ToString(),
                    Text = $"{h.Nombre} - Bs. {h.PrecioPorNoche}"
                }).ToListAsync();

            if (vm.FechaInicio.Date < DateTime.Today)
                ModelState.AddModelError(nameof(vm.FechaInicio), "No se permiten reservas en fechas pasadas.");

            if (vm.FechaFin.Date <= vm.FechaInicio.Date)
                ModelState.AddModelError(nameof(vm.FechaFin), "La fecha de fin debe ser mayor a la fecha de inicio.");

            var hotelExiste = await _context.Hoteles.AnyAsync(h => h.Id == vm.HotelId);
            if (!hotelExiste)
                ModelState.AddModelError(nameof(vm.HotelId), "El hotel seleccionado no existe.");

            if (!ModelState.IsValid)
                return View(vm);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var reserva = new Reserva
            {
                FechaInicio = vm.FechaInicio,
                FechaFin = vm.FechaFin,
                HotelId = vm.HotelId,
                UsuarioId = user.Id
            };

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            TempData["ok"] = "Reserva registrada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var reserva = await _context.Reservas
                .Include(r => r.Hotel)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reserva == null) return NotFound();

            if (!User.IsInRole("Administrador"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null || reserva.UsuarioId != user.Id)
                    return Forbid();
            }

            return View(reserva);
        }
    }
}