using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace car_rent_backend.domain
{
    public class Reservation
    {
        public int? Id { get; set; }
        public string ReservationNr { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }
        public CarClass? CarClass { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Total { get; set; }
        public bool IsContract { get; set; }
    }
}
