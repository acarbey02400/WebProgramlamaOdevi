using Kuaffy.DataAccess.Entity;
using System.ComponentModel.DataAnnotations;

namespace Kuaffy.Models
{
    public class CompanyType:IEntity
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
    }
}
