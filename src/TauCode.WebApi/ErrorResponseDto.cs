namespace TauCode.WebApi
{
    public class ErrorResponseDto
    {
        public ErrorResponseDto()
        {   
        }

        public ErrorResponseDto(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public string Code { get; set; }
        public string Message { get; set; }
    }
}
