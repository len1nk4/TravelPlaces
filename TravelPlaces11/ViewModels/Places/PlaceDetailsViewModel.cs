using TravelPlaces11.Services.DTOs.Places;

namespace TravelPlaces11.ViewModels.Places
{
    public class PlaceDetailsViewModel
    {
        public PlaceDetailsDto Place { get; set; } = null!;

        // Формата за нов отзив
        public CreateReviewViewModel NewReview { get; set; } = new CreateReviewViewModel();
    }
}
