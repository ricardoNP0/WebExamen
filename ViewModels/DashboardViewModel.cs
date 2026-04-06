namespace WebExamen.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalHoteles { get; set; }
        public int TotalUsuarios { get; set; }
        public int TotalReservas { get; set; }
        public List<int> ReservasPorMes { get; set; }
    }
}