using Kuaffy.DataAccess.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Kuaffy.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    [Area("Admin")]
    //[Route("[area]/[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ICompanyDal _companyDal;
        private readonly IAppointmentDal _appointmentDal;
        private readonly ICommentDal _commentDal;
        public HomeController(ICompanyDal companyDal, IAppointmentDal appointmentDal, ICommentDal commentDal)
        {
            _companyDal = companyDal;
            _appointmentDal = appointmentDal;
            _commentDal = commentDal;
        }

        public IActionResult Index()
        {
            ViewBag.CompanyCount=_companyDal.GetAll(p=>p.Status==true).Count();
            ViewBag.AppointmentCount=_appointmentDal.GetAll().Count();
            ViewBag.CommentCount= _commentDal.GetAll().Count();
            return View();
        }
    }
}
