using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lista7_zad1.Models
{
    public class Major
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MajorID { get; set; }
        public string? Name { get; set;}
        public ICollection<Student>? Students { get; set; }

    }
}
