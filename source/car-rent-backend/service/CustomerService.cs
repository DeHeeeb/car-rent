using System.Collections.Generic;
using car_rent_backend.domain;
using car_rent_backend.repository;

namespace car_rent_backend.service
{
    public class CustomerService
    {
        private readonly CustomerRepository _repo = new();

        public List<Customer> GetAll()
        {
            return _repo.GetAll();
        }

        public Customer Get(int id)
        {
            return _repo.GetSingle(id);
        }
    }
}
