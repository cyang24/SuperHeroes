using SuperHeroes.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperHeroes.Controllers
{
    public class SuperHeroController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: SuperHero
        public ActionResult Index()
        {
            return View(db.SuperHeros.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.SuperheroID = new SelectList(db.SuperHeros, "Id", "SuperHeroName");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "Id, SuperHeroName, AlterEgo, PrimaryAbility, SecondaryAbility, CatchPhrase")] SuperHero superhero)
        {
            if(ModelState.IsValid)
            {
                db.SuperHeros.Add(superhero);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SuperheroID = new SelectList(db.SuperHeros, "Id", "SuperHeroName", superhero.Id);
            return View(superhero);
        }

        public ActionResult Details(int id = 0)
        {

            SuperHero superhero = db.SuperHeros.Find(id);
            return View(superhero);
        }

        public ActionResult Edit(int id = 0)
        {

            var hero = db.SuperHeros.Where(s=>s.Id == id).FirstOrDefault();

            return View(hero);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "Id, SuperHeroName, AlterEgo, PrimaryAbility, SecondaryAbility, CatchPhrase")] SuperHero superhero)
        {

            if (ModelState.IsValid)
            {
                db.Entry(superhero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(superhero);
        }

        public ActionResult Delete(int? id = 0)
        {

            SuperHero superhero = db.SuperHeros.Find(id);
            return View(superhero);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id)
        {
                SuperHero superhero = db.SuperHeros.Find(id);
                db.SuperHeros.Remove(superhero);
                db.SaveChanges();
                return RedirectToAction("Index");
        
        }

    }
}