using System.Collections.Generic;
using System.Linq;
using car_rent_backend.common;
using car_rent_backend.domain;
using car_rent_backend.repository;

namespace car_rent_backend.service
{
    public class CarService
    {
        private readonly CarRepository _repo = new();

        public List<Car> GetAll()
        {
            return _repo.GetAll();
        }

        public Car Get(int id)
        {
            try
            {
                return _repo.GetSingle(id);
            }
            catch (NotFoundException e)
            {
                throw new NotFoundException("Car not found", e);
            }
        }

        public Car Save(Car car)
        {
            return _repo.Save(car);
        }

        public Car Update(Car car)
        {
            return _repo.Update(car);
        }

        public Car Delete(int id)
        {
            try
            {
                return _repo.Delete(id);
            }
            catch (NotFoundException e)
            {
                throw new NotFoundException("Car not found", e);
            }
            catch (CouldNotBeDeletedException e)
            {
                throw new CouldNotBeDeletedException("Car could not be deleted", e);
            }
        }
        public List<Car> Search(string text)
        {
            var cars = _repo.Search(text);

            if (!cars.Any())
            {
                throw new NotFoundException("No car found");
            }

            return cars;
        }
    }
}