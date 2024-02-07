using Dyslexword.Common;
using Dyslexword.Data;
using Dyslexword.Models;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Dyslexword.Controllers
{
    public class OrderWordController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderWord
        public ActionResult Index(string palabra)
        {
            var levelData = db.Levels.ToList();
            var orderwordData = db.OrderWords.ToList();
            if (!string.IsNullOrEmpty(palabra))
            {
                palabra = palabra.ToUpper();
                orderwordData = orderwordData.Where(u => u.Word.Contains(palabra)).ToList();
            }

            CombinedViewModelWord viewModel = new CombinedViewModelWord
            {
               level = levelData,
               orderWord = orderwordData
            };

            return View(viewModel);
        }

        // GET: OrderWord/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderWord orderWord = db.OrderWords.Find(id);
            if (orderWord == null)
            {
                return HttpNotFound();
            }
            return View(orderWord);
        }

        // GET: OrderWord/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderWord/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Word,Image")] OrderWord orderWord)
        {

            HttpPostedFileBase fileBase = Request.Files[0];


            if (fileBase != null && fileBase.ContentLength > 0)
            {
                WebImage image = new WebImage(fileBase.InputStream);
                orderWord.Image = image.GetBytes(); 

            }

                db.OrderWords.Add(orderWord);
                db.SaveChanges();
                return RedirectToAction("Index");
        }

        // GET: OrderWord/Edit/
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderWord orderWord = db.OrderWords.Find(id);
            if (orderWord == null)
            {
                return HttpNotFound();
            }
            return View(orderWord);
        }

        // POST: OrderWord/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Word,Image")] OrderWord orderWord)
        {

            byte[] imagenAcual = null;

            HttpPostedFileBase fileBase = Request.Files[0];

            if (fileBase.ContentLength == 0)
            {
                imagenAcual = db.OrderWords.Find(orderWord.Id).Image;
                orderWord.Image = imagenAcual;
            }
            else
            {
                WebImage image = new WebImage(fileBase.InputStream);
                orderWord.Image = image.GetBytes();
            }

            var existingOrderWord = db.OrderWords.FirstOrDefault(o => o.Id == orderWord.Id);

            if (existingOrderWord != null)
            {

                existingOrderWord.Word = orderWord.Word;
                existingOrderWord.Image = orderWord.Image;

                db.SaveChanges(); 
            }
            
            return RedirectToAction("Index");
        }

        // GET: OrderWord/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderWord orderWord = db.OrderWords.Find(id);
            if (orderWord == null)
            {
                return HttpNotFound();
            }
            return View(orderWord);
        }

        // POST: OrderWord/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderWord orderWord = db.OrderWords.Find(id);
            db.OrderWords.Remove(orderWord);
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

        public ActionResult Jugar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderWord orderWord = db.OrderWords.Find(id);
            if (orderWord == null)
            {
                return HttpNotFound();
            }
            return View(orderWord);
        }

        // Repetir nivel
        public ActionResult Repetir(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderWord orderWord = db.OrderWords.Find(id);
            if (orderWord == null)
            {
                return HttpNotFound();
            }
            return View(orderWord);
        }

        // Comprobar acierto nivel
        [HttpPost]
        public ActionResult Acertado(string UserId, string IdGame)
        {
            Level level = db.Levels.First( l => l.UserId == UserId);
            if (level != null)
            {
                level.OrderWordId = 1 + int.Parse(IdGame);
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


        // Obtener imagen 
        public ActionResult getImage(int id)
        {
            OrderWord orderWord = db.OrderWords.Find(id);
            byte[] byteImage = orderWord.Image;

            MemoryStream memoryStream = new MemoryStream(byteImage);

            System.Drawing.Image image = System.Drawing.Image.FromStream(memoryStream);

            memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            memoryStream.Position = 0;

            return File(memoryStream, "image/jpg");
        }

        public ActionResult Inicio()
        {
            return View();
        }

        
        // Pasar al siguiente nivel
        [HttpPost]
        public ActionResult PasarNivel(string UserId, string IdGame)
        {
            Level level = db.Levels.First(l => l.UserId == UserId);
            if (level != null)
            {
                level.OrderWordId = 1 + int.Parse(IdGame);
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
