using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace My_Library.Entities
{
    public class Library
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} don't be empty!"), StringLength(40), Display(Name = "Book Name")]
        public string Name { get; set; }


        [Required(ErrorMessage = "{0} don't be empty!"), StringLength(30), Display(Name = "Book Author")]
        public string Author { get; set; }


        [Display(Name="Type")]
        public int BookTypeId { get; set; }

        [Display(Name = "Book Type")]
        public BookType? BookType { get; set; }


        [Required(ErrorMessage = "{0} don't be empty!"), StringLength(30), Display(Name = "Where")]
        public string Where { get; set; }


        [Display(Name="Status")]
        public int StatusId { get; set; }

        [Display(Name = "Status")]
        public Status? Status { get; set; }


        [Required(ErrorMessage = "{0} don't be empty!"), StringLength(30), Display(Name = "Book Image")]
        public string Image { get; set; }

        [Required(ErrorMessage = "{0} don't be empty!"), StringLength(600), Display(Name = "About Book")]
        public string? Notes { get; set; }

        [Display(Name = "Record Date"), ScaffoldColumn(false)]
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        
    }
}
