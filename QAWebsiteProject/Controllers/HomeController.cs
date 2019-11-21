using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PusherServer;
using QAWebsiteProject.Models;

namespace QAWebsiteProject.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
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
        public ActionResult Comments(int? id)
        {
            var comments = db.Comment.Where(x => x.QuestionID == id).ToArray();
            return Json(comments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Comment(Comment data)
        {
            db.Comment.Add(data);
            db.SaveChanges();
            var options = new PusherOptions();
            options.Cluster = "mt1";
            var pusher = new Pusher("903489", "1bfe83e4cfdaea4bd143", "250fe69cbb1c4721ffa6", options);
            ITriggerResult result = await pusher.TriggerAsync("asp_channel", "asp_event", data);
            return Content("ok");
        }
        public ActionResult Answers(int? id)
        {
            var comments = db.Comment.Where(x => x.QuestionID == id).ToArray();
            return Json(comments, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Answer(Answer data)
        {
            db.Answer.Add(data);
            db.SaveChanges();
            var options = new PusherOptions();
            options.Cluster = "mt1";
            var pusher = new Pusher("903489", "1bfe83e4cfdaea4bd143", "250fe69cbb1c4721ffa6", options);
            ITriggerResult result = await pusher.TriggerAsync("asp_channel", "asp_event", data);
            return Content("ok");
        }
    }
}