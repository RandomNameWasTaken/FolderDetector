using Microsoft.AspNetCore.Mvc;
using FolderDetector.Models;

namespace FolderDetector.Controllers
{
    public class FileErrorController : Controller
    {
        public IActionResult Index(FileErrorModel fileErrorModel)
        {
            return View(fileErrorModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BackToMainPage(IEnumerable<string> loadedFolders)
        {
            return RedirectToAction("Index", "Home", new { loadedFolders });
        }
    }
}
