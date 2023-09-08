using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Contracts.App;
using Domain;
using Helpers;
using Helpers.Base;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class ReminderController : Controller
    {
        private readonly IAppUOW _uow;
        
        public ReminderController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: Reminder
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.ReminderRepository.AllAsync(User.GetUserId());
            return View(vm);
        }

        // GET: Reminder/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _uow.ReminderRepository.FindAsync(id.Value);
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

        // GET: Reminder/Create
        public IActionResult Create()
        {
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync(User.GetUserId()).Result, "Id", "PlantName");
            ViewData["ReminderTypeId"] = new SelectList(_uow.ReminderTypeRepository.AllAsync().Result, "Id", "ReminderTypeName");
            return View();
        }

        // POST: Reminder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReminderFrequency,ReminderMessage,PlantId,ReminderTypeId,AppUserId,Id")] Reminder reminder)
        {
            reminder.AppUserId = User.GetUserId();
            if (ModelState.IsValid)
            {
                reminder.Id = Guid.NewGuid();
                _uow.ReminderRepository.Add(reminder);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync(User.GetUserId()).Result, "Id", "PlantName", reminder.PlantId);
            ViewData["ReminderTypeId"] = new SelectList(_uow.ReminderTypeRepository.AllAsync().Result, "Id", "ReminderTypeName", reminder.ReminderTypeId);
            return View(reminder);
        }

        // GET: Reminder/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _uow.ReminderRepository.FindAsync(id.Value);
            if (reminder == null)
            {
                return NotFound();
            }
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync(User.GetUserId()).Result, "Id", "PlantName", reminder.PlantId);
            ViewData["ReminderTypeId"] = new SelectList(_uow.ReminderTypeRepository.AllAsync().Result, "Id", "ReminderTypeName", reminder.ReminderTypeId);
            return View(reminder);
        }

        // POST: Reminder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ReminderFrequency,ReminderMessage,PlantId,ReminderTypeId,AppUserId,Id")] Reminder reminder)
        {
            reminder.AppUserId = User.GetUserId();
            if (id != reminder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.ReminderRepository.Update(reminder);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync(User.GetUserId()).Result, "Id", "PlantName", reminder.PlantId);
            ViewData["ReminderTypeId"] = new SelectList(_uow.ReminderTypeRepository.AllAsync().Result, "Id", "ReminderTypeName", reminder.ReminderTypeId);
            return View(reminder);
        }

        // GET: Reminder/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminder = await _uow.ReminderRepository.FindAsync(id.Value);
            
            if (reminder == null)
            {
                return NotFound();
            }

            return View(reminder);
        }

        // POST: Reminder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reminder = await _uow.ReminderRepository.FindAsync(id);
            if (reminder != null)
            {
                _uow.ReminderRepository.Remove(reminder);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
