using TravelPLaces11.Data.Entities;

namespace TravelPLaces11.Data
{
    public static class DemoData
    {
        private static readonly string[] AreaNames =
        {
            "Благоевград", "Бургас", "Варна", "Велико Търново", "Видин", "Враца", "Габрово",
            "Добрич", "Кърджали", "Кюстендил", "Ловеч", "Монтана", "Пазарджик", "Перник",
            "Плевен", "Пловдив", "Разград", "Русе", "Силистра", "Сливен", "Смолян",
            "София (столица)", "София", "Стара Загора", "Търговище", "Хасково", "Шумен", "Ямбол"
        };

        private static readonly string[] PlaceTypeNames =
        {
            "Музей", "Крепост", "Манастир", "Природна забележителност", "Исторически паметник", "Забавление"
        };

        public static void Seed(TravelPlaces11DbContext db)
        {
            SeedAreas(db);
            SeedPlaceTypes(db);
            SeedPlaces(db);
            SeedVisitorsAndReviews(db);
        }

        private static void SeedAreas(TravelPlaces11DbContext db)
        {
            var existing = db.Areas.Select(a => a.Name).ToList();

            foreach (var name in AreaNames)
            {
                if (!existing.Contains(name, StringComparer.OrdinalIgnoreCase))
                {
                    db.Areas.Add(new Area { Name = name });
                }
            }

            db.SaveChanges();
        }

        private static void SeedPlaceTypes(TravelPlaces11DbContext db)
        {
            var existing = db.PlaceTypes.Select(t => t.Name).ToList();

            foreach (var name in PlaceTypeNames)
            {
                if (!existing.Contains(name, StringComparer.OrdinalIgnoreCase))
                {
                    db.PlaceTypes.Add(new PlaceType { Name = name });
                }
            }

            db.SaveChanges();
        }

        private static void SeedPlaces(TravelPlaces11DbContext db)
        {
            // Добавяме демо обекти само ако базата е празна
            if (db.TravelPlaces.Any())
            {
                return;
            }

            db.TravelPlaces.AddRange(
                CreatePlace(db, "Рилски манастир", "Кюстендил", "Манастир", 0m,
                    "Най-големият манастир в България, обект на ЮНЕСКО."),
                CreatePlace(db, "Царевец", "Велико Търново", "Крепост", 10m,
                    "Средновековна крепост, столица на Второто българско царство."),
                CreatePlace(db, "Белоградчишки скали", "Видин", "Природна забележителност", 6m,
                    "Причудливи скални образувания и крепостта Калето."),
                CreatePlace(db, "Национален исторически музей", "София (столица)", "Музей", 15m,
                    "Най-големият исторически музей в страната."),
                CreatePlace(db, "Античен театър", "Пловдив", "Исторически паметник", 8m,
                    "Римски театър от II век, в който и днес се играят спектакли."));

            db.SaveChanges();
        }

        private static TravelPlace CreatePlace(TravelPlaces11DbContext db, string name, string areaName,
            string typeName, decimal fee, string description)
        {
            return new TravelPlace
            {
                Name = name,
                AreaId = db.Areas.First(a => a.Name == areaName).Id,
                PlaceTypeId = db.PlaceTypes.First(t => t.Name == typeName).Id,
                Fee = fee,
                Description = description
            };
        }

        private static void SeedVisitorsAndReviews(TravelPlaces11DbContext db)
        {
            if (db.Visitors.Any())
            {
                return;
            }

            var places = db.TravelPlaces.OrderBy(p => p.Id).Take(3).ToList();

            if (places.Count == 0)
            {
                return;
            }

            var ivan = new Visitor { FullName = "Иван Петров", Email = "ivan.petrov@example.com" };
            var maria = new Visitor { FullName = "Мария Георгиева", Email = "maria.georgieva@example.com" };
            var nikola = new Visitor { FullName = "Никола Димитров", Email = "nikola.dimitrov@example.com" };

            db.Visitors.AddRange(ivan, maria, nikola);

            db.Reviews.Add(new Review
            {
                TravelPlace = places[0],
                Visitor = ivan,
                Rating = 5,
                Comment = "Невероятно място, задължително за посещение.",
                VisitDate = new DateTime(2026, 7, 14)
            });

            db.Reviews.Add(new Review
            {
                TravelPlace = places[0],
                Visitor = maria,
                Rating = 4,
                Comment = "Много красиво, но имаше доста хора.",
                VisitDate = new DateTime(2026, 8, 2)
            });

            if (places.Count > 1)
            {
                db.Reviews.Add(new Review
                {
                    TravelPlace = places[1],
                    Visitor = nikola,
                    Rating = 5,
                    VisitDate = new DateTime(2026, 9, 20)
                });
            }

            if (places.Count > 2)
            {
                db.Reviews.Add(new Review
                {
                    TravelPlace = places[2],
                    Visitor = maria,
                    Rating = 3,
                    Comment = "Гледките са страхотни, пътят е лош.",
                    VisitDate = new DateTime(2026, 10, 5)
                });
            }

            db.SaveChanges();
        }
    }
}
