using System.ComponentModel.DataAnnotations;

namespace WebExamen.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "La fecha de fin es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }

        // Relaciones (Foreign Keys)
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
    }
}