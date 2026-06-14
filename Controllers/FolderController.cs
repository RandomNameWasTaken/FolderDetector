using Microsoft.AspNetCore.Mvc;
using FolderDetector.Models;
using FolderDetector.Store;

namespace FolderDetector.Controllers
{
    public class FolderController : Controller
    {
        private readonly IFolderStore _store;

        public FolderController(IFolderStore store)
        {
            _store = store;
        }

        public IActionResult Index(string folderPath)
        {
             FolderModel folder;

            if (!_store.TryGet(folderPath, out folder))
            {
                folder = new FolderModel(folderPath);
                _store.Set(folderPath, folder);
            }

            return View(folder);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Analyse(string folderPath)
        {
            if (_store.TryGet(folderPath, out var folder))
            {
                folder.AnalyseFiles();
                return RedirectToAction("Index", new { folderPath });
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoadAnotherFolder()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}