using Microsoft.AspNetCore.Mvc.Rendering;
using TravelPlaces11.Services.DTOs.Places;

namespace TravelPlaces11.ViewModels.Places
{
    public class PlacesIndexViewModel
    {
        public List<PlaceListDto> Places { get; set; } = new List<PlaceListDto>();

        // Текст за търсене по име
        public string? SearchName { get; set; }

        // Избран вид за филтриране
        public int? PlaceTypeId { get; set; }

        // Опции за падащото меню с видове
        public List<SelectListItem> PlaceTypes { get; set; } = new List<SelectListItem>();

        public bool HasFilters => !string.IsNullOrWhiteSpace(SearchName) || PlaceTypeId.HasValue;
    }
}
