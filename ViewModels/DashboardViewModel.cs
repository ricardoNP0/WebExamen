namespace WebExamen.ViewModels
{
    public class DashboardViewModel
    {
        public int TotalHoteles { get; set; }
        public int TotalUsuarios { get; set; }
        public int TotalReservas { get; set; }

        public List<string> Meses { get; set; } = new();
        public List<int> ReservasPorMes { get; set; } = new();

        public List<string> HotelesLabels { get; set; } = new();
        public List<int> HotelesCantidades { get; set; } = new();
    }
}