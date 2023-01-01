using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kuaffy.Models;
using Kuaffy.DataAccess.Abstract;
using Kuaffy.DataAccess.Concrete;

namespace Kuaffy.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompaniesController : Controller
    {
        private readonly ICompanyDal _context;

        public CompaniesController(ICompanyDal context)
        {
            _context = context;
        }

        // GET: Admin/Companies
        public IActionResult Index()
        {
              return _context.GetCompaniesDetail() != null ? 
                          View(_context.GetCompaniesDetail()) :
                          Problem("Entity set 'KuaffyDataContext.Companies'  is null.");
        }

        // GET: Admin/Companies/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _context.GetAll() == null)
            {
                return NotFound();
            }

            var company =  _context.Get(p => p.Id == id);


            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Admin/Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,CompanyType,Address,City,Point,Description,Status")] Company company)
        {
            if (ModelState.IsValid)
            {
                _context.Add(company);
                
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Admin/Companies/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _context.GetAll() == null)
            {
                return NotFound();
            }

            var company = _context.Get(p => p.Id == id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Admin/Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,CompanyType,Address,City,Point,Description,Status")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(company);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            return View(company);
        }

        // GET: Admin/Companies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _context.GetAll() == null)
            {
                return NotFound();
            }

            var company = _context.Get(p => p.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Admin/Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_context.Get(p => p.Id == id)==null)
            {
                return Problem("Entity set 'KuaffyDataContext.Companies'  is null.");
            }
            var company = _context.Get(p => p.Id == id);
            if (company != null)
            {
                _context.Delete(company);
            }
            
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
            var result = _context.Get(e => e.Id == id);
            if (result == null)
            {
                return false;
            }
            return true;
        }
    }
}
