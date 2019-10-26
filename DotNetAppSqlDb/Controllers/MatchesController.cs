using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DotNetAppSqlDb.Models;
using System.Diagnostics;
using System.Security.Cryptography;

namespace DotNetAppSqlDb.Controllers
{
    public class MatchesController : Controller
    {
        private MyDatabaseContext db = new MyDatabaseContext();

        public ActionResult Leaderboard()
        {
            Trace.WriteLine("GET /Matches/Leaderboard");
            var result = db.Matches.GroupBy(p => p.WinnerId).
                Select(g => new { id = g.Key, count = g.Count() });

            var res = from todo in db.Todoes
                      join winner in result on todo.ID equals winner.id
                      select new Winner
                        { 
                            Name = todo.Name, 
                            ID= todo.ID, 
                            Count= winner.count 
                        };
            var finalList = res.OrderByDescending(x => x.Count).ToList();
            return View(finalList);
        }

        // GET: Todos/Details/5
        public ActionResult Details(int? id)
        {
            Trace.WriteLine("GET /Matches/Details/" + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var results = db.Matches.Where(x => (id == x.Player2Id)||(id == x.Player1Id)).ToList();

            List<MatchDetailsModel> matchDetails = new List<MatchDetailsModel>();
            foreach(Matches item in results)
            {
                MatchDetailsModel detail = new MatchDetailsModel();
                detail.PlayerOneName = db.Todoes.Find(item.Player1Id).Name;
                detail.PlayerTwoName = db.Todoes.Find(item.Player2Id).Name;
                detail.WinnerName = db.Todoes.Find(item.WinnerId).Name;
                detail.entryDate = item.CreatedDate;
                matchDetails.Add(detail);
            }
            return View(matchDetails);
        }

        // GET: Todos/Create
        public ActionResult Create()
        {
            Trace.WriteLine("GET /Matches/Create");
            CreateMatchResultModel createMatchResultModel = new CreateMatchResultModel
            {
                playerSelectionList = db.Todoes.ToList()
            };

            return View(createMatchResultModel);
        }

        // POST: Todos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlayerOneID,PlayerOnePwd,PlayerTwoID,PlayerTwoPwd,WinnerID")] CreateMatchResultModel match)
        {
            Trace.WriteLine("POST /Matches/Create");

            if(!match.PlayerOnePwd.Equals(db.Todoes.Find(match.PlayerOneID).Password)
                || !match.PlayerTwoPwd.Equals(db.Todoes.Find(match.PlayerTwoID).Password)) 
                    return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);

            if(match.PlayerOneID == match.PlayerTwoID)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            if (!((match.WinnerID == match.PlayerOneID) 
                || (match.WinnerID == match.PlayerTwoID)))
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Matches matchEntry = new Matches
            {
                CreatedDate = DateTime.Now,
                Player1Id = match.PlayerOneID,
                Player2Id = match.PlayerTwoID,
                WinnerId = match.WinnerID
            };
            db.Matches.Add(matchEntry);
            db.SaveChanges();
            return RedirectToAction("Leaderboard");
        }

    }
}