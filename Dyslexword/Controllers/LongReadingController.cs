using Dyslexword.Common;
using Dyslexword.Data;
using Dyslexword.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Dyslexword.Controllers
{
    public class LongReadingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: LongReading
        public ActionResult Index(string texto)
        {
            var levelData = db.Levels.ToList();
            var longreadingData = db.LongReadings.ToList();
            if (!string.IsNullOrEmpty(texto))
            {
                texto = texto.ToLower();
                longreadingData = longreadingData.Where(u => u.Text.Contains(texto)).ToList();
            }

            CombinedViewModelLongReading viewModel = new CombinedViewModelLongReading
            {
                level = levelData,
                longReading = longreadingData
            };

            return View(viewModel);
        }

        // GET: LongReading/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LongReading longReading = db.LongReadings.Find(id);
            if (longReading == null)
            {
                return HttpNotFound();
            }
            return View(longReading);
        }

        // GET: LongReading/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LongReading/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Text,Question,CorrectOption,OptionA,OptionB")] LongReading longReading)
        {
            if (ModelState.IsValid)
            {
                db.LongReadings.Add(longReading);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(longReading);
        }

        // GET: LongReading/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LongReading longReading = db.LongReadings.Find(id);
            if (longReading == null)
            {
                return HttpNotFound();
            }
            return View(longReading);
        }

        // POST: LongReading/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Text,Question,CorrectOption,OptionA,OptionB")] LongReading longReading)
        {
            if (ModelState.IsValid)
            {
                db.Entry(longReading).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(longReading);
        }

        // GET: LongReading/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LongReading longReading = db.LongReadings.Find(id);
            if (longReading == null)
            {
                return HttpNotFound();
            }
            return View(longReading);
        }

        // POST: LongReading/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LongReading longReading = db.LongReadings.Find(id);
            db.LongReadings.Remove(longReading);
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

        //Jugar un nuevo nivel
        public ActionResult Jugar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LongReading longReading = db.LongReadings.Find(id);
            if (longReading == null)
            {
                return HttpNotFound();
            }
            return View(longReading);
        }

        //Repetir un nivel
        public ActionResult Repetir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LongReading longReading = db.LongReadings.Find(id);
            if (longReading == null)
            {
                return HttpNotFound();
            }
            return View(longReading);
        }

        //Comprobar que el nivel esta acertado
        [HttpPost]
        public ActionResult Acertado(string UserId, string IdGame)
        {
            Level level = db.Levels.First(l => l.UserId == UserId);
            if (level != null)
            {
                level.LongReadingId = 1 + int.Parse(IdGame);
                db.SaveChanges();
            }

            ApplicationUser user = db.Users.First(l => l.Id == UserId);
            if (user != null)
            {
                user.Coins = user.Coins + 100;
                db.SaveChanges();
            }

            if (level == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index");
        }

        public ActionResult Inicio()
        {
            return View();
        }

        //Pasar al siguiente nivel
        [HttpPost]
        public ActionResult PasarNivel(string UserId, string IdGame)
        {
            Level level = db.Levels.First(l => l.UserId == UserId);
            if (level != null)
            {
                level.LongReadingId = 1 + int.Parse(IdGame);
                db.SaveChanges();
            }

            ApplicationUser user = db.Users.First(l => l.Id == UserId);
            if (user != null)
            {
                user.Coins = user.Coins - 400;
                db.SaveChanges();
            }

            if (level == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index");
        }

    }
}
