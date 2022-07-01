using System.ComponentModel.DataAnnotations;

namespace MailService.WebApi.Models
{
    public class UserClass
    {
        [Key]
        public int Id { get; set; }
        public string source { get; set; }
        public string destination { get; set; }
    }
}
