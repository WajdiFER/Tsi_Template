using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Tsi.Template.ViewModels.Grh
{
    public class EmployeeViewModel
    {
        public int Id { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public long Cin { get; set; }  
        public string DepartementName { get; set; } 
    }
}