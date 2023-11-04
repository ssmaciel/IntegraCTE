using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Core.ValidationMessages
{
    public interface IValidationMessage
    {
        void AddMessage(string message, ValidationType type);

        IEnumerable<MessageDTO> GetValidations();

        bool HasValidation();
    }
}
