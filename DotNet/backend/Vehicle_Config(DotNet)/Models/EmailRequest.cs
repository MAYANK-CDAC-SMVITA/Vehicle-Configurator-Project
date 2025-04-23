namespace Vehicle_Config_DotNet_.Models
{
    public class EmailRequest
    {
        public IEnumerable<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public List<IFormFile> Attachments { get; set; }
    }
}
