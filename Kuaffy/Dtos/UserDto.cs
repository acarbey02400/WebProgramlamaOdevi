using Kuaffy.DataAccess.Entity;
using Microsoft.AspNetCore.Identity;

namespace Kuaffy.Dtos
{
    public class UserDto:IDto
    {
       
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserRoles { get; set; }



      
    }
}
