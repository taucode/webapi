namespace TauCode.WebApi
{
    public static class DtoHelper
    {
        public static readonly string PayloadTypeHeaderName = "X-Payload-Type";

        public static readonly string ErrorPayloadType = "Error";
        public static readonly string ValidationErrorPayloadType = "Error.Validation";
        public static readonly string CreateResultPayloadType = "CreateResult";
        public static readonly string UpdateResultPayloadType = "UpdateResult";
        public static readonly string DeleteResultPayloadType = "DeleteResult";
    }
}
