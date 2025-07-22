namespace DigitalProject.Services.Interface
{
    public interface IValidatorService
    {
        void Validate<T>(T model);
    }
}
