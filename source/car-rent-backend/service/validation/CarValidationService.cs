using car_rent_backend.domain;

namespace car_rent_backend.service.validation
{
    public class CarValidationService : ValidationService<Car>
    {
        public override void ValidateSave(Car car)
        {
            Assert(NotNull(car.CarNr), "CarNr must not be null");
            Assert(NotNull(car.Type), "Type must not be null");
            Assert(NotNull(car.Class), "Class must not be null");
            Assert(NotNull(car.Brand), "Brand must not be null");

            CheckForViolations();
        }

        public override void ValidateUpdate(Car car)
        {
            Assert(NotNull(car.Id), "Id must not be null");
            ValidateSave(car);

            CheckForViolations();
        }
    }
}
