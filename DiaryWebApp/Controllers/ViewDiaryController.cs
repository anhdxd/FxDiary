using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ActionCoreLib.Models;
using ActionCoreLib.DbEntities;

namespace DiaryWebApp.Controllers
{
    public class ViewDiaryController : Controller
    {
        private readonly string dbName = ActionCoreLib.Utilities.GlobalConst.diaryPath;
        // GET: ViewDiaryController
        public ActionResult Index()
        {
           
            IEnumerable<DiaryModel> diaryList;
            using (var context = new DataContext(dbName))
            {
                diaryList = context.Diary.ToList();
            }
            return View(diaryList);
        }

        // GET: ViewDiaryController/Details/id
        public ActionResult Details(int id)
        {
            DiaryModel diaryModel;
            using (var context = new DataContext(dbName))
            {
                diaryModel = context.Diary.Find(id)!;
            }

            return View(diaryModel);
        }

        // GET: ViewDiaryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViewDiaryController/Create
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

        // GET: ViewDiaryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ViewDiaryController/Edit/5
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

        // GET: ViewDiaryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ViewDiaryController/Delete/5
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
