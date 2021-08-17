using System;

namespace car_rent_backend.common
{
    public class CouldNotBeDeletedException : Exception
    {
        public CouldNotBeDeletedException()
        {
        }

        public CouldNotBeDeletedException(string message) : base(message)
        {
        }

        public CouldNotBeDeletedException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
