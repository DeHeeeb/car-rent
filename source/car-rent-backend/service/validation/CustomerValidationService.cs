using car_rent_backend.domain;

namespace car_rent_backend.service.validation
{
    public class CustomerValidationService : ValidationService<Customer>
    {
        public override void ValidateSave(Customer customer)
        {
            Assert(NotNull(customer.CustomerNr), "CustomerNr must not be null");
            Assert(NotNull(customer.LastName), "LastName must not be null");
            Assert(NotNull(customer.FirstName), "FirstName must not be null");
            Assert(NotNull(customer.Street), "Street must not be null");
            Assert(NotNull(customer.Zip), "Zip must not be null");
            Assert(NotNull(customer.City), "City must not be null");

            CheckForViolations();
        }

        public override void ValidateUpdate(Customer customer)
        {
            Assert(NotNull(customer.Id), "Id must not be null");
            ValidateSave(customer);

            CheckForViolations();
        }
    }
}
