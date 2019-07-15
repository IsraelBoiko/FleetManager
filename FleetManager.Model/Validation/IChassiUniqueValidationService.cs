namespace FleetManager.Model.Validation
{
    /// <summary>
    /// Valida se o chassi é único.
    /// </summary>
    public interface IChassiUniqueValidationService
    {
        bool IsValid(string chassi);
    }
}