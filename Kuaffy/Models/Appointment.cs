using Kuaffy.DataAccess.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kuaffy.Models
{
    public class Appointment:IEntity
    {
        public int Id { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [StringLength(450,ErrorMessage ="Kullanıcı id maksimum karakteri geçti.")]
        public string? UserId { get; set; }
        public DateTime dateTime { get; set; }
    }
}
    