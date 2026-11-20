using TravelPlaces11.Services.DTOs.Places;

namespace TravelPlaces11.Services.Contracts
{
    public interface IPlaceService
    {
        // searchName – търсене по име; placeTypeId – филтър по вид
        Task<List<PlaceListDto>> GetAllAsync(string? searchName, int? placeTypeId);

        Task<PlaceDetailsDto?> GetDetailsAsync(int id);

        Task CreateAsync(CreatePlaceDto dto);

        // Връща false, ако обектът не съществува
        Task<bool> AddReviewAsync(CreateReviewDto dto);
    }
}
