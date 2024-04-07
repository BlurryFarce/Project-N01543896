using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using Project_N01543896.Models;
using System.Web.Http.Cors;

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
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <example>GET api/TeacherData/ListTeachers/alex</example>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{searchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string searchKey = null)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText =
                "Select * from teachers where lower(teacherfname) like lower(@stringKey) "
                + "or lower(teacherlname) like lower(@stringKey) "
                + "or lower(concat(teacherfname, ' ', teacherlname)) like lower(@stringKey)";

            cmd.Parameters.AddWithValue("@stringKey", "%" + searchKey + "%");

            cmd.Prepare();

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
        /// Returns a list of Teachers that match the specified salary.
        /// </summary>
        /// <param name="salary">The salary to search for.</param>
        /// <returns>
        /// A list of teachers with the specified salary.
        /// </returns>
        /// <example>GET api/TeacherData/ListTeachersBySalary/55.30</example>
        [HttpGet]
        [Route("api/TeacherData/ListTeachersBySalary/{salary}")]
        public IEnumerable<Teacher> ListTeachersBySalary(decimal salary)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from teachers where salary = @salary";
            cmd.Parameters.AddWithValue("@salary", salary);

            cmd.Prepare();

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
        /// <example>GET api/TeacherData/FindTeacher/5</example>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher Teacher = new Teacher();

            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from teachers where teacherid =  @id ";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.Prepare();

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
        /// Finds classes taught by a teacher in the system given a teacher ID
        /// </summary>
        /// <param name="id">The teachers primary key</param>
        /// <returns>A classes object</returns>
        /// <example>GET api/TeacherData/FindCLassesByTeacher/2</example>
        [HttpGet]
        [Route("api/TeacherData/FindCLassesByTeacher/{id}")]
        public IEnumerable<Classes> FindCLassesByTeacher(int id)
        {
            MySqlConnection Conn = School.AccessDatabase();

            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "Select * from classes where teacherid = @id ";

            cmd.Parameters.AddWithValue("@id", id);

            cmd.Prepare();

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


        /// <summary>
        /// Adds a teacher to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the teachers's table.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"AuthorFname":"Vaibhav",
        ///	"AuthorLname":"Baria",
        ///	"EmployeeNumber":"T666",
        ///	"Salary":"60"
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher) {

            


            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(NewTeacher.teacherFName);
 
            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            
            cmd.CommandText = "INSERT INTO teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) values (@TeacherFname,@TeacherLname,@EmployeeNumber, CURRENT_DATE(), @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.teacherFName);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.teacherLName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.employeeNumber);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

        /// <summary>
        /// Deletes a teacher from the Database if the ID of that teacher exists. Does NOT maintain relational integrity.
        /// </summary>
        /// <param name="id">The ID of the Teacher.</param>
        /// <example>POST /api/TeacherData/DeleteDelete/3</example>
        [HttpPost]
        [Route("api/TeacherData/DeleteTeacher/{id}")]
        public void DeleteTeacher(int id) {
            MySqlConnection Conn = School.AccessDatabase();
          
            Conn.Open();

            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();

        }

       
    }
}
