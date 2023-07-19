using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lista7_zad1.Models
{
    public class Grade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeID { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("Student")]
        public int? StudentID { get; set; }
        public Student? Student { get; set; }

        [ForeignKey("Professor")]
        public int? ProfessorID { get; set; }
        public Professor? Professor { get; set; }

        [ForeignKey("GradeVal")]
        public int? GradeValID { get; set; }
        public GradeVal? GradeVal { get; set; }

        [ForeignKey("Subject")]
        public int? SubjectID { get; set; }
        public Subject? Subject { get; set; }

        [ForeignKey("Classification")]
        public int? ClassificationID { get; set; }
        public Classification? Classification { get; set; }

    }
}
