using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using QAWebsiteProject.Models;

namespace QAWebsiteProject.Controllers
{
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index()
        {
            string currentUserId = User.Identity.GetUserId();
            var questions = db.Questions.Where(e => e.ApplicationUserId==currentUserId);
            return View(questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FullName");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionID,Title,description,DatePosted,ApplicationUserId")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.DatePosted = DateTime.Now;
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FullName", question.ApplicationUserId);
            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FullName", question.ApplicationUserId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionID,Title,description,DatePosted,ApplicationUserId")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "FullName", question.ApplicationUserId);
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
   
        public ActionResult AllQuestions(string sortOrder = "" )
        {
            ViewBag.DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.MostAnsSort = string.IsNullOrEmpty(sortOrder) ? "popular" : "";
            ViewBag.Today = string.IsNullOrEmpty(sortOrder) ? "Last 24 hours" : "";
            var ques = db.Questions.ToList();
            switch (sortOrder.ToLower())
            {
                case "Date":
                    ques = ques.OrderBy(q => q.DatePosted).ToList();
                    break;
                case "date_desc":
                    ques = ques.OrderByDescending(q => q.DatePosted).ToList();
                    break;
                case "popular":
                    ques = ques.OrderByDescending(q => q.Answers.Count).ToList();
                    break;
                case "Last 24 hours":
                    var today = DateTime.Today;
                    ques = ques.OrderByDescending(q => q.Answers.Where(a => a.DateCreated.Date == today).ToList().Count()).ToList();
                    break;
            }
            
            return View(ques);
            

        }
        [HttpPost]
        public ActionResult AllQuestions(Vote model)
        {
            var vote = model.Value;
            var ques = model.question;
            db.SaveChanges();
            return View();
        }
        //public ActionResult HomePage()
        //{
        //    var questions = db.Questions.ToList();
        //    return View(questions);
        //}
        public ActionResult ListOfTags()
        {
            var tags = db.Tags.ToList();
            return View(tags);
        }
    }
}
