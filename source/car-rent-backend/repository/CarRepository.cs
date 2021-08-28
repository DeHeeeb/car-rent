using System.Collections.Generic;
using System.Linq;
using car_rent_backend.domain;

namespace car_rent_backend.repository
{
    public class CarRepository : RepositoryBase<Car>
    {
        public List<Car> Search(string text)
        {
            text = text.ToLower();
            using var context = new ProjectContext();

            return context.Cars
                .Where(c =>
                    c.CarNr.ToLower().Contains(text) ||
                    c.Brand.ToLower().Contains(text)
                )
                .OrderBy(c => c.Id)
                .ToList();
        }
    }
}
