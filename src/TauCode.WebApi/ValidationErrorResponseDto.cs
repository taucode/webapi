using System.Collections.Generic;

namespace TauCode.WebApi
{
    public class ValidationErrorResponseDto : ErrorResponseDto
    {
        public static ValidationErrorResponseDto Standard => new ValidationErrorResponseDto
        {
            Message = "The request is invalid.",
        };

        public ValidationErrorResponseDto()
        {
            this.Code = DtoHelper.ValidationErrorCode;
            this.Failures = new Dictionary<string, ValidationFailureDto>();
        }

        public IDictionary<string, ValidationFailureDto> Failures { get; set; }

        public ValidationErrorResponseDto WithValidationError(string key, string code, string message)
        {
            this.Failures[key] = new ValidationFailureDto(code, message);
            return this;
        }
    }
}
