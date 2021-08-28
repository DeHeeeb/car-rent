using System;
using System.Collections.Generic;
using System.Linq;
using car_rent_backend.common;

namespace car_rent_backend.service.validation
{
    public abstract class ValidationService<T>
    {
        private readonly List<string> _validationMessages = new();

        public abstract void ValidateSave(T obj);

        public abstract void ValidateUpdate(T obj);

        protected void CheckForViolations()
        {
            if (_validationMessages.Any())
            {
                var validationMessage = string.Join("\r\n", _validationMessages);
                throw new ValidationException(validationMessage);
            }
        }

        protected void Assert(bool assertion, string validationMessage)
        {
            if (!assertion)
            {
                _validationMessages.Add(validationMessage);
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
