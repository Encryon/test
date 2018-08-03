using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCCal
{
    public class T_CRAController : Controller
    {
        private DevRedSolCRA db = new DevRedSolCRA();

        // GET: T_CRA
        public async Task<ActionResult> Index()
        {
            var t_CRA = db.T_CRA.Include(t => t.T_CONSULTANT);
            return View(await t_CRA.ToListAsync());
        }

        // GET: T_CRA/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_CRA t_CRA = await db.T_CRA.FindAsync(id);
            if (t_CRA == null)
            {
                return HttpNotFound();
            }
            return View(t_CRA);
        }

        // GET: T_CRA/Create
        public ActionResult Create()
        {
            ViewBag.RESSOURCE = new SelectList(db.T_CONSULTANT, "ID", "NOM");
            return View();
        }

        // POST: T_CRA/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,MOIS,ANNEE,RESSOURCE,JOUR,JOURINFO,DATEMS")] T_CRA t_CRA)
        {
            if (ModelState.IsValid)
            {
                db.T_CRA.Add(t_CRA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RESSOURCE = new SelectList(db.T_CONSULTANT, "ID", "NOM", t_CRA.RESSOURCE);
            return View(t_CRA);
        }

        // GET: T_CRA/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_CRA t_CRA = await db.T_CRA.FindAsync(id);
            if (t_CRA == null)
            {
                return HttpNotFound();
            }
            ViewBag.RESSOURCE = new SelectList(db.T_CONSULTANT, "ID", "NOM", t_CRA.RESSOURCE);
            return View(t_CRA);
        }

        // POST: T_CRA/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,MOIS,ANNEE,RESSOURCE,JOUR,JOURINFO,DATEMS")] T_CRA t_CRA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_CRA).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RESSOURCE = new SelectList(db.T_CONSULTANT, "ID", "NOM", t_CRA.RESSOURCE);
            return View(t_CRA);
        }

        // GET: T_CRA/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_CRA t_CRA = await db.T_CRA.FindAsync(id);
            if (t_CRA == null)
            {
                return HttpNotFound();
            }
            return View(t_CRA);
        }

        // POST: T_CRA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            T_CRA t_CRA = await db.T_CRA.FindAsync(id);
            db.T_CRA.Remove(t_CRA);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // GET: T_CRA/Create
        public ActionResult CreateEvent()
        {
            ViewBag.RESSOURCE = new SelectList(db.T_CONSULTANT, "ID", "NOM");
            return PartialView(new T_CRA());
        }

        // POST: T_CRA/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateEvent([Bind(Include = "ID,MOIS,ANNEE,RESSOURCE,JOUR,JOURINFO,DATEMS")] T_CRA t_CRA)
        {
            if (ModelState.IsValid)
            {
                db.T_CRA.Add(t_CRA);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RESSOURCE = new SelectList(db.T_CONSULTANT, "ID", "NOM", t_CRA.RESSOURCE);
            return PartialView(t_CRA);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        //public static LoadMasterRessource()
        //{

        //}
    }
}
