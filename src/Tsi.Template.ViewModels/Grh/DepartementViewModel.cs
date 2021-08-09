using System.ComponentModel.DataAnnotations;

namespace Tsi.Template.ViewModels.Grh
{
    public class DepartementViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="You must provide a code for this department!")]
        [StringLength(10, ErrorMessage ="The department code must be less than (10) characters")]
        [Display(Name = "ISO Code")]
        [RegularExpression("^[A-Za-z]*$", ErrorMessage ="Only alphabetique characters are allowed")]
        public string Code { get; set; }
        [Required(ErrorMessage ="The title of the department is required")]
        [StringLength(100)]
        [Display(Name = "Title")]
        public string Libelle { get; set; }
    }
}
