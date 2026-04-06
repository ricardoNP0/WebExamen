using System.ComponentModel.DataAnnotations;

namespace WebExamen.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200)]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El precio por noche es obligatorio")]
        [Range(1, 100000, ErrorMessage = "El precio debe ser mayor a 0")]
        [Display(Name = "Precio por noche")]
        public decimal PrecioPorNoche { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}