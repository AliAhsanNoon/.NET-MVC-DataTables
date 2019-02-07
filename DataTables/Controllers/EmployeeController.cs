using DataTables.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DataTables.Controllers
{
    public class EmployeeController : Controller
    {
        DataTableEntities _context = new DataTableEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult EmployeeForm(int? id)
        {
                return View(_context.Employees.Where(x => x.EmpID == id).SingleOrDefault());   
        }

        [HttpPost]
        public ActionResult EmployeeForm(Employee employee)
        {
            using (_context)
            {
                if (employee.EmpID == 0)
                {
                    _context.Employees.Add(employee);
                    _context.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    _context.Entry(employee).State = EntityState.Modified;
                    _context.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult GetData()
        {
            using (_context)
            {
                var empList = _context.Employees.ToList();
                return Json(new { data = empList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (_context)
            {
                var rmEmp = _context.Employees.Where(x=> x.EmpID == id).Single();
                _context.Employees.Remove(rmEmp);
                _context.SaveChanges();
                return Json(new { success = true, message = "Successfully Deleted" }, JsonRequestBehavior.AllowGet);
            }
            
        }
    }
}