using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DAL.Contracts.App;
using Domain;
using Helpers;
using Helpers.Base;
#pragma warning disable 1591
namespace WebApp.Controllers
{
    public class PlantInCollectionController : Controller
    {
        private readonly IAppUOW _uow;
        
        public PlantInCollectionController(IAppUOW uow)
        {
            _uow = uow;
        }

        // GET: PlantInCollection
        public async Task<IActionResult> Index()
        {
            var vm = await _uow.PlantInCollectionRepository.AllAsync();
            return View(vm);
        }

        // GET: PlantInCollection/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantInCollection = await _uow.PlantInCollectionRepository.FindAsync(id.Value);
            if (plantInCollection == null)
            {
                return NotFound();
            }

            return View(plantInCollection);
        }

        // GET: PlantInCollection/Create
        public IActionResult Create()
        {
            ViewData["PlantId"] = new SelectList(_uow.PlantRepository.AllAsync(User.GetUserId()).Result, "Id", "PlantName");
            ViewData["PlantCollectionId"] = new SelectList(_uow.PlantCollectionRepository.AllAsync(User.GetUserId()).Result, "Id", "CollectionName");
            return View();
        }

        // POST: PlantInCollection/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlantCollectionId,PlantId,Id")] PlantInCollection plantInCollection)
        {
            if (ModelState.IsValid)
            {
                plantInCollection.Id = Guid.NewGuid();
                _uow.PlantInCollectionRepository.Add(plantInCollection);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantId"] = new SelectList(
                _uow.PlantRepository.AllAsync(User.GetUserId()).Result, "Id", "PlantName", plantInCollection.PlantId);
            ViewData["PlantCollectionId"] = new SelectList(
                _uow.PlantCollectionRepository.AllAsync(User.GetUserId()).Result, "Id", "CollectionName", plantInCollection.PlantCollectionId);
            
            return View(plantInCollection);
        }

        // GET: PlantInCollection/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantInCollection = await _uow.PlantInCollectionRepository.FindAsync(id.Value);
            if (plantInCollection == null)
            {
                return NotFound();
            }
            ViewData["PlantId"] = new SelectList(
                _uow.PlantRepository.AllAsync(User.GetUserId()).Result, "Id", "PlantName", plantInCollection.PlantId);
            ViewData["PlantCollectionId"] = new SelectList(
                _uow.PlantCollectionRepository.AllAsync(User.GetUserId()).Result, "Id", "CollectionName", plantInCollection.PlantCollectionId);
            return View(plantInCollection);
        }

        // POST: PlantInCollection/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PlantCollectionId,PlantId,Id")] PlantInCollection plantInCollection)
        {
            if (id != plantInCollection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _uow.PlantInCollectionRepository.Update(plantInCollection);
                await _uow.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlantId"] = new SelectList(
                _uow.PlantRepository.AllAsync(User.GetUserId()).Result, "Id", "PlantName", plantInCollection.PlantId);
            ViewData["PlantCollectionId"] = new SelectList(
                _uow.PlantCollectionRepository.AllAsync(User.GetUserId()).Result, "Id", "CollectionName", plantInCollection.PlantCollectionId);
            return View(plantInCollection);
        }

        // GET: PlantInCollection/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plantInCollection = await _uow.PlantInCollectionRepository.FindAsync(id.Value);
            if (plantInCollection == null)
            {
                return NotFound();
            }

            return View(plantInCollection);
        }

        // POST: PlantInCollection/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var plantInCollection = await _uow.PlantInCollectionRepository.FindAsync(id);
            if (plantInCollection != null)
            {
                _uow.PlantInCollectionRepository.Remove(plantInCollection);
            }
            
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
