using FullAdoProject.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;

namespace FullAdoProject.Controllers
{
    public class StudentController : Controller
    {
        private readonly SqlConnection con;

        public StudentController()
        {
            con = new SqlConnection(@"Data Source=DANISH\SQLEXPRESS; Database=FullAdoProject; Integrated Security=true");
            con.Open();
        }

        [HttpGet]
        public IActionResult ReadAllData()
        {
            List<Students> std = new List<Students>();
            string query = "select * from Students";
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                std.Add(new Students { Id = Convert.ToInt32(dr["Id"]), Name = Convert.ToString(dr["Name"]), Address = Convert.ToString(dr["Address"]) });
            }
            
            return View(std);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(); 
        }

        [HttpPost]
        public IActionResult Create(Students std)
        {
            string query = "insert into Students values('" + std.Id + "', '" + std.Name + "', '" + std.Address + "' )";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("ReadAllData");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            Students std = new Students();
            string query = "select * from Students where Id = '"+ id + "' ";
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                std.Id = Convert.ToInt32(dr["Id"]);
                std.Name = Convert.ToString(dr["Name"]);
                std.Address = Convert.ToString(dr["Address"]);
            }
            return View(std);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Students std = new Students();
            string query = "select * from Students where Id = '" + id + "' ";
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                std.Id = Convert.ToInt32(dr["Id"]);
                std.Name = Convert.ToString(dr["Name"]);
                std.Address = Convert.ToString(dr["Address"]);
            }
            return View(std);
        }

        [HttpPost]
        public IActionResult Delete(Students std, int id)
        {
            string query = "Delete from Students where Id = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("ReadAllData");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Students std = new Students();
            string query = "select * from Students where Id = '" + id + "' ";
            SqlDataAdapter adp = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                std.Id = Convert.ToInt32(dr["Id"]);
                std.Name = Convert.ToString(dr["Name"]);
                std.Address = Convert.ToString(dr["Address"]);
            }
            return View(std);
            
        }

        [HttpPost]
        public IActionResult Edit(Students std, int id)
        {
            string query = "update Students set Name = '"+std.Name+"', Address = '"+std.Address+"'  where Id = '"+id+"' ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("ReadAllData");
        }
    }
}
