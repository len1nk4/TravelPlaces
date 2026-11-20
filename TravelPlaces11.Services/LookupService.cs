using Microsoft.EntityFrameworkCore;
using TravelPLaces11.Data;
using TravelPlaces11.Services.Contracts;
using TravelPlaces11.Services.DTOs.Lookups;

namespace TravelPlaces11.Services
{
    public class LookupService : ILookupService
    {
        private readonly TravelPlaces11DbContext _context;

        public LookupService(TravelPlaces11DbContext context)
        {
            _context = context;
        }

        public Task<List<LookupDto>> GetAreasAsync()
        {
            return _context.Areas
                .OrderBy(a => a.Name)
                .Select(a => new LookupDto { Id = a.Id, Name = a.Name })
                .ToListAsync();
        }

        public Task<List<LookupDto>> GetPlaceTypesAsync()
        {
            return _context.PlaceTypes
                .OrderBy(t => t.Name)
                .Select(t => new LookupDto { Id = t.Id, Name = t.Name })
                .ToListAsync();
        }
    }
}
