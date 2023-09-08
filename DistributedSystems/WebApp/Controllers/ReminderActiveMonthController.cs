using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Contracts.App;
using Domain;
using Helpers;
using Helpers.Base;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class ReminderActiveMonthController : Controller
    {
        private readonly IAppUOW _uow;
        
        public ReminderActiveMonthController(IAppUOW uow)
        {
            _uow = uow;
        }


        // GET: ReminderActiveMonth
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.ReminderActiveMonthRepository.AllAsync();
            return View(vm);
        }

        // GET: ReminderActiveMonth/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminderActiveMonth = await _uow.ReminderActiveMonthRepository.FindAsync(id.Value);
            if (reminderActiveMonth == null)
            {
                return NotFound();
            }

            return View(reminderActiveMonth);
        }

        // GET: ReminderActiveMonth/Create
        public IActionResult Create()
        {
            ViewData["MonthId"] = new SelectList(_uow.MonthRepository.AllAsync().Result, "Id", "MonthName");
            ViewData["ReminderId"] = new SelectList(_uow.ReminderRepository.AllAsync(User.GetUserId()).Result, "Id", "Id");
            return View();
        }

        // POST: ReminderActiveMonth/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReminderId,MonthId,Id")] ReminderActiveMonth reminderActiveMonth)
        {
            if (ModelState.IsValid)
            {
                reminderActiveMonth.Id = Guid.NewGuid();
                _uow.ReminderActiveMonthRepository.Add(reminderActiveMonth);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(_uow.MonthRepository.AllAsync().Result, "Id", "MonthName", reminderActiveMonth.MonthId);
            ViewData["ReminderId"] = new SelectList(_uow.ReminderRepository.AllAsync(User.GetUserId()).Result, "Id", "Id", reminderActiveMonth.ReminderId);
            
            return View(reminderActiveMonth);
        }

        // GET: ReminderActiveMonth/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminderActiveMonth = await _uow.ReminderActiveMonthRepository.FindAsync(id.Value);
            if (reminderActiveMonth == null)
            {
                return NotFound();
            }
            ViewData["MonthId"] = new SelectList(_uow.MonthRepository.AllAsync().Result, "Id", "MonthName", reminderActiveMonth.MonthId);
            ViewData["ReminderId"] = new SelectList(_uow.ReminderRepository.AllAsync(User.GetUserId()).Result, "Id", "Id", reminderActiveMonth.ReminderId);
            return View(reminderActiveMonth);
        }

        // POST: ReminderActiveMonth/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ReminderId,MonthId,Id")] ReminderActiveMonth reminderActiveMonth)
        {
            if (id != reminderActiveMonth.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.ReminderActiveMonthRepository.Update(reminderActiveMonth);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["MonthId"] = new SelectList(_uow.MonthRepository.AllAsync().Result, "Id", "MonthName", reminderActiveMonth.MonthId);
            ViewData["ReminderId"] = new SelectList(_uow.ReminderRepository.AllAsync(User.GetUserId()).Result, "Id", "Id", reminderActiveMonth.ReminderId);
            return View(reminderActiveMonth);
        }

        // GET: ReminderActiveMonth/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminderActiveMonth = await _uow.ReminderActiveMonthRepository.FindAsync(id.Value);
            if (reminderActiveMonth == null)
            {
                return NotFound();
            }

            return View(reminderActiveMonth);
        }

        // POST: ReminderActiveMonth/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reminderActiveMonth = await _uow.ReminderActiveMonthRepository.FindAsync(id);
            if (reminderActiveMonth != null)
            {
                _uow.ReminderActiveMonthRepository.Remove(reminderActiveMonth);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
