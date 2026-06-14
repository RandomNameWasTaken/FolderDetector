using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using FolderDetector.Models;
using FolderDetector.Store;

namespace FolderDetector.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFolderStore _store;

        public HomeController(IFolderStore store)
        {
            _store = store;
        }

        public IActionResult Index()
        {
            return View(_store.GetAll());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GoToFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                return RedirectToAction("Index", "Folder", new { folderPath });
            }

            return RedirectToAction("Index", "FileError", new FileErrorModel { ErrorMessage = "Folder not found" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}