using System.Collections.Generic;
using System.Linq;
using car_rent_backend.common;
using car_rent_backend.domain;
using car_rent_backend.repository;
using car_rent_backend.service.validation;

namespace car_rent_backend.service
{
    public class CustomerService
    {
        private readonly CustomerRepository _repo;
        private readonly CustomerValidationService _validation;

        public CustomerService(CustomerRepository repo, CustomerValidationService validation)
        {
            _repo = repo;
            _validation = validation;
        }

        public List<Customer> GetAll()
        {
            return _repo.GetAll();
        }

        public Customer Get(int id)
        {
            try
            {
                return _repo.GetSingle(id);
            }
            catch (NotFoundException e)
            {
                throw new NotFoundException("Customer not found", e);
            }
        }

        public Customer Save(Customer customer)
        {
            _validation.ValidateSave(customer);
            return _repo.Save(customer);
        }

        public Customer Update(Customer customer)
        {
            _validation.ValidateUpdate(customer);
            return _repo.Update(customer);
        }

        public Customer Delete(int id)
        {
            try
            {
                return _repo.Delete(id);
            } 
            catch (NotFoundException e)
            {
                throw new NotFoundException("Customer not found", e);
            } 
            catch (CouldNotBeDeletedException e)
            {
                throw new CouldNotBeDeletedException("Customer could not be deleted", e);
            }
        }

        public List<Customer> Search(string text)
        {
            var customers = _repo.Search(text);

            if (!customers.Any())
            {
                throw new NotFoundException("No customer found");
            }

            return customers;
        }
    }
}
