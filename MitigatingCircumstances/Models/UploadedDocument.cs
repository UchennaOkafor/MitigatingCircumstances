using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MitigatingCircumstances.Models
{
    public class UploadedDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CloudId { get; set; }

        public virtual ExtensionRequest ExtensionRequest { get; set; }

        public virtual ApplicationUser UploadedBy { get; set; }

        public string MediaLink { get; set; }

        public string Name { get; set; }

        public string Bucket { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.ToLocalTime();
    }
}
