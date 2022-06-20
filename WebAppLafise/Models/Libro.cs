using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppLafise.Models
{
    [Table("Libro")]
    public partial class Libro
    {
        

        public int Id { get; set; }
        [StringLength(50)]
        public string? Titulo { get; set; }
        [Column("Id_Autor")]
        public int? IdAutor { get; set; }
    }
}
