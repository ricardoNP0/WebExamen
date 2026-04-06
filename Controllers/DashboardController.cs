using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebExamen.ViewModels;

[Authorize(Roles = "Administrador")]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;
    public DashboardController(ApplicationDbContext context) { _context = context; }

    public IActionResult Index()
    {
        var data = new DashboardViewModel
        {
            TotalHoteles = _context.Hoteles.Count(),
            TotalUsuarios = _context.Users.Count(),
            TotalReservas = _context.Reservas.Count(),
            ReservasPorMes = new List<int> { 10, 20, 15, 30 } // Datos ejemplo para el gráfico
        };
        return View(data);
    }
}