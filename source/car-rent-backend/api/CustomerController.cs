using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using car_rent_backend.common;
using car_rent_backend.domain;
using car_rent_backend.service;

namespace car_rent_backend.api
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly CustomerService _service = new();

        [HttpGet]
        public IEnumerable<Customer> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Customer> Get(int id)
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
        public Customer Save(Customer customer)
        {
            return _service.Save(customer);
        }

        [HttpPatch]
        public Customer Update(Customer customer)
        {
            return _service.Update(customer);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<Customer> Delete(int id)
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
        public ActionResult<List<Customer>> Search(string text)
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
