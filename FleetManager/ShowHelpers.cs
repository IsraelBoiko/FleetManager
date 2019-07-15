using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace FleetManager
{
    public static class ShowHelpers
    {
        public static void ShowErrors(this TextWriter writer, IList<ValidationResult> results)
        {
            foreach (var result in results)
            {
                writer.WriteLine(result.ErrorMessage);
            }
        }
    }
}
