using CRUD.Models;
using CRUD.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            List<VistaUsuario> lista = new List<VistaUsuario>();
            //instance conn
            using (Models.DBPRUEBAEntities1 db = 
                new Models.DBPRUEBAEntities1() )
            {
                //usiing linq
                 lista =
                    (from d in db.USUARIO
                     orderby d.id descending
                     select new VistaUsuario
                     {
                         Id = d.id,
                         Name = d.nombre,
                         Apellido = d.apellido,
                         Dni = d.dni
                     }).ToList();
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult Editar()
        {
            return View();
        }

        public ActionResult Buscar(string dni)
        {
                using(DBPRUEBAEntities1 db = new DBPRUEBAEntities1())
                {
                    VistaUsuario user =
                     db.USUARIO.Where(val => val.dni == dni).Select(val => new VistaUsuario()
                    {
                        Id = val.id,
                        Name = val.nombre,
                        Apellido = val.apellido,
                        Dni = val.dni
                    }).FirstOrDefault();

                return Json(user, JsonRequestBehavior.AllowGet);

            }
            
        }

        public ActionResult Nuevo(VistaUsuario user)
        {
            try
            {
                using(DBPRUEBAEntities1 db = new DBPRUEBAEntities1())
                {
                    var oPeople = new USUARIO();
                    oPeople.nombre = user.Name;
                    oPeople.apellido = user.Apellido;
                    oPeople.dni = user.Dni;
                    db.USUARIO.Add(oPeople);
                    db.SaveChanges();  
                }

                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
           
        }
    }
}