using System.Collections.Generic;
using System.Linq;
using car_rent_backend.common;
using car_rent_backend.domain;
using car_rent_backend.repository;
using car_rent_backend.service.validation;

namespace car_rent_backend.service
{
    public class ReservationService
    {
        private readonly ReservationRepository _repo = new();
        private readonly ReservationValidationService _validation = new();
        private const double PriceLow = 80;
        private const double PriceMedium = 100;
        private const double PriceHigh = 150;

        public List<Reservation> GetAll()
        {
            return _repo.GetAll();
        }

        public List<Reservation> GetContracts()
        {
            return _repo.GetContracts();
        }

        public Reservation Get(int id)
        {
            try
            {
                return _repo.GetSingle(id);
            }
            catch (NotFoundException e)
            {
                throw new NotFoundException("Reservation not found", e);
            }
        }

        public Reservation Save(Reservation reservation)
        {
            _validation.ValidateSave(reservation);
            var saved = _repo.Save(reservation);
            saved.Total = Calculate(saved);

            return saved;
        }

        public Reservation Update(Reservation reservation)
        {
            _validation.ValidateUpdate(reservation);
            var updated = _repo.Update(reservation);
            updated.Total = Calculate(reservation);

            return updated;
        }

        public Reservation Delete(int id)
        {
            try
            {
                return _repo.Delete(id);
            }
            catch (NotFoundException e)
            {
                throw new NotFoundException("Reservation not found", e);
            }
            catch (CouldNotBeDeletedException e)
            {
                throw new CouldNotBeDeletedException("Reservation could not be deleted", e);
            }
        }

        public double Calculate(Reservation reservation)
        {
            var timespan = (reservation.EndDate - reservation.StartDate);
            if (timespan == null) return 0;
            var days = timespan.Value.Days + 1;
            return reservation.CarClass switch
            {
                CarClass.Low => days * PriceLow,
                CarClass.Medium => days * PriceMedium,
                CarClass.High => days * PriceHigh,
                _ => 0d
            };

        }

        public Reservation ConvertToContract(int id)
        {
            var reservation = Get(id);
            if (reservation.IsContract)
            {
                throw new ValidationException("Contract already exists");
            }

            reservation.Total = Calculate(reservation);
            reservation.IsContract = true;

            return _repo.Update(reservation);
        }
    }
}