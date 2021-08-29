using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using car_rent_backend.common;
using car_rent_backend.domain;
using car_rent_backend.service;

namespace car_rent_backend.api
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : Controller
    {
        private readonly ReservationService _service;

        public ReservationController(ReservationService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Reservation> GetAll()
        {
            return _service.GetAll();
        }

        [HttpGet]
        [Route("contracts")]
        public IEnumerable<Reservation> GetContracts()
        {
            return _service.GetContracts();
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Reservation> Get(int id)
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
        public ActionResult<Reservation> Save(Reservation reservation)
        {
            try
            {
                return _service.Save(reservation);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
        public ActionResult<Reservation> Update(Reservation reservation)
        {
            try
            {
                return _service.Update(reservation);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<Reservation> Delete(int id)
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
                return BadRequest("Reservation could not be deleted");
            }
        }

        [HttpPost]
        [Route("calculate")]
        public double Calculate(Reservation reservation)
        {
            return _service.Calculate(reservation);
        }

        [HttpPatch]
        [Route("{id}/contract")]
        public ActionResult<Reservation> ConvertToContract(int id)
        {
            try
            {
                return _service.ConvertToContract(id);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
