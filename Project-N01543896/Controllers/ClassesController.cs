using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_N01543896.Models;

namespace Project_N01543896.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Classes/List
        public ActionResult List()
        {
            ClassesDataController controller = new ClassesDataController();
            IEnumerable<Classes> Classes = controller.ListClasses();
            return View(Classes);
        }

        //GET : /Student/Show/{id}
        public ActionResult Show(int id)
        {
            ClassesDataController controller = new ClassesDataController();
            Classes NewClass = controller.FindClass(id);

            return View(NewClass);
        }
    }
}
