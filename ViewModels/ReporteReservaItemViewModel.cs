namespace WebExamen.ViewModels
{
    public class ReporteReservaItemViewModel
    {
        public int Id { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string Hotel { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}