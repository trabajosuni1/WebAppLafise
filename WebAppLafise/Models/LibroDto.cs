using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppLafise.Models
{
    public class LibroDto
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string? Titulo { get; set; }
        
        public string Autor { get; set; }
    }
}
