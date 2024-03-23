using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using MySql.Data.MySqlClient;
using Project_N01543896.Models;

namespace Project_N01543896.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <returns>
        /// A list of teachers (first names and last names)
        /// </returns>
        /// /// <example>GET api/TeacherData/ListTeachers</example>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]
        public IEnumerable<Teacher> ListTeachers()
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from teachers";

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> TeachersList = new List<Teacher> { };

            while (ResultSet.Read())
            {
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                Teacher TeacherInstance = new Teacher();

                TeacherInstance.teacherId = TeacherId;
                TeacherInstance.teacherFName = TeacherFname;
                TeacherInstance.teacherLName = TeacherLname;
                TeacherInstance.employeeNumber = EmployeeNumber;
                TeacherInstance.hireDate = HireDate;
                TeacherInstance.salary = Salary;

                TeachersList.Add(TeacherInstance);
            }

            Conn.Close();

            return TeachersList;
        }

        /// <summary>
        /// Finds a teacher in the system given an ID
        /// </summary>
        /// <param name="id">The teachers primary key</param>
        /// <returns>A teacher object</returns>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher Teacher = new Teacher();

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from teachers where teacherid = " + id;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string TeacherFname = ResultSet["teacherfname"].ToString();
                string TeacherLname = ResultSet["teacherlname"].ToString();
                string EmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime HireDate = Convert.ToDateTime(ResultSet["hiredate"]);
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                Teacher.teacherId = TeacherId;
                Teacher.teacherFName = TeacherFname;
                Teacher.teacherLName = TeacherLname;
                Teacher.employeeNumber = EmployeeNumber;
                Teacher.hireDate = HireDate;
                Teacher.salary = Salary;
            }

            return Teacher;
        }

        /// <summary>
        /// Finds classes taught by a teache in the system given a teacher ID
        /// </summary>
        /// <param name="id">The teachers primary key</param>
        /// <returns>A classes object</returns>
        [HttpGet]
        [Route("api/TeacherData/FindCLassesByTeacher/{id}")]
        public IEnumerable<Classes> FindCLassesByTeacher(int id)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from classes where teacherid = " + id;

            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Classes> ClassesList = new List<Classes> { };

            while (ResultSet.Read())
            {
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = ResultSet["classcode"].ToString();
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string ClassName = ResultSet["classname"].ToString();
                DateTime StartDate = Convert.ToDateTime(ResultSet["startdate"]);
                DateTime FinishDate = Convert.ToDateTime(ResultSet["finishdate"]);

                Classes Class = new Classes();

                Class.classId = ClassId;
                Class.classCode = ClassCode;
                Class.teacherId = TeacherId;
                Class.className = ClassName;
                Class.startDate = StartDate;
                Class.finishDate = FinishDate;

                ClassesList.Add(Class);
            }

            Conn.Close();

            return ClassesList;
        }
    }
}
