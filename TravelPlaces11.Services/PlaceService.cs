using Microsoft.EntityFrameworkCore;
using TravelPLaces11.Data;
using TravelPLaces11.Data.Entities;
using TravelPlaces11.Services.Contracts;
using TravelPlaces11.Services.DTOs.Places;

namespace TravelPlaces11.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly TravelPlaces11DbContext _context;

        public PlaceService(TravelPlaces11DbContext context)
        {
            _context = context;
        }

        public Task<List<PlaceListDto>> GetAllAsync(string? searchName, int? placeTypeId)
        {
            var query = _context.TravelPlaces.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchName))
            {
                var term = searchName.Trim();
                query = query.Where(p => p.Name.Contains(term));
            }

            if (placeTypeId.HasValue)
            {
                query = query.Where(p => p.PlaceTypeId == placeTypeId.Value);
            }

            return query
                .OrderBy(p => p.Name)
                .Select(p => new PlaceListDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    AreaName = p.Area.Name,
                    PlaceTypeName = p.PlaceType != null ? p.PlaceType.Name : null,
                    Fee = p.Fee,
                    Description = p.Description,
                    AverageRating = p.Reviews.Average(r => (double?)r.Rating),
                    ReviewsCount = p.Reviews.Count
                })
                .ToListAsync();
        }

        public Task<PlaceDetailsDto?> GetDetailsAsync(int id)
        {
            return _context.TravelPlaces
                .Where(p => p.Id == id)
                .Select(p => new PlaceDetailsDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    AreaName = p.Area.Name,
                    PlaceTypeName = p.PlaceType != null ? p.PlaceType.Name : null,
                    Fee = p.Fee,
                    Description = p.Description,
                    AverageRating = p.Reviews.Average(r => (double?)r.Rating),
                    Reviews = p.Reviews
                        .OrderByDescending(r => r.VisitDate)
                        .Select(r => new ReviewDto
                        {
                            VisitorName = r.Visitor.FullName,
                            Rating = r.Rating,
                            Comment = r.Comment,
                            VisitDate = r.VisitDate
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(CreatePlaceDto dto)
        {
            var place = new TravelPlace
            {
                Name = dto.Name.Trim(),
                AreaId = dto.AreaId,
                PlaceTypeId = dto.PlaceTypeId,
                Fee = dto.Fee,
                Description = string.IsNullOrWhiteSpace(dto.Description) ? null : dto.Description.Trim()
            };

            _context.TravelPlaces.Add(place);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AddReviewAsync(CreateReviewDto dto)
        {
            var placeExists = await _context.TravelPlaces.AnyAsync(p => p.Id == dto.TravelPlaceId);

            if (!placeExists)
            {
                return false;
            }

            var email = dto.VisitorEmail.Trim();

            var visitor = await _context.Visitors.FirstOrDefaultAsync(v => v.Email == email);

            if (visitor == null)
            {
                visitor = new Visitor
                {
                    FullName = dto.VisitorName.Trim(),
                    Email = email
                };

                _context.Visitors.Add(visitor);
            }

            _context.Reviews.Add(new Review
            {
                TravelPlaceId = dto.TravelPlaceId,
                Visitor = visitor,
                Rating = dto.Rating,
                Comment = string.IsNullOrWhiteSpace(dto.Comment) ? null : dto.Comment.Trim(),
                VisitDate = dto.VisitDate.Date
            });

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
