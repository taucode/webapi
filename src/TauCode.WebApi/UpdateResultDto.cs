namespace TauCode.WebApi
{
    public class UpdateResultDto
    {
        public string InstanceId { get; set; }
        public string Url { get; set; }
    }

    public class UpdateResultDto<TContent> : UpdateResultDto
    {
        public TContent Content { get; set; }
    }
}
