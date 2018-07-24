using SuperHeroes.Models;
using System;
using System.Collections.Generic;
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
    }
}