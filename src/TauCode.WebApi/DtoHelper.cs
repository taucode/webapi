namespace TauCode.WebApi;

public static class DtoHelper
{
    public static readonly string PayloadTypeHeaderName = "X-Payload-Type";
    public static readonly string DeletedInstanceIdHeaderName = "X-Deleted-Instance-Id";

    public static readonly string ErrorPayloadType = "Error";
    public static readonly string ValidationErrorPayloadType = "Error.Validation";
}