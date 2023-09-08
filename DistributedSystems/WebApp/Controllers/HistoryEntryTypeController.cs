using Microsoft.AspNetCore.Mvc;
using DAL.Contracts.App;
using Domain;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class HistoryEntryTypeController : Controller
    {

        private readonly IAppUOW _uow;

        public HistoryEntryTypeController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: HistoryEntryType
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.HistoryEntryTypeRepository.AllAsync();
            return View(vm);
        }

        // GET: HistoryEntryType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyEntryType = await _uow.HistoryEntryTypeRepository.FindAsync(id.Value);
            if (historyEntryType == null)
            {
                return NotFound();
            }

            return View(historyEntryType);
        }

        // GET: HistoryEntryType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HistoryEntryType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EntryTypeName,Id")] HistoryEntryType historyEntryType)
        {
            if (ModelState.IsValid)
            {
                historyEntryType.Id = Guid.NewGuid();
                _uow.HistoryEntryTypeRepository.Add(historyEntryType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(historyEntryType);
        }

        // GET: HistoryEntryType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyEntryType = await _uow.HistoryEntryTypeRepository.FindAsync(id.Value);
            if (historyEntryType == null)
            {
                return NotFound();
            }
            return View(historyEntryType);
        }

        // POST: HistoryEntryType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EntryTypeName,Id")] HistoryEntryType historyEntryType)
        {
            if (id != historyEntryType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.HistoryEntryTypeRepository.Update(historyEntryType);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(historyEntryType);
        }

        // GET: HistoryEntryType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historyEntryType = await _uow.HistoryEntryTypeRepository.FindAsync(id.Value);
            if (historyEntryType == null)
            {
                return NotFound();
            }

            return View(historyEntryType);
        }

        // POST: HistoryEntryType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var historyEntryType = await _uow.HistoryEntryTypeRepository.FindAsync(id);
            if (historyEntryType != null)
            {
                _uow.HistoryEntryTypeRepository.Remove(historyEntryType);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
