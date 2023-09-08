using Microsoft.AspNetCore.Mvc;
using DAL.Contracts.App;
using Domain;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class ReminderTypeController : Controller
    {
        private readonly IAppUOW _uow;
        
        public ReminderTypeController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: ReminderType
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.ReminderTypeRepository.AllAsync();
            return View(vm);
        }

        // GET: ReminderType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminderType = await _uow.ReminderTypeRepository.FindAsync(id.Value);
            if (reminderType == null)
            {
                return NotFound();
            }

            return View(reminderType);
        }

        // GET: ReminderType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReminderType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReminderTypeName,Id")] ReminderType reminderType)
        {
            if (ModelState.IsValid)
            {
                reminderType.Id = Guid.NewGuid();
                _uow.ReminderTypeRepository.Add(reminderType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reminderType);
        }

        // GET: ReminderType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminderType = await _uow.ReminderTypeRepository.FindAsync(id.Value);
            if (reminderType == null)
            {
                return NotFound();
            }
            return View(reminderType);
        }

        // POST: ReminderType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ReminderTypeName,Id")] ReminderType reminderType)
        {
            if (id != reminderType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.ReminderTypeRepository.Update(reminderType);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(reminderType);
        }

        // GET: ReminderType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reminderType = await _uow.ReminderTypeRepository.FindAsync(id.Value);
            if (reminderType == null)
            {
                return NotFound();
            }

            return View(reminderType);
        }

        // POST: ReminderType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var reminderType = await _uow.ReminderTypeRepository.FindAsync(id);
            if (reminderType != null)
            {
                _uow.ReminderTypeRepository.Remove(reminderType);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
