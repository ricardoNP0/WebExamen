using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebExamen.ViewModels
{
    public class ReservaCreateViewModel
    {
        [Required]
        [Display(Name = "Fecha de inicio")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Display(Name = "Fecha de fin")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        [Required]
        [Display(Name = "Hotel")]
        public int HotelId { get; set; }

        public List<SelectListItem> Hoteles { get; set; } = new();
    }
}