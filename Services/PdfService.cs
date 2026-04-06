using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using WebExamen.ViewModels;

namespace WebExamen.Services
{
    public class PdfService
    {
        public byte[] GenerarReporteReservas(List<ReporteReservaItemViewModel> reservas)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header()
                        .Text("Reporte de Reservas")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Blue.Darken2);

                    page.Content().PaddingVertical(10).Column(column =>
                    {
                        column.Item().Text($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}");

                        column.Item().PaddingTop(15).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(40);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1.5f);
                                columns.RelativeColumn(1.5f);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("ID").Bold();
                                header.Cell().Element(CellStyle).Text("Cliente").Bold();
                                header.Cell().Element(CellStyle).Text("Hotel").Bold();
                                header.Cell().Element(CellStyle).Text("Inicio").Bold();
                                header.Cell().Element(CellStyle).Text("Fin").Bold();
                            });

                            foreach (var item in reservas)
                            {
                                table.Cell().Element(CellStyle).Text(item.Id.ToString());
                                table.Cell().Element(CellStyle).Text(item.Cliente);
                                table.Cell().Element(CellStyle).Text(item.Hotel);
                                table.Cell().Element(CellStyle).Text(item.FechaInicio.ToString("dd/MM/yyyy"));
                                table.Cell().Element(CellStyle).Text(item.FechaFin.ToString("dd/MM/yyyy"));
                            }
                        });
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Sistema Web de Reservas de Hoteles - Página ");
                            x.CurrentPageNumber();
                        });
                });
            }).GeneratePdf();
        }

        private static IContainer CellStyle(IContainer container)
        {
            return container
                .Border(1)
                .BorderColor(Colors.Grey.Lighten2)
                .Padding(5);
        }
    }
}