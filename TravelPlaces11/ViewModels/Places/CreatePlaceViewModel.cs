using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TravelPlaces11.ViewModels.Places
{
    public class CreatePlaceViewModel
    {
        [Display(Name = "Име")]
        [Required(ErrorMessage = "Името е задължително.")]
        [StringLength(100, ErrorMessage = "Името може да е най-много {1} символа.")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Област")]
        [Required(ErrorMessage = "Изберете област.")]
        public int? AreaId { get; set; }

        [Display(Name = "Вид")]
        [Required(ErrorMessage = "Изберете вид.")]
        public int? PlaceTypeId { get; set; }

        [Display(Name = "Входна такса (лв.)")]
        [Required(ErrorMessage = "Въведете входна такса (0, ако е безплатно).")]
        [Range(0, 10000, ErrorMessage = "Входната такса не може да е отрицателна.")]
        public decimal? Fee { get; set; }

        [Display(Name = "Описание")]
        [StringLength(1000, ErrorMessage = "Описанието може да е най-много {1} символа.")]
        public string? Description { get; set; }

        [ValidateNever]
        public List<SelectListItem> Areas { get; set; } = new List<SelectListItem>();

        [ValidateNever]
        public List<SelectListItem> PlaceTypes { get; set; } = new List<SelectListItem>();
    }
}
