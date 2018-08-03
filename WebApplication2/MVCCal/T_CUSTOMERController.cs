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
    public class T_CUSTOMERController : Controller
    {
        private DevRedSolCRA db = new DevRedSolCRA();

        // GET: T_CUSTOMER
        public async Task<ActionResult> Index()
        {
            return View(await db.T_CUSTOMER.ToListAsync());
        }

        // GET: T_CUSTOMER/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_CUSTOMER t_CUSTOMER = await db.T_CUSTOMER.FindAsync(id);
            if (t_CUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(t_CUSTOMER);
        }

        // GET: T_CUSTOMER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_CUSTOMER/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CUSTOMER_ID,CUSTOMER_NAME,CUSTOMER_CONTRACT,CUSTOMER_CONTACT,CUSTOMER_EMAIL,CUSTOMER_ADDRESS,CUSTOMER_PROJECT_ID,CUSTOMER_CREATION,CUSTOMER_UPDATE,CUSTOMER_ACTIF")] T_CUSTOMER t_CUSTOMER)
        {
            if (ModelState.IsValid)
            {
                db.T_CUSTOMER.Add(t_CUSTOMER);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(t_CUSTOMER);
        }

        // GET: T_CUSTOMER/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_CUSTOMER t_CUSTOMER = await db.T_CUSTOMER.FindAsync(id);
            if (t_CUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(t_CUSTOMER);
        }

        // POST: T_CUSTOMER/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CUSTOMER_ID,CUSTOMER_NAME,CUSTOMER_CONTRACT,CUSTOMER_CONTACT,CUSTOMER_EMAIL,CUSTOMER_ADDRESS,CUSTOMER_PROJECT_ID,CUSTOMER_CREATION,CUSTOMER_UPDATE,CUSTOMER_ACTIF")] T_CUSTOMER t_CUSTOMER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_CUSTOMER).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(t_CUSTOMER);
        }

        // GET: T_CUSTOMER/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_CUSTOMER t_CUSTOMER = await db.T_CUSTOMER.FindAsync(id);
            if (t_CUSTOMER == null)
            {
                return HttpNotFound();
            }
            return View(t_CUSTOMER);
        }

        // POST: T_CUSTOMER/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            T_CUSTOMER t_CUSTOMER = await db.T_CUSTOMER.FindAsync(id);
            db.T_CUSTOMER.Remove(t_CUSTOMER);
            await db.SaveChangesAsync();
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
