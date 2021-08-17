using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using car_rent_backend.common;
using car_rent_backend.domain;
using car_rent_backend.service;

namespace car_rent_backend.api
{
    [ApiController]
    [Route("[controller]")]
    public class CarController : Controller
    {
        private readonly CarService _service = new();

        [HttpGet]
        public IEnumerable<Car> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Car> Get(int id)
        {
            try
            {
                return _service.Get(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public Car Save(Car car)
        {
            return _service.Save(car);
        }

        [HttpPatch]
        public Car Update(Car car)
        {
            return _service.Update(car);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<Car> Delete(int id)
        {
            try
            {
                return _service.Delete(id);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (CouldNotBeDeletedException)
            {
                return BadRequest("Customer could not be deleted");
            }
        }
    }
}
