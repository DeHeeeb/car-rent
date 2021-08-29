using System;
using car_rent_backend.common;
using car_rent_backend.domain;
using car_rent_backend.service.validation;
using Xunit;

namespace car_rent_backend.Tests.service.validation
{
    public class ReservationValidationServiceTest
    {
        [Fact]
        public void GivenEmptyReservation_WhenValidateSaveIsCalled_ThenValidationMessagesAreFilled()
        {
            var validator = new ReservationValidationService();
            var reservation = new Reservation();

            Assert.Throws<ValidationException>(() => validator.ValidateSave(reservation));
            
            Assert.Equal(5, validator.ValidationMessages.Count);
        }

        [Fact]
        public void GivenValidReservation_WhenValidateSaveIsCalled_ThenNoExceptionIsThrown()
        {
            var validator = new ReservationValidationService();
            var reservation = new Reservation()
            {
                ReservationNr = "R111",
                CustomerId = 2,
                CarClass = CarClass.Medium,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 01, 02)
            };

            validator.ValidateSave(reservation);

            Assert.Empty(validator.ValidationMessages);
        }

        [Fact]
        public void GivenReservationWithIsContractSet_WhenValidateSaveIsCalled_ThenValidationMessagesAreFilled()
        {
            var validator = new ReservationValidationService();
            var reservation = new Reservation()
            {
                ReservationNr = "R111",
                CustomerId = 2,
                CarClass = CarClass.Medium,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 01, 02),
                IsContract = true
            };

            Assert.Throws<ValidationException>(() => validator.ValidateSave(reservation));

            Assert.Single(validator.ValidationMessages);
        }

        [Fact]
        public void GivenReservationWitTotalSet_WhenValidateSaveIsCalled_ThenValidationMessagesAreFilled()
        {
            var validator = new ReservationValidationService();
            var reservation = new Reservation()
            {
                ReservationNr = "R111",
                CustomerId = 2,
                CarClass = CarClass.Medium,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 01, 02),
                Total = 420
            };

            Assert.Throws<ValidationException>(() => validator.ValidateSave(reservation));

            Assert.Single(validator.ValidationMessages);
        }

        [Fact]
        public void GivenReservationWithMissingId_WhenValidateUpdateIsCalled_ThenValidationMessagesAreFilled()
        {
            var validator = new ReservationValidationService();
            var reservation = new Reservation()
            {
                ReservationNr = "R111",
                CustomerId = 2,
                CarClass = CarClass.Medium,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 01, 02)
            };

            Assert.Throws<ValidationException>(() => validator.ValidateUpdate(reservation));

            Assert.Single(validator.ValidationMessages);
        }

        [Fact]
        public void GivenValidReservation_WhenValidateUpdateIsCalled_ThenNoExceptionIsThrown()
        {
            var validator = new ReservationValidationService();
            var reservation = new Reservation()
            {
                Id = 5,
                ReservationNr = "R111",
                CustomerId = 2,
                CarClass = CarClass.Medium,
                StartDate = new DateTime(2021, 01, 01),
                EndDate = new DateTime(2021, 01, 02)
            };

            validator.ValidateSave(reservation);

            Assert.Empty(validator.ValidationMessages);
        }
    }
}
