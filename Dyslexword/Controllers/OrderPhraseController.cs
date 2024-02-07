using Dyslexword.Common;
using Dyslexword.Data;
using Dyslexword.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Dyslexword.Controllers
{
    public class OrderPhraseController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderPhrase
        public ActionResult Index(string frase)
        {
            var levelData = db.Levels.ToList();
            var orderPhraseData = db.OrderPhrases.ToList();
            if (!string.IsNullOrEmpty(frase))
            {
                frase = frase.ToUpper();
                orderPhraseData = orderPhraseData.Where(u => u.Phrase.Contains(frase)).ToList();
            }

            CombinedViewModelPhrase viewModel = new CombinedViewModelPhrase
            {
                level = levelData,
                orderPhrase = orderPhraseData
            };

            return View(viewModel);
        }

        // GET: OrderPhrase/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPhrase orderPhrase = db.OrderPhrases.Find(id);
            if (orderPhrase == null)
            {
                return HttpNotFound();
            }
            return View(orderPhrase);
        }

        // GET: OrderPhrase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderPhrase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Phrase")] OrderPhrase orderPhrase)
        {
            if (ModelState.IsValid)
            {
                db.OrderPhrases.Add(orderPhrase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderPhrase);
        }

        // GET: OrderPhrase/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPhrase orderPhrase = db.OrderPhrases.Find(id);
            if (orderPhrase == null)
            {
                return HttpNotFound();
            }
            return View(orderPhrase);
        }

        // POST: OrderPhrase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Phrase")] OrderPhrase orderPhrase)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderPhrase).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderPhrase);
        }

        // GET: OrderPhrase/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPhrase orderPhrase = db.OrderPhrases.Find(id);
            if (orderPhrase == null)
            {
                return HttpNotFound();
            }
            return View(orderPhrase);
        }

        // POST: OrderPhrase/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderPhrase orderPhrase = db.OrderPhrases.Find(id);
            db.OrderPhrases.Remove(orderPhrase);
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

        // Jugar un nuevo nivel
        public ActionResult Jugar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPhrase orderPhrase = db.OrderPhrases.Find(id);
            if (orderPhrase == null)
            {
                return HttpNotFound();
            }
            return View(orderPhrase);
        }

        // Comprobar que el nivel esta accertado
        [HttpPost]
        public ActionResult Acertado(string UserId, string IdGame)
        {
            Level level = db.Levels.First(l => l.UserId == UserId);
            if (level != null)
            {
                level.OrderPhraseId = 1 + int.Parse(IdGame);
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

        // Pasar al siguiente nivel
        [HttpPost]
        public ActionResult PasarNivel(string UserId, string IdGame)
        {
            Level level = db.Levels.First(l => l.UserId == UserId);
            if (level != null)
            {
                level.OrderPhraseId = 1 + int.Parse(IdGame);
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

        // Repetir nivel
        public ActionResult Repetir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderPhrase orderPhrase = db.OrderPhrases.Find(id);
            if (orderPhrase == null)
            {
                return HttpNotFound();
            }
            return View(orderPhrase);
        }

        public ActionResult Inicio()
        {
            return View();
        }
    }
}
