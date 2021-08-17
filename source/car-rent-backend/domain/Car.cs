
namespace car_rent_backend.domain
{
    public class Car
    {
        public int Id { get; set; }
        public string CarNr { get; set; }
        public CarType Type { get; set; }
        public CarClass Class { get; set; }
        public string Brand { get; set; }
    }
}
