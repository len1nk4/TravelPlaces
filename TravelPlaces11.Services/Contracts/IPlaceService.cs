using TravelPlaces11.Services.DTOs.Places;

namespace TravelPlaces11.Services.Contracts
{
    public interface IPlaceService
    {
        Task<List<PlaceListDto>> GetAllAsync(string? searchName, int? placeTypeId);

        Task<PlaceDetailsDto?> GetDetailsAsync(int id);

        Task CreateAsync(CreatePlaceDto dto);

        Task<bool> AddReviewAsync(CreateReviewDto dto);
    }
}
