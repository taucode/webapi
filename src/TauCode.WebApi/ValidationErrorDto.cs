namespace TauCode.WebApi;

public class ValidationErrorDto : ErrorDto
{
    public const string StandardCode = "ValidationError";
    public const string StandardMessage = "The request is invalid.";

    public static ValidationErrorDto CreateStandard(string message = null)
    {
        return new ValidationErrorDto
        {
            Code = StandardCode,
            Message = message ?? StandardMessage,
        };
    }

    public ValidationErrorDto()
    {
        this.Failures = new Dictionary<string, ValidationFailureDto>();
    }

    public IDictionary<string, ValidationFailureDto> Failures { get; set; }

    public void AddFailure(string key, string code, string message)
    {
        this.Failures[key] = new ValidationFailureDto(code, message);
    }
}