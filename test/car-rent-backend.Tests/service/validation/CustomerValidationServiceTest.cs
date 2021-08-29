using System;
using car_rent_backend.common;
using car_rent_backend.domain;
using car_rent_backend.service.validation;
using Xunit;

namespace car_rent_backend.Tests.service.validation
{
    public class CustomerValidationServiceTest
    {
        [Fact]
        public void GivenEmptyCustomer_WhenValidateSaveIsCalled_ThenValidationMessagesAreFilled()
        {
            var validator = new CustomerValidationService();
            var customer = new Customer();

            Assert.Throws<ValidationException>(() => validator.ValidateSave(customer));
            
            Assert.Equal(6, validator.ValidationMessages.Count);
        }

        [Fact]
        public void GivenValidCustomer_WhenValidateSaveIsCalled_ThenNoExceptionIsThrown()
        {
            var validator = new CustomerValidationService();
            var customer = new Customer()
            {
                CustomerNr = "ABC",
                FirstName = "Lukas",
                LastName = "Heeb",
                Street = "Buchenstrasse 11",
                Zip = 9000,
                City = "St. Gallen"
            };

            validator.ValidateSave(customer);

            Assert.Empty(validator.ValidationMessages);
        }

        [Fact]
        public void GivenCustomerWithMissingId_WhenValidateUpdateIsCalled_ThenValidationMessagesAreFilled()
        {
            var validator = new CustomerValidationService();
            var customer = new Customer()
            {
                CustomerNr = "ABC",
                FirstName = "Lukas",
                LastName = "Heeb",
                Street = "Buchenstrasse 11",
                Zip = 9000,
                City = "St. Gallen"
            };

            Assert.Throws<ValidationException>(() => validator.ValidateUpdate(customer));

            Assert.Single(validator.ValidationMessages);
        }

        [Fact]
        public void GivenValidCustomer_WhenValidateUpdateIsCalled_ThenNoExceptionIsThrown()
        {
            var validator = new CustomerValidationService();
            var customer = new Customer()
            {
                Id = 42,
                CustomerNr = "ABC",
                FirstName = "Lukas",
                LastName = "Heeb",
                Street = "Buchenstrasse 11",
                Zip = 9000,
                City = "St. Gallen"
            };

            validator.ValidateSave(customer);

            Assert.Empty(validator.ValidationMessages);
        }
    }
}
