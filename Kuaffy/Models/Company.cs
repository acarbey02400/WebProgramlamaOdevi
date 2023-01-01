using Kuaffy.DataAccess.Entity;
using System.ComponentModel.DataAnnotations;

namespace Kuaffy.Models
{
    public class Company:IEntity
    {
        public int Id { get; set; }
        [StringLength(50,ErrorMessage ="Maksimum 50 karakter girilebilir.")]
        public string? Name { get; set; }
        [Required]
        public int CompanyType { get; set; }
        [Required]
        [StringLength(250, ErrorMessage = "Maksimum 250 karakter girilebilir.")]
        public string? Address { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public float Point { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public bool Status { get; set; }

    }
}
