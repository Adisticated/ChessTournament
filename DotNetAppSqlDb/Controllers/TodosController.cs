using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DotNetAppSqlDb.Models;using System.Diagnostics;
using DotNetAppSqlDb.Helpers.SendgridEmailServiceHelper;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace DotNetAppSqlDb.Controllers
{
    public class TodosController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();
        private IEmailSenderService emailSenderService = new EmailSenderService();

        // GET: Todos
        public ActionResult Home()
        {
            Trace.WriteLine("GET /Todos/Home");
            return View();
        }

        public ActionResult Index()
        {            
            Trace.WriteLine("GET /Todos/Index");
            return View(db.Todoes.ToList());
        }

        // GET: Todos/Details/5
        public ActionResult Details(int? id)
        {
            Trace.WriteLine("GET /Todos/Details/" + id);
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

        // GET: Todos/Create
        public ActionResult Create()
        {
            Trace.WriteLine("GET /Todos/Create");
            return View(new Todo { CreatedDate = DateTime.Now });
        }

        // POST: Todos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Name,CreatedDate,Password,Email")] Todo todo)
        {
            Trace.WriteLine("POST /Todos/Create");
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(todo.Name) || 
                    string.IsNullOrWhiteSpace(todo.Password) ||
                    string.IsNullOrWhiteSpace(todo.Email)) 
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                db.Todoes.Add(todo);
                db.SaveChanges();
                await emailSenderService.SendRegistrationConfirmation(todo.Name, 
                                            new EmailAddress(todo.Email));
                return RedirectToAction("Index");
            }

            return View(todo);
        }

        // GET: Todos/Edit/5
        public ActionResult Edit(int? id)
        {
            Trace.WriteLine("GET /Todos/Edit/" + id);
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

        // POST: Todos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,CreatedDate,Password,Email")] Todo todo)
        {
            Trace.WriteLine("POST /Todos/Edit/" + todo.ID);
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

        // GET: Todos/Delete/5
        public ActionResult Delete(int? id)
        {
            Trace.WriteLine("GET /Todos/Delete/" + id);
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

        // POST: Todos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed([Bind(Include = "ID,Password")]Todo todo1)
        {
            if (!db.Todoes.Find(todo1.ID).Password.Equals(todo1.Password))
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }

            if(db.Matches.Where(x => (x.Player1Id == todo1.ID)
                || (x.Player2Id == todo1.ID)).ToList().Count > 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trace.WriteLine("POST /Todos/Delete/" + todo1.ID);
            Todo todo = db.Todoes.Find(todo1.ID);
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
