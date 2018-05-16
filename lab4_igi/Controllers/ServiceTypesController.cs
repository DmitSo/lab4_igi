using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab1_ef;
using Newtonsoft.Json;
using lab4_igi.Models;
using Microsoft.AspNetCore.Http;
using Lab4.Models.Filters;

namespace lab4_igi.Controllers
{
    public class ServiceTypesController : Controller
    {
        private readonly HotelContext _context;

        public ServiceTypesController(HotelContext context)
        {
            _context = context;
        }

        // GET: ServiceTypes
        public async Task<IActionResult> Index()
        {
            var journal = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("Journal"));
            ViewBag.UserActions = journal;

            return View(await _context.ServiceTypes.ToListAsync());
        }

        public ActionResult SortedList(bool first, bool second)
        {
            var sortedList = _context.ServiceTypes.ToList();
            if (first & !second)
            {
                sortedList = sortedList.OrderBy(p => p.Name).ToList();
            }
            else if (!first & second)
            {
                sortedList = sortedList.OrderBy(p => p.Cost).ToList();
            }
            else if (first & second)
            {
                sortedList.Sort(new ServiceTypesComparer());
            }
            else
            {
                sortedList = _context.ServiceTypes.ToList();
            }
            return PartialView("SortedList", sortedList);
        }

        public void SaveFiltration(string find, bool first, bool second)
        {
            var findingTextJSON = JsonConvert.SerializeObject(find);
            HttpContext.Session.SetString("ServiceType.Finding", findingTextJSON);
            var filterFirstJSON = JsonConvert.SerializeObject(first.ToString());
            HttpContext.Session.SetString("ServiceType.Filter.First", filterFirstJSON);
            var filterSecondJSON = JsonConvert.SerializeObject(second.ToString());
            HttpContext.Session.SetString("ServiceType.Filter.Second", filterSecondJSON);
        }

        // GET: ServiceTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceType = await _context.ServiceTypes
                .SingleOrDefaultAsync(m => m.ServiceTypeId == id);
            if (serviceType == null)
            {
                return NotFound();
            }

            return View(serviceType);
        }

        // GET: ServiceTypes/Create
        public IActionResult Create()
        {
            var serviceTypeStr = HttpContext.Session.GetString("ServiceType");
            var serviceType = serviceTypeStr == null ? 
                new ServiceType() : 
                JsonConvert.DeserializeObject<ServiceType>(serviceTypeStr);

            ViewBag.Name = serviceType.Name;
            ViewBag.Price = serviceType.Cost;
            ViewBag.Description = serviceType.Description;

            return View();
        }

        // POST: ServiceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceTypesSaveForm]
        public async Task<IActionResult> Create([Bind("ServiceTypeId,Name,Description,Cost")] ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }

        // GET: ServiceTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceType = await _context.ServiceTypes.SingleOrDefaultAsync(m => m.ServiceTypeId == id);
            if (serviceType == null)
            {
                return NotFound();
            }
            return View(serviceType);
        }

        // POST: ServiceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceTypeId,Name,Description,Cost")] ServiceType serviceType)
        {
            if (id != serviceType.ServiceTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceTypeExists(serviceType.ServiceTypeId))
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
            return View(serviceType);
        }

        // GET: ServiceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceType = await _context.ServiceTypes
                .SingleOrDefaultAsync(m => m.ServiceTypeId == id);
            if (serviceType == null)
            {
                return NotFound();
            }

            return View(serviceType);
        }

        // POST: ServiceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceType = await _context.ServiceTypes.SingleOrDefaultAsync(m => m.ServiceTypeId == id);
            _context.ServiceTypes.Remove(serviceType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceTypeExists(int id)
        {
            return _context.ServiceTypes.Any(e => e.ServiceTypeId == id);
        }
    }
}
