using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Reflection.Metadata;
using WebExamen.Models;

namespace WebExamen.Services
{
    public class PdfService
    {
        public byte[] GenerarReporteReservas(List<Reserva> reservas)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Header().Text("Reporte de Reservas").FontSize(25).SemiBold();
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(col => { col.RelativeColumn(); col.RelativeColumn(); });
                        table.Header(h => { h.Cell().Text("Hotel"); h.Cell().Text("Fecha"); });
                        foreach (var r in reservas)
                        {
                            table.Cell().Text(r.HotelId.ToString()); // Ricardo debe proveer el objeto Hotel
                            table.Cell().Text(r.FechaInicio.ToShortDateString());
                        }
                    });
                });
            }).GeneratePdf();
        }
    }
}