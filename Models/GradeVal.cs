using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lista7_zad1.Models
{
    public class GradeVal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeValID { get; set; }
        public string? Name { get; set; }
        public double? Value { get; set; }
        public ICollection<Grade>? Grades { get; set; }

    }
}
