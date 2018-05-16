using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab1_ef;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using lab4_igi.Models;
using Lab4.Models.Filters;

namespace lab4_igi.Controllers
{
    public class ServicesController : Controller
    {
        private readonly HotelContext _context;

        public ServicesController(HotelContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            var journal = JsonConvert.DeserializeObject<List<string>>(HttpContext.Session.GetString("Journal"));
            ViewBag.UserActions = journal;

            var hotelContext = _context.Services.Include(s => s.Client).Include(s => s.Employee).Include(s => s.ServiceType);
            return View(await hotelContext.ToListAsync());
        }

        public ActionResult SortedList(bool first, bool second, bool third)
        {
            var sortedList = _context.Services.ToList();
            if ((first & second & third))
            {
                sortedList.Sort(new ServiceComparer());
                sortedList = sortedList.OrderBy(p => p.ClientId).ToList();
            }
            else
            {
                if (first)
                {
                    sortedList = sortedList.OrderBy(p => p.ClientId).ToList();
                }
                if (second)
                {
                    sortedList = sortedList.OrderBy(p => p.EmployeeId).ToList();
                }
                if (third)
                {
                    sortedList = sortedList.OrderBy(p => p.ServiceTypeId).ToList();
                }
            }
            return PartialView("SortedList", sortedList);
        }

        public void SaveFiltration(string find, bool first, bool second, bool third)
        {
            var findingTextJSON = JsonConvert.SerializeObject(find);
            HttpContext.Session.SetString("Service.Finding", findingTextJSON);
            var filterFirstJSON = JsonConvert.SerializeObject(first.ToString());
            HttpContext.Session.SetString("Service.Filter.First", filterFirstJSON);
            var filterSecondJSON = JsonConvert.SerializeObject(second.ToString());
            HttpContext.Session.SetString("Service.Filter.Second", filterSecondJSON);
            var filterThirdJSON = JsonConvert.SerializeObject(third.ToString());
            HttpContext.Session.SetString("Service.Filter.Third", filterThirdJSON);
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Client)
                .Include(s => s.Employee)
                .Include(s => s.ServiceType)
                .SingleOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        //
        // GET: Services/Create
        public IActionResult Create()
        {
            var serviceString = HttpContext.Session.GetString("Service");
            var service = serviceString == null ? new Service() : JsonConvert.DeserializeObject<Service>(serviceString);
            ViewBag.ClientId = service.ClientId;
            ViewBag.ServiceTypeId = service.ServiceTypeId;
            ViewBag.EmployeeId = service.EmployeeId;

            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "ServiceTypeId", "ServiceTypeId");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServicesSaveForm]
        public async Task<IActionResult> Create([Bind("ServiceId,ClientId,EmployeeId,ServiceTypeId")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", service.ClientId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", service.EmployeeId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "ServiceTypeId", "ServiceTypeId", service.ServiceTypeId);
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.SingleOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", service.ClientId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", service.EmployeeId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "ServiceTypeId", "ServiceTypeId", service.ServiceTypeId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,ClientId,EmployeeId,ServiceTypeId")] Service service)
        {
            if (id != service.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServiceId))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", service.ClientId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", service.EmployeeId);
            ViewData["ServiceTypeId"] = new SelectList(_context.ServiceTypes, "ServiceTypeId", "ServiceTypeId", service.ServiceTypeId);
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Client)
                .Include(s => s.Employee)
                .Include(s => s.ServiceType)
                .SingleOrDefaultAsync(m => m.ServiceId == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.SingleOrDefaultAsync(m => m.ServiceId == id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ServiceId == id);
        }
    }
}
