using OnionArchitecture.Core.Interfaces.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnionArchitecture.Core.Models;
using OnionArchitecture.Repository.ADO.NET;
using System.Diagnostics;

namespace OnionArchitecture.Controllers
{
    public class HomeController : Controller
    {
        ITodoService todoService;
        private TODOEntities db = new TODOEntities();

        public HomeController(ITodoService _todoService)
        {
            this.todoService = _todoService;
        }

        public ActionResult Index()
        {
            //select
            var timer = new Stopwatch();
            timer.Start();



            var test = from abc in db.Tbl_ToDo
                       select new Todo
                       {
                           Id = abc.Id,
                           TaskName = abc.TaskName
                       };
            var test1 = test.ToList();

            timer.Stop();
            var coba1 = db.Tbl_ToDo.ToList();


            var testSP = db.USP_GetAllTask().ToList();

            var timer1 = new Stopwatch();
            timer1.Start();
            var get = todoService.GetTodos();
            timer1.Stop();

            ////insert
            //Tbl_User user = new Tbl_User();
            //user.UserName = "rahmat";
            //user.Password = "rahmat";
            //var inser = db.Tbl_User.Add(user);
            //db.SaveChanges();

            ////update
            //Tbl_User updateUser = db.Tbl_User.Where(x => x.Id == 1).First();
            //updateUser.UserName = "updated";
            //db.SaveChanges();

            //delete
            //Tbl_User user = new Tbl_User(x);
            //db.Tbl_User.Remove(user);
            //db.SaveChanges();

            //List<Todo> todos = todoService.GetTodos().ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}