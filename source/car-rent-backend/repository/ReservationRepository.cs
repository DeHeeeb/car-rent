using System.Collections.Generic;
using System.Linq;
using car_rent_backend.domain;
using Microsoft.EntityFrameworkCore;

namespace car_rent_backend.repository
{
    public class ReservationRepository : RepositoryBase<Reservation>
    {
        public new List<Reservation> GetAll()
        {
            using var context = new ProjectContext();
            return context.Reservations
                .Include(r => r.Customer)
                .OrderBy(r => r.Id)
                .ToList();
        }

        public new Reservation GetSingle(int id)
        {
            using var context = new ProjectContext();
            return context.Reservations
                .Include(r => r.Customer)
                .Single(r => r.Id == id);
        }
    }
}
