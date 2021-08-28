using car_rent_backend.domain;

namespace car_rent_backend.service.validation
{
    public class ReservationValidationService : ValidationService<Reservation>
    {
        public override void ValidateSave(Reservation reservation)
        {
            Assert(NotNull(reservation.ReservationNr), "ReservationNr must not be null");
            Assert(NotNull(reservation.CustomerId), "CustomerId must not be null");
            Assert(NotNull(reservation.CarClass), "CarClass must not be null");
            Assert(NotNull(reservation.StartDate), "StartDate must not be null");
            Assert(NotNull(reservation.EndDate), "EndDate must not be null");
            Assert(IsNull(reservation.Total), "Total must be null");
            Assert(!reservation.IsContract, "IsContract cannot be true");

            CheckForViolations();
        }

        public override void ValidateUpdate(Reservation reservation)
        {
            Assert(NotNull(reservation.Id), "Id must not be null");
            ValidateSave(reservation);

            CheckForViolations();
        }
    }
}
