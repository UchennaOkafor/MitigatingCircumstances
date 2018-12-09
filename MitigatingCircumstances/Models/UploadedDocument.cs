namespace MitigatingCircumstances.Models
{
    public class UploadedDocument
    {
        public int Id { get; set; }

        public virtual ApplicationUser UploadedBy { get; set; }

        public string Url { get; set; }
    }
}
