using System;
using System.Collections.Generic;
using System.Linq;
using car_rent_backend.common;

namespace car_rent_backend.service.validation
{
    public abstract class ValidationService<T>
    {
        public List<string> ValidationMessages = new();

        public abstract void ValidateSave(T obj);

        public abstract void ValidateUpdate(T obj);

        protected void CheckForViolations()
        {
            if (ValidationMessages.Any())
            {
                var validationMessage = string.Join("\r\n", ValidationMessages);
                throw new ValidationException(validationMessage);
            }
        }

        protected void Assert(bool assertion, string validationMessage)
        {
            if (!assertion)
            {
                ValidationMessages.Add(validationMessage);
            }
        }

        protected bool NotNull(object obj)
        {
            return obj != null;
        }

        protected bool IsNull(object obj)
        {
            return obj == null;
        }
    }
}
