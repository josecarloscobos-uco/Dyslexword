using Dyslexword.Data;
using Dyslexword.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Dyslexword.Controllers
{
    public class LevelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Level
        public ActionResult Index()
        {
            var levels = db.Levels.Include(l => l.OrderPhrase).Include(l => l.OrderWord);
            return View(levels.ToList());
        }

        // GET: Level/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Level level = db.Levels.Find(id);
            if (level == null)
            {
                return HttpNotFound();
            }
            return View(level);
        }

        // GET: Level/Create
        public ActionResult Create()
        {
            ViewBag.OrderPhraseId = new SelectList(db.OrderPhrases, "Id", "Phrase");
            ViewBag.OrderWordId = new SelectList(db.OrderWords, "Id", "Word");
            return View();
        }

        // POST: Level/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,OrderWordId,OrderPhraseId")] Level level)
        {
            if (ModelState.IsValid)
            {
                db.Levels.Add(level);
                db.SaveChanges();
            }

            ViewBag.OrderPhraseId = new SelectList(db.OrderPhrases, "Id", "Phrase", level.OrderPhraseId);
            ViewBag.OrderWordId = new SelectList(db.OrderWords, "Id", "Word", level.OrderWordId);
            return View(level);
        }

        // GET: Level/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Level level = db.Levels.Find(id);
            if (level == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderPhraseId = new SelectList(db.OrderPhrases, "Id", "Phrase", level.OrderPhraseId);
            ViewBag.OrderWordId = new SelectList(db.OrderWords, "Id", "Word", level.OrderWordId);
            return View(level);
        }

        // POST: Level/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,OrderWordId,OrderPhraseId")] Level level)
        {
            if (ModelState.IsValid)
            {
                db.Entry(level).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderPhraseId = new SelectList(db.OrderPhrases, "Id", "Phrase", level.OrderPhraseId);
            ViewBag.OrderWordId = new SelectList(db.OrderWords, "Id", "Word", level.OrderWordId);
            return View(level);
        }

        // GET: Level/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Level level = db.Levels.Find(id);
            if (level == null)
            {
                return HttpNotFound();
            }
            return View(level);
        }

        // POST: Level/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Level level = db.Levels.Find(id);
            db.Levels.Remove(level);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
