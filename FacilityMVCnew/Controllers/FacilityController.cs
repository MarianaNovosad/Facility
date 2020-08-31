using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FacilityMVCnew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FacilityMVCnew.Controllers
{
    public class FacilityController : Controller
    {
        private readonly ILogger<FacilityController> _logger;

        FacilityContext _context;

        public FacilityController(FacilityContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var facilities = from s in _context.Facilities.Include(x => x.FacilityStatus)
                           select s;
            
            int pageSize = 10;
            return View(await PaginatedList<Facility>.CreateAsync(facilities.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
            {
                return NotFound();
            }
            StatusesDropDownList(facility.FacilityStatusId);
            return View(facility);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Address,PhoneNumber,Email,FacilityStatusId")] Facility facility)
        {           
            if (id != facility.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facility);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    //if (FacilityExists(facility.Id))
                    //{
                    //    return NotFound();
                    //}
                    //else
                    //{
                        throw;
                    //}
                }
                return RedirectToAction("Index");
            }
            StatusesDropDownList(facility.FacilityStatusId);
            return View(facility);
        }

        public IActionResult Create()
        {
            StatusesDropDownList();
            return View();
        }

        private void StatusesDropDownList(object selectedStatus = null)
        {
            var departmentsQuery = from s in _context.FacilityStatuses
                                   orderby s.Name
                                   select s;
            ViewBag.FacilityStatusID = new SelectList(departmentsQuery, "FacilityStatusId", "Name", selectedStatus);                       
        }

        
        [HttpPost]
        public ActionResult Create([Bind("Name,Address,PhoneNumber,Email,FacilityStatusId")]Facility facility)
        {
            if (ModelState.IsValid)
            {
                _context.Facilities.Add(facility);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            StatusesDropDownList(facility.FacilityStatusId);
            return View(facility);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Facility facility = await _context.Facilities.FirstOrDefaultAsync(p => p.Id == id);
                if (facility != null)
                    return View(facility);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Facility facility = await _context.Facilities.FirstOrDefaultAsync(p => p.Id == id);
                if (facility != null)
                {
                    _context.Facilities.Remove(facility);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }

    }
}
