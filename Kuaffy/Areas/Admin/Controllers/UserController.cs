using Kuaffy.DataAccess;
using Kuaffy.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Kuaffy.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
    
        private readonly KuaffyContext _kuaffyContext;
        public UserController(UserManager<IdentityUser> userManager, KuaffyContext kuaffyContext)
        {
            _userManager = userManager;
            _kuaffyContext = kuaffyContext;
          
        }
        public async Task<IActionResult> Index()
        {

            var result = from us in _kuaffyContext.Users
                         join uro in _kuaffyContext.UserRoles
                         on us.Id equals uro.UserId
                         join ro in _kuaffyContext.Roles
                         on uro.RoleId equals ro.Id
                         select new UserDto { UserEmail = us.Email, UserId = us.Id, UserName = us.UserName, UserRoles = ro.Name };
            result.ToList();
            return result != null ? View(result.ToList()) : Problem("Entity set 'KuaffyContext.Users'  is null.");
        }

        
    }
}
