namespace TauCode.WebApi;

public class ErrorDto
{
    public ErrorDto()
    {
    }

    public ErrorDto(string code, string message)
    {
        this.Code = code;
        this.Message = message;
    }

    public string Code { get; set; }
    public string Message { get; set; }
}