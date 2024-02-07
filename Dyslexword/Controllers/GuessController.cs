using Dyslexword.Common;
using Dyslexword.Data;
using Dyslexword.Models;
using Microsoft.AspNet.Identity;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Dyslexword.Controllers
{
    public class GuessController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Guess
        public ActionResult Index(string palabra)
        {
            var levelData = db.Levels.ToList();
            var guessData = db.Guesses.ToList();
            if (!string.IsNullOrEmpty(palabra))
            {
                palabra = palabra.ToUpper();
                guessData = guessData.Where(u => u.Word.Contains(palabra)).ToList();
            }

            CombinedViewModelGuess viewModel = new CombinedViewModelGuess
            {         
                level = levelData,
                guess = guessData
            };

            return View(viewModel);
        }

        // GET: Guess/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guess guess = db.Guesses.Find(id);
            if (guess == null)
            {
                return HttpNotFound();
            }
            return View(guess);
        }

        // GET: Guess/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guess/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Word,Image")] Guess guess)
        {
            HttpPostedFileBase fileBase = Request.Files[0];


            if (fileBase != null && fileBase.ContentLength > 0)
            {
                WebImage image = new WebImage(fileBase.InputStream);
                guess.Image = image.GetBytes();

            }

            db.Guesses.Add(guess);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Guess/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guess guess = db.Guesses.Find(id);
            if (guess == null)
            {
                return HttpNotFound();
            }
            return View(guess);
        }

        // POST: Guess/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Word,Image")] Guess guess)
        {
            byte[] imagenAcual = null;

            HttpPostedFileBase fileBase = Request.Files[0];

            if (fileBase.ContentLength == 0)
            {
                imagenAcual = db.Guesses.Find(guess.Id).Image;
                guess.Image = imagenAcual;
            }
            else
            {
                WebImage image = new WebImage(fileBase.InputStream);
                guess.Image = image.GetBytes();
            }

            var existingOrderWord = db.Guesses.FirstOrDefault(o => o.Id == guess.Id);

            if (existingOrderWord != null)
            {

                existingOrderWord.Word = guess.Word;
                existingOrderWord.Image = guess.Image;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Guess/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guess guess = db.Guesses.Find(id);
            if (guess == null)
            {
                return HttpNotFound();
            }
            return View(guess);
        }

        // POST: Guess/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guess guess = db.Guesses.Find(id);
            db.Guesses.Remove(guess);
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

        // jugar un nuevo nivel
        public ActionResult Jugar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guess guess = db.Guesses.Find(id);
            if (guess == null)
            {
                return HttpNotFound();
            }
            return View(guess);
        }

        //Mostrar imagen
        public ActionResult getImage(int id)
        {
            Guess guess = db.Guesses.Find(id);
            byte[] byteImage = guess.Image;

            MemoryStream memoryStream = new MemoryStream(byteImage);

            System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
        }

        //Comprobra que un nivel esta acertado
        [HttpPost]
        public ActionResult Acertado(string UserId, string IdGame)
        {
            Level level = db.Levels.First(l => l.UserId == UserId);
            if (level != null)
            {
                level.GuessId = 1 + int.Parse(IdGame);
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

        // Pasar a un nuevo nivel 
        [HttpPost]
        public ActionResult PasarNivel(string UserId, string IdGame)
        {
            Level level = db.Levels.First(l => l.UserId == UserId);
            if (level != null)
            {
                level.GuessId = 1 + int.Parse(IdGame);
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

        //Repetir un nivel
        public ActionResult Repetir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guess guess = db.Guesses.Find(id);
            if (guess == null)
            {
                return HttpNotFound();
            }
            return View(guess);
        }

        public ActionResult Inicio()
        {
            return View();
        }
    }
}
