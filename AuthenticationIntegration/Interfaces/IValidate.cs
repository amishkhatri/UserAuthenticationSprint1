namespace xp.auth.core.integration.Interfaces
{
    public interface IValidate
    {
        bool IsValid { get; set; }

        bool Validate(string validate);

        bool IsValidOpertation(string OperationType);
    }
}
