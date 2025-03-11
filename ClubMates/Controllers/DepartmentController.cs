using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClubMates.Db;
using ClubMates.Models;

namespace ClubMates.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department

        private ApplicationDbContext Db = new ApplicationDbContext();
        public ActionResult Index()
        {
            /*List<Department> departmentList = new List<Department>()
            {
                new Department() { DepartmentId = 1, DepartmentName = "Engineering" },
                new Department() { DepartmentId = 2, DepartmentName = "Bio"}
            };*/

            var departmentList = Db.Departments.ToList();

            return View(departmentList);
        }

        public ActionResult AddDepartment()
        {
            return View(new Department());
        }

        [HttpPost]
        public ActionResult AddDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                Db.Departments.Add(department);
                Db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public ActionResult RemoveDepartment()
        {
            return View(new Department());
        }

        [HttpPost]
        public ActionResult RemoveDepartment(Department department)
        {
            if (!ModelState.IsValid)
            {
                Db.Departments.AddOrUpdate(department);
                return RedirectToAction("Index");
            }
            return View(department);
        }
    }
}