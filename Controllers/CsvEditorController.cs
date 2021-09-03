using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSVEDITOR.Controllers
{
    public class CsvEditorController : Controller
    {
        // GET: CsvEditor
        public ActionResult Index()
        {
            return View();
        }

        // GET: CsvEditor/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CsvEditor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CsvEditor/Create
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

        // GET: CsvEditor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CsvEditor/Edit/5
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

        // GET: CsvEditor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CsvEditor/Delete/5
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
