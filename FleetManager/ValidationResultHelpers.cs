using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleetManager
{
    public static class ValidationResultHelpers
    {
        public static bool HasErros(this IList<ValidationResult> results) => results.Count != 0;
    }
}
