namespace TauCode.WebApi
{
    public static class DtoHelper
    {
        public static readonly string ValidationErrorCode = "ValidationError";
        public static readonly string BusinessLogicErrorCode = "BusinessLogicError";
        public static readonly string ForbiddenErrorCode = "ForbiddenError";

        public static readonly string SubReasonHeaderName = "X-Sub-Reason";
        public static readonly string InstanceIdHeaderName = "X-Instance-Id";
        public static readonly string RouteHeaderName = "X-Route";

        public static readonly string ValidationErrorSubReason = "Validation";
        public static readonly string BusinessLogicErrorSubReason = "BusinessLogic";
        public static readonly string ForbiddenErrorSubReason = "Forbidden";
    }
}
