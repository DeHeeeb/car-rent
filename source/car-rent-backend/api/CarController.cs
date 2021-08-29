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
        private readonly CarService _service;

        public CarController(CarService service)
        {
            _service = service;
        }

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
        public ActionResult<Car> Save(Car car)
        {
            try
            {
                return _service.Save(car);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        public ActionResult<Car> Update(Car car)
        {
            try
            {
                return _service.Update(car);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
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

        [HttpGet]
        [Route("find/{text}")]
        public ActionResult<List<Car>> Search(string text)
        {
            try
            {
                return _service.Search(text);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
