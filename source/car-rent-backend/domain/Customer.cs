
namespace car_rent_backend.domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerNr { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Street { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
    }
}
