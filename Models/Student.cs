using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace lista7_zad1.Models
{
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        public string? AlbumNr { get; set; }

        [ForeignKey("People")]
        public int PeopleID { get; set; }
        public People? People { get; set; }

        [ForeignKey("Major")]
        public int MajorID { get; set; }
        public Major? Major { get; set; }

        public ICollection<Grade>? Grades { get; set; }


    }
}
