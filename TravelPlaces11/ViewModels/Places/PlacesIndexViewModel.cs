using Microsoft.AspNetCore.Mvc.Rendering;
using TravelPlaces11.Services.DTOs.Places;

namespace TravelPlaces11.ViewModels.Places
{
    public class PlacesIndexViewModel
    {
        public List<PlaceListDto> Places { get; set; } = new List<PlaceListDto>();

        public string? SearchName { get; set; }

        public int? PlaceTypeId { get; set; }

        public List<SelectListItem> PlaceTypes { get; set; } = new List<SelectListItem>();

        public bool HasFilters => !string.IsNullOrWhiteSpace(SearchName) || PlaceTypeId.HasValue;
    }
}
