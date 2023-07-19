using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lista7_zad1.Models
{
    public class Professor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProfessorID { get; set; }
        public string? AlbumNr { get; set; }
        public string? Password { get; set; }

        [ForeignKey("People")]
        public int PeopleID { get; set; }
        public People? People { get; set; }

        public ICollection<Grade>? Grades { get; set; }


    }
}
