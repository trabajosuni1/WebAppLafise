using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppLafise.Models
{
    [Table("Autor")]
    public partial class Autor
    {
       
        public int Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
    }
}
