using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace libAPI.Models
{
    public class BookLoan
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanID { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; }
        public int MemberID { get; set; }
        public Member Member { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime DueDate { get; set; }
        //public int LoanID { get; internal set; }
    }
}
