using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Contracts.App;
using Domain;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class HistoryEntryController : Controller
    {
        private readonly IAppUOW _uow;
        
        public HistoryEntryController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: HistoryEntry
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.HistoryEntryRepository.AllAsync();
            return View(vm);
        }

        // GET: HistoryEntry/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyEntry = await _uow.HistoryEntryRepository.FindAsync(id.Value);
            
            if (historyEntry == null)
            {
                return NotFound();
            }

            return View(historyEntry);
        }

        // GET: HistoryEntry/Create
        public IActionResult Create()
        {
            ViewData["HistoryEntryTypeId"] = new SelectList(_uow.HistoryEntryTypeRepository.AllAsync().Result, "Id", "EntryTypeName");
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync().Result, "Id", "PlantName");
            return View();
        }

        // POST: HistoryEntry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntryComment,EntryTime,HistoryEntryTypeId,PlantId,Id")] HistoryEntry historyEntry)
        {
            if (ModelState.IsValid)
            {
                historyEntry.Id = Guid.NewGuid();
                _uow.HistoryEntryRepository.Add(historyEntry);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HistoryEntryTypeId"] = new SelectList(_uow.HistoryEntryTypeRepository.AllAsync().Result, "Id", "EntryTypeName", historyEntry.HistoryEntryTypeId);
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync().Result, "Id", "PlantName", historyEntry.PlantId);
            return View(historyEntry);
        }

        // GET: HistoryEntry/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyEntry = await _uow.HistoryEntryRepository.FindAsync(id.Value);
            if (historyEntry == null)
            {
                return NotFound();
            }
            ViewData["HistoryEntryTypeId"] = new SelectList(_uow.HistoryEntryTypeRepository.AllAsync().Result, "Id", "EntryTypeName", historyEntry.HistoryEntryTypeId);
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync().Result, "Id", "PlantName", historyEntry.PlantId);

            return View(historyEntry);
        }

        // POST: HistoryEntry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EntryComment,EntryTime,HistoryEntryTypeId,PlantId,Id")] HistoryEntry historyEntry)
        {
            if (id != historyEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.HistoryEntryRepository.Update(historyEntry);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["HistoryEntryTypeId"] = new SelectList(_uow.HistoryEntryTypeRepository.AllAsync().Result, "Id", "EntryTypeName", historyEntry.HistoryEntryTypeId);
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync().Result, "Id", "PlantName", historyEntry.PlantId);

            return View(historyEntry);
        }

        // GET: HistoryEntry/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyEntry = await _uow.HistoryEntryRepository.FindAsync(id.Value);
            if (historyEntry == null)
            {
                return NotFound();
            }

            return View(historyEntry);
        }

        // POST: HistoryEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var historyEntry = await _uow.HistoryEntryRepository.FindAsync(id);
            if (historyEntry != null)
            {
                _uow.HistoryEntryRepository.Remove(historyEntry);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
