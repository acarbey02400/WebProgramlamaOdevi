using Kuaffy.DataAccess.Entity;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kuaffy.Models
{
    public class Comment:IEntity
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        public DateTime dateTime { get; set; }

    }
}
