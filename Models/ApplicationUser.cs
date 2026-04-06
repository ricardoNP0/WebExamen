using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebExamen.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(120)]
        [Display(Name = "Nombre completo")]
        public string NombreCompleto { get; set; } = string.Empty;

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}