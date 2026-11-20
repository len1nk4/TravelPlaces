using System.ComponentModel.DataAnnotations;

namespace TravelPlaces11.ViewModels.Places
{
    public class CreateReviewViewModel
    {
        public int TravelPlaceId { get; set; }

        [Display(Name = "Вашето име")]
        [Required(ErrorMessage = "Името е задължително.")]
        [StringLength(100, ErrorMessage = "Името може да е най-много {1} символа.")]
        public string VisitorName { get; set; } = string.Empty;

        [Display(Name = "Имейл")]
        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл.")]
        [StringLength(100, ErrorMessage = "Имейлът може да е най-много {1} символа.")]
        public string VisitorEmail { get; set; } = string.Empty;

        [Display(Name = "Оценка")]
        [Range(1, 5, ErrorMessage = "Оценката е от 1 до 5.")]
        public int Rating { get; set; }

        [Display(Name = "Коментар")]
        [StringLength(500, ErrorMessage = "Коментарът може да е най-много {1} символа.")]
        public string? Comment { get; set; }

        [Display(Name = "Дата на посещение")]
        [Required(ErrorMessage = "Датата е задължителна.")]
        [DataType(DataType.Date)]
        public DateTime? VisitDate { get; set; }
    }
}
