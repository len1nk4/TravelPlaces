using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TravelPlaces11.Services.Contracts;
using TravelPlaces11.Services.DTOs.Places;
using TravelPlaces11.Services.DTOs.Lookups;
using TravelPlaces11.ViewModels.Places;

namespace TravelPlaces11.Controllers
{
    public class PlaceController : Controller
    {
        private readonly IPlaceService _placeService;
        private readonly ILookupService _lookupService;

        public PlaceController(IPlaceService placeService, ILookupService lookupService)
        {
            _placeService = placeService;
            _lookupService = lookupService;
        }

        // GET: /Place?searchName=...&placeTypeId=...
        public async Task<IActionResult> Index(string? searchName, int? placeTypeId)
        {
            var model = new PlacesIndexViewModel
            {
                SearchName = searchName,
                PlaceTypeId = placeTypeId,
                Places = await _placeService.GetAllAsync(searchName, placeTypeId),
                PlaceTypes = ToSelectList(await _lookupService.GetPlaceTypesAsync())
            };

            return View(model);
        }

        // GET: /Place/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var place = await _placeService.GetDetailsAsync(id);

            if (place == null)
            {
                return NotFound();
            }

            var model = new PlaceDetailsViewModel
            {
                Place = place,
                NewReview = new CreateReviewViewModel
                {
                    TravelPlaceId = id,
                    Rating = 5,
                    VisitDate = DateTime.Today
                }
            };

            return View(model);
        }

        // GET: /Place/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new CreatePlaceViewModel();
            await FillCreateLists(model);

            return View(model);
        }

        // POST: /Place/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePlaceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // При грешка показваме формата отново, със съобщенията за грешки
                await FillCreateLists(model);
                return View(model);
            }

            await _placeService.CreateAsync(new CreatePlaceDto
            {
                Name = model.Name,
                AreaId = model.AreaId!.Value,
                PlaceTypeId = model.PlaceTypeId!.Value,
                Fee = model.Fee!.Value,
                Description = model.Description
            });

            return RedirectToAction(nameof(Index));
        }

        // POST: /Place/AddReview
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview([Bind(Prefix = "NewReview")] CreateReviewViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var place = await _placeService.GetDetailsAsync(model.TravelPlaceId);

                if (place == null)
                {
                    return NotFound();
                }

                return View(nameof(Details), new PlaceDetailsViewModel { Place = place, NewReview = model });
            }

            var added = await _placeService.AddReviewAsync(new CreateReviewDto
            {
                TravelPlaceId = model.TravelPlaceId,
                VisitorName = model.VisitorName,
                VisitorEmail = model.VisitorEmail,
                Rating = model.Rating,
                Comment = model.Comment,
                VisitDate = model.VisitDate!.Value
            });

            if (!added)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Details), new { id = model.TravelPlaceId });
        }

        private async Task FillCreateLists(CreatePlaceViewModel model)
        {
            model.Areas = ToSelectList(await _lookupService.GetAreasAsync());
            model.PlaceTypes = ToSelectList(await _lookupService.GetPlaceTypesAsync());
        }

        private static List<SelectListItem> ToSelectList(List<LookupDto> items)
        {
            return items
                .Select(i => new SelectListItem(i.Name, i.Id.ToString()))
                .ToList();
        }
    }
}
