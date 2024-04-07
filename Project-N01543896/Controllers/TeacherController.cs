using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        //POST : /Author/Add
        [HttpPost]
        public ActionResult Add(string TeacherFname, string TeacherLname, string EmployeeNumber, decimal Salary) {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("This is the Add teacher method");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);
            Debug.WriteLine(EmployeeNumber);
            Debug.WriteLine(Salary);


            Teacher NewTeacher = new Teacher();
            NewTeacher.teacherFName = TeacherFname;
            NewTeacher.teacherLName = TeacherLname;
            NewTeacher.employeeNumber = EmployeeNumber;
            NewTeacher.salary = Salary;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        //POST : /Teacher/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id) {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET : /Teacher/New
        public ActionResult New() {
            return View();
        }

        //GET : /Author/Ajax_New
        public ActionResult Ajax_New() {
            return View();

        }

        //GET : /Author/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id) {
            TeacherDataController controller = new TeacherDataController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }

    }
}
