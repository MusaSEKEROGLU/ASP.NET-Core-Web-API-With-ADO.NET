using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_Web_API_With_ADO.NET.Models
{
    public class Footballer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FootballerName { get; set; }
        public int Age { get; set; }
        public string Address { get; set; } = String.Empty;
    }
}
