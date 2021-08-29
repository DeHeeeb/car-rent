using System.Collections.Generic;
using System.Linq;
using car_rent_backend.domain;
using Microsoft.EntityFrameworkCore;

namespace car_rent_backend.repository
{
    public class CustomerRepository : RepositoryBase<Customer>
    {
        public CustomerRepository(DbContextOptions<ProjectContext> options) : base(options)
        {
        }

        public List<Customer> Search(string text)
        {
            text = text.ToLower();
            using var context = new ProjectContext(Options);

            return context.Customers
                .Where(c => 
                    c.CustomerNr.ToLower().Contains(text) || 
                    c.FirstName.ToLower().Contains(text) || 
                    c.LastName.ToLower().Contains(text)
                )
                .OrderBy(c => c.Id)
                .ToList();
        }
    }
}
