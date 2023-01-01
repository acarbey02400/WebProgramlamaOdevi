using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kuaffy.DataAccess;
using Kuaffy.Models;
using Microsoft.AspNetCore.Authorization;
using Kuaffy.DataAccess.Abstract;
using Kuaffy.DataAccess.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Kuaffy.Controllers
{
    [Authorize(Roles ="Admin,User,Employee")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentDal _appointmentDal;

        string? _userId =string.Empty;
       
        public AppointmentsController(IAppointmentDal appointmentDal, IHttpContextAccessor httpContextAccessor)
        {
            
            _appointmentDal = appointmentDal;
            _userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
         
        }

        // GET: Appointments
        /* return _commentDal.GetAll() !=null? 
                View(_commentDal.GetAll().ToList()):
                 Problem("Entity set 'KuaffyContext.Appointments'  is null.");*/
        public async Task<IActionResult> Index()
        {
              return _appointmentDal.GetAppointmentDetails(_userId) != null ? 
                          View(_appointmentDal.GetAppointmentDetails(_userId)):
                          Problem("Entity set 'KuaffyContext.Appointments'  is null.");
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment =  _appointmentDal.Get(p=>p.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        [Authorize(Roles = "User,Admin,Employee")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin,Employee")]
        public async Task<IActionResult> Create([Bind("Id,CompanyId,UserId,dateTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.UserId= _userId;
                _appointmentDal.Add(appointment);
                
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        [Authorize(Roles = "User,Admin,Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment =  _appointmentDal.Get(p=>p.Id==id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin,Employee")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyId,UserId,dateTime")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _appointmentDal.Update(appointment);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        [Authorize(Roles = "User,Admin,Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = _appointmentDal.Get(p=>p.Id==id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User,Admin,Employee")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_appointmentDal.Get(p=>p.Id==id) == null)
            {
                return Problem("Entity set is null.");
            }
            var appointment = _appointmentDal.Get(p => p.Id == id);
            if (appointment != null)
            {
               _appointmentDal.Delete(appointment);
            }
            
          
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            var result = _appointmentDal.Get(e => e.Id == id);
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
