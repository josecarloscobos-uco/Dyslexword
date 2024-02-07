using Dyslexword.Common;
using Dyslexword.Data;
using Dyslexword.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dyslexword.Controllers
{
    public class AlumnoTutorController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AlumnoTutor
        public ActionResult Index()
        {
            var levelData = db.Levels.ToList();
            var alumnotutorData = db.AlumnoTutors.ToList();
            var usersData=db.Users.ToList();

            CombinedViewModelUser viewModel = new CombinedViewModelUser
            {
                level = levelData,
                alumnotutors = alumnotutorData,
                applicationUsers = usersData


            };

            return View(viewModel);
        }

        // GET: AlumnoTutor/Details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlumnoTutor alumnoTutor = db.AlumnoTutors.Find(id);
            if (alumnoTutor == null)
            {
                return HttpNotFound();
            }
            return View(alumnoTutor);
        }

        // GET: AlumnoTutor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AlumnoTutor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserAlumnoId,UserTutorId")] AlumnoTutor alumnoTutor)
        {
            if (ModelState.IsValid)
            {
                db.AlumnoTutors.Add(alumnoTutor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(alumnoTutor);
        }

        // GET: AlumnoTutor/Edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlumnoTutor alumnoTutor = db.AlumnoTutors.Find(id);
            if (alumnoTutor == null)
            {
                return HttpNotFound();
            }
            return View(alumnoTutor);
        }

        // POST: AlumnoTutor/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserAlumnoId,UserTutorId")] AlumnoTutor alumnoTutor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alumnoTutor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(alumnoTutor);
        }

        // GET: AlumnoTutor/Delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlumnoTutor alumnoTutor = db.AlumnoTutors.Find(id);
            if (alumnoTutor == null)
            {
                return HttpNotFound();
            }
            return View(alumnoTutor);
        }

        // POST: AlumnoTutor/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlumnoTutor alumnoTutor = db.AlumnoTutors.Find(id);
            db.AlumnoTutors.Remove(alumnoTutor);
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
        public ActionResult Calificaciones(int id)
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

        
        [AllowAnonymous]
        public ActionResult LoginTutor(int? selected = null)
        {
            return View();
        }
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginTutor(string correo, string IduserAlumno)
        {
            
            var usuario = db.Users.FirstOrDefault(u => u.Email == correo);
            if (usuario == null)
            {
                ViewBag.ErrorMessage = "El correo electrónico proporcionado no está asociado a ningún usuario en nuestros registros.";
                return View();
            }

            ApplicationUser applicationUser = db.Users.Find(usuario.Id);
            var rolesUsuario = db.Set<IdentityUserRole>().Where(ur => ur.UserId == usuario.Id).ToList();
            var code = rolesUsuario.FirstOrDefault();
            var idRole = code.RoleId;
            if (idRole == "24bac441-3497-48ba-a2ba-64a3347a9ec7")
            {
                AlumnoTutor alumnotutor = new AlumnoTutor();
                alumnotutor.UserAlumnoId = IduserAlumno;
                alumnotutor.UserTutorId = usuario.Id;
                var _alumnoTutor = new AlumnoTutorController();
                _alumnoTutor.Create(alumnotutor);

                return RedirectToAction("Index", "Manage");
            }
            else
            {
                ViewBag.ErrorMessage = "El correo electrónico proporcionado no está vinculado a ningún tutor registrado en nuestro sistema.";
                return View();
            }

            return View();
        }


    }
}