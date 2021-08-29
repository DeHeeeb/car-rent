using System;
using car_rent_backend.common;
using car_rent_backend.domain;
using car_rent_backend.service.validation;
using Xunit;

namespace car_rent_backend.Tests.service.validation
{
    public class CarValidationServiceTest
    {
        [Fact]
        public void GivenEmptyCar_WhenValidateSaveIsCalled_ThenValidationMessagesAreFilled()
        {
            var validator = new CarValidationService();
            var car = new Car();

            Assert.Throws<ValidationException>(() => validator.ValidateSave(car));
            
            Assert.Equal(4, validator.ValidationMessages.Count);
        }

        [Fact]
        public void GivenValidCar_WhenValidateSaveIsCalled_ThenNoExceptionIsThrown()
        {
            var validator = new CarValidationService();
            var car = new Car()
            {
                CarNr = "C123",
                Class = CarClass.Medium,
                Type = CarType.Convertible,
                Brand = "BMW"
            };

            validator.ValidateSave(car);

            Assert.Empty(validator.ValidationMessages);
        }

        [Fact]
        public void GivenCarWithMissingId_WhenValidateUpdateIsCalled_ThenValidationMessagesAreFilled()
        {
            var validator = new CarValidationService();
            var car = new Car()
            {
                CarNr = "C123",
                Class = CarClass.Medium,
                Type = CarType.Convertible,
                Brand = "BMW"
            };

            Assert.Throws<ValidationException>(() => validator.ValidateUpdate(car));

            Assert.Single(validator.ValidationMessages);
        }

        [Fact]
        public void GivenValidCar_WhenValidateUpdateIsCalled_ThenNoExceptionIsThrown()
        {
            var validator = new CarValidationService();
            var car = new Car()
            {
                Id = 3,
                CarNr = "C123",
                Class = CarClass.Medium,
                Type = CarType.Convertible,
                Brand = "BMW"
            };

            validator.ValidateSave(car);

            Assert.Empty(validator.ValidationMessages);
        }
    }
}
