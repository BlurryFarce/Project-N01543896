using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project_N01543896.Models;
using Project_N01543896.ViewModels;

namespace Project_N01543896.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        //GET : /Teacher/List
        public ActionResult List(string searchKey = null, decimal? salaryKey = null)
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers(searchKey);
            if (!string.IsNullOrEmpty(searchKey))
            {
                Teachers = controller.ListTeachers(searchKey);
            }
            else if (salaryKey.HasValue)
            {
                Teachers = controller.ListTeachersBySalary(salaryKey.Value);
            }
            return View(Teachers);
        }

        //GET : /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);

            ClassesDataController classesDataController = new ClassesDataController();
            IEnumerable<Classes> NewClassList = controller.FindCLassesByTeacher(id);

            TeacherClassViewData ViewModel = new TeacherClassViewData();
            ViewModel.teacher = NewTeacher;
            ViewModel.classes = NewClassList;

            return View(ViewModel);
        }
    }
}
