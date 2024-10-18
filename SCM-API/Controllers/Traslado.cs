using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SCM_API.Controllers
{
    public class Traslado : Controller
    {
        // GET: Traslado
        public ActionResult Index()
        {
            return View();
        }

        // GET: Traslado/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Traslado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Traslado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Traslado/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Traslado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Traslado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Traslado/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
