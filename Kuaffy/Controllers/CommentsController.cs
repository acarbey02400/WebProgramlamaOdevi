using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kuaffy.Models;
using Kuaffy.DataAccess.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace Kuaffy.Controllers
{
    [Authorize(Roles ="User,Admin,Employee")]
    public class CommentsController : Controller
    {
        private readonly ICommentDal _commentDal;

        public CommentsController(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        // GET: Comments
        /*_context.Appointments != null ? 
                          View(await _context.Appointments.ToListAsync()) :
                          Problem("Entity set 'KuaffyContext.Appointments'  is null.");*/
        public IActionResult Index()
        {
            return _commentDal.GetAll() !=null? 
                View(_commentDal.GetAll().ToList()):
                 Problem("Entity set 'KuaffyContext.Appointments'  is null.");
        }

        // GET: Comments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment =  _commentDal.Get(p=>p.Id== id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Description,AppointmentId,dateTime")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _commentDal.Add(comment);
                
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment =  _commentDal.Get(p=>p.Id== id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,AppointmentId,dateTime")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _commentDal.Update(comment);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            return View(comment);
        }

        // GET: Comments/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = _commentDal.Get(p => p.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            var comment = _commentDal.Get(p => p.Id == id);
            if (comment != null)
            {
                _commentDal.Delete(comment);
            }
            
           
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            var result = _commentDal.Get(e => e.Id == id);
            if (result==null)
            {
                return false;
            }
         return true;
        }
    }
}
