using OnionArchitecture.Models;
using OnionArchitecture.Repository.ADO.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnionArchitecture.Controllers
{
    public class ToDoController : Controller
    {
        private TODOEntities db = new TODOEntities();
        // GET: ToDo
        public ActionResult Index()
        {
            ToDo_VM toDo_VM = new ToDo_VM();
            toDo_VM.ListStatus = GetAllStatus();
            return View(toDo_VM);
        }

        public List<SelectListItem> GetAllStatus()
        {
            List<SelectListItem> listStatus = db.Tbl_M_Status.ToList().OrderBy(x => x.Id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Status
                }).ToList();


            return listStatus;
        }

        [HttpPost]
        public string AddMaterial(ToDo_VM toDo_VM)
        {
            try
            {
                Tbl_ToDo toDo = new Tbl_ToDo
                {
                    TaskName = toDo_VM.TaskName,
                    TaskDetails = toDo_VM.TaskDetails,
                    TaskStatus = toDo_VM.TaskStatusID,
                    TaskDate = DateTime.ParseExact(toDo_VM.TaskDate, "dd/mm/yyyy", new System.Globalization.CultureInfo("id-ID")),
                    CreatedDate = DateTime.Now,
                    CreatedBy = "rahmat"
                };
                db.Tbl_ToDo.Add(toDo);
                db.SaveChanges();
                return "success";
            }
            catch (Exception ex)
            {
                return "failed";
            }
        }

        [HttpGet]
        public JsonResult GetAllTask()
        {
            var result = db.USP_GetAllTask().ToList();
            return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
    }
}