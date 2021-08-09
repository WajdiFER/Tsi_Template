using System.ComponentModel.DataAnnotations;

namespace Tsi.Template.ViewModels.Grh
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "National ID Card")]
        public long Cin { get; set; }

        [Display(Name = "Associated Department ")]
        public int DepartementId { get; set; }

        public string DepartementName { get; set; }
    }
}