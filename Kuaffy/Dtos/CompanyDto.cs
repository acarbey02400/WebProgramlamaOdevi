using Kuaffy.DataAccess.Entity;
using System.ComponentModel.DataAnnotations;

namespace Kuaffy.Dtos
{
    public class CompanyDto:IDto
    {
        public int CompanyId { get; set; }
        public string? Name { get; set; }
       
        public string? CompanyTypeName { get; set; }
       
        public string? Address { get; set; }
      
        public string? City { get; set; }
      
        public float Point { get; set; }
      
        public string? Description { get; set; }
      
        public bool Status { get; set; }
    }
}
