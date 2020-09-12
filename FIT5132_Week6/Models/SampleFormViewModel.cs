using System.ComponentModel.DataAnnotations;
namespace FIT5032_Week6.Models
{
    public class SampleFormViewModels
    {
    }
    public class FormOneViewModel
    {
        [Required(ErrorMessage ="Please enter first name")]
        [StringLength(20, MinimumLength = 1)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}