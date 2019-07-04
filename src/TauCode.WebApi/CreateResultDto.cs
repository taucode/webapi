namespace TauCode.WebApi
{
    public class CreateResultDto
    {
        public string InstanceId { get; set; }
        public string Url { get; set; }
    }

    public class CreateResultDto<TContent> : CreateResultDto
    {
        public TContent Content { get; set; }
    }
}
