namespace TauCode.WebApi
{
    public class ValidationFailureDto
    {
        public ValidationFailureDto()
        {
        }

        public ValidationFailureDto(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public string Code { get; set; }
        public string Message { get; set; }
    }
}
