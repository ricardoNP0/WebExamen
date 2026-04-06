using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebExamen.Data;
using WebExamen.Models;
using WebExamen.ViewModels;

namespace WebExamen.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new DashboardViewModel
            {
                TotalHoteles = await _context.Hoteles.CountAsync(),
                TotalUsuarios = await _userManager.Users.CountAsync(),
                TotalReservas = await _context.Reservas.CountAsync()
            };

            var reservasPorMes = await _context.Reservas
                .GroupBy(r => new { r.FechaInicio.Year, r.FechaInicio.Month })
                .Select(g => new
                {
                    g.Key.Year,
                    g.Key.Month,
                    Cantidad = g.Count()
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            vm.Meses = reservasPorMes
                .Select(x => $"{x.Month:00}/{x.Year}")
                .ToList();

            vm.ReservasPorMes = reservasPorMes
                .Select(x => x.Cantidad)
                .ToList();

            var hotelesMasReservados = await _context.Reservas
                .Include(r => r.Hotel)
                .GroupBy(r => r.Hotel!.Nombre)
                .Select(g => new
                {
                    Hotel = g.Key,
                    Cantidad = g.Count()
                })
                .OrderByDescending(x => x.Cantidad)
                .Take(5)
                .ToListAsync();

            vm.HotelesLabels = hotelesMasReservados.Select(x => x.Hotel).ToList();
            vm.HotelesCantidades = hotelesMasReservados.Select(x => x.Cantidad).ToList();

            return View(vm);
        }
    }
}