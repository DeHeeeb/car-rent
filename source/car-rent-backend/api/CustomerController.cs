using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            var customer = _service.Get(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
    }
}
