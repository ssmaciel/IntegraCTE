using IntegraCTE.Core.ValidationMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Infra.ValidationMessages
{
    public class ValidationMessage : IValidationMessage
    {
        private readonly List<MessageDTO> _validations = new();
        public void AddMessage(string message, ValidationType type)
        {
            _validations.Add(new MessageDTO
            {
                Message = message,
                Type = type
            });
        }

        public IEnumerable<MessageDTO> GetValidations()
        {
            return _validations.ToList();
        }

        public bool HasValidation()
        {
            return _validations.Any();
        }
    }
}
