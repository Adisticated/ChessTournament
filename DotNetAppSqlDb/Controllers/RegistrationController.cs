using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DotNetAppSqlDb.Models;using System.Diagnostics;

namespace DotNetAppSqlDb.Controllers
{
    public class RegistrationController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        // GET: Registration
        public ActionResult Index()
        {            
            Trace.WriteLine("GET /Registration/Index");
            return View(db.Todoes.ToList());
        }

        // GET: Registration/Details/5
        public ActionResult Details(int? id)
        {
            Trace.WriteLine("GET /Registration/Details/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            Trace.WriteLine("GET /Registration/Create");
            return View(new Todo { CreatedDate = DateTime.Now });
        }

        // POST: Registration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,CreatedDate,Password")] Todo todo)
        {
            Trace.WriteLine("POST /Registration/Create");
            if (ModelState.IsValid)
            {
                db.Todoes.Add(todo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        // GET: Registration/Edit/5
        public ActionResult Edit(int? id)
        {
            Trace.WriteLine("GET /Registration/Edit/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Registration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,CreatedDate,Password")] Todo todo)
        {
            Trace.WriteLine("POST /Registration/Edit/" + todo.ID);
            Todo existingTodo = db.Todoes.Find(todo.ID);
            if (!existingTodo.Password.Equals(todo.Password))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            if (ModelState.IsValid)
            {
                db.Entry(existingTodo).CurrentValues.SetValues(todo);
                db.Entry(existingTodo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(todo);
        }

        // GET: Registration/Delete/5
        public ActionResult Delete(int? id)
        {
            Trace.WriteLine("GET /Registration/Delete/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Todo todo = db.Todoes.Find(id);
            if (todo == null)
            {
                return HttpNotFound();
            }
            return View(todo);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, string password)
        {
            if (!db.Todoes.Find(id).Password.Equals(password))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            Trace.WriteLine("POST /Registration/Delete/" + id);
            Todo todo = db.Todoes.Find(id);
            db.Todoes.Remove(todo);
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
    }
}
