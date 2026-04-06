using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebExamen.Data;
using WebExamen.Services;
using WebExamen.ViewModels;

namespace WebExamen.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ReportesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PdfService _pdfService;

        public ReportesController(ApplicationDbContext context, PdfService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }

        public async Task<IActionResult> Index()
        {
            var reservas = await _context.Reservas
                .Include(r => r.Hotel)
                .Include(r => r.Usuario)
                .OrderByDescending(r => r.Id)
                .Select(r => new ReporteReservaItemViewModel
                {
                    Id = r.Id,
                    Cliente = r.Usuario != null ? (r.Usuario.NombreCompleto ?? r.Usuario.Email ?? "Sin nombre") : "Sin cliente",
                    Hotel = r.Hotel != null ? r.Hotel.Nombre : "Sin hotel",
                    FechaInicio = r.FechaInicio,
                    FechaFin = r.FechaFin
                })
                .ToListAsync();

            return View(reservas);
        }

        public async Task<IActionResult> ReservasPdf()
        {
            var reservas = await _context.Reservas
                .Include(r => r.Hotel)
                .Include(r => r.Usuario)
                .OrderByDescending(r => r.Id)
                .Select(r => new ReporteReservaItemViewModel
                {
                    Id = r.Id,
                    Cliente = r.Usuario != null ? (r.Usuario.NombreCompleto ?? r.Usuario.Email ?? "Sin nombre") : "Sin cliente",
                    Hotel = r.Hotel != null ? r.Hotel.Nombre : "Sin hotel",
                    FechaInicio = r.FechaInicio,
                    FechaFin = r.FechaFin
                })
                .ToListAsync();

            var pdfBytes = _pdfService.GenerarReporteReservas(reservas);

            return File(pdfBytes, "application/pdf", "ReporteReservas.pdf");
        }
    }
}