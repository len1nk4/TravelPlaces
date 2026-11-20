using TravelPlaces11.Services.DTOs.Lookups;

namespace TravelPlaces11.Services.Contracts
{
    public interface ILookupService
    {
        Task<List<LookupDto>> GetAreasAsync();

        Task<List<LookupDto>> GetPlaceTypesAsync();
    }
}
