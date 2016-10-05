using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SWNI.Data;
using SWNI.Entities;
using SWNI.Services;

namespace SWNI.Website.Controllers
{
    public class ItemCostsController : Controller
    {        
        private readonly IItemCostService itemsService;

        public ItemCostsController(IItemCostService itemsService)
        {
            this.itemsService = itemsService;
        }

        // GET: ItemCosts
        public async Task<ActionResult> Index()
        {
            return View(await Task.Run(()=> itemsService.GetAll()));
        }

        // GET: ItemCosts/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCost itemCost = await Task.Run(() => itemsService.Get(id.Value));
            if (itemCost == null)
            {
                return HttpNotFound();
            }
            return View(itemCost);
        }

        // GET: ItemCosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemCosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Amount,DateCreated,CreatedBy,DateUpdated,UpdatedBy")] ItemCost itemCost)
        {
            itemCost.DateCreated = DateTime.Now;
            itemCost.CreatedBy = User.Identity.Name;

            if (ModelState.IsValid)
            {
                itemsService.Insert(itemCost);
                return RedirectToAction("Index");
            }

            return View(itemCost);
        }

        // GET: ItemCosts/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCost itemCost = await Task.Run(() => itemsService.Get(id.Value));
            if (itemCost == null)
            {
                return HttpNotFound();
            }
            return View(itemCost);
        }

        // POST: ItemCosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Amount")] ItemCost itemCost)
        {
            if (ModelState.IsValid)
            {
                ItemCost original = await Task.Run(() => itemsService.Get(itemCost.Id));
                if (original != null)
                {
                    original.Name = itemCost.Name;
                    original.Amount = itemCost.Amount;
                    original.DateUpdated = DateTime.Now;
                    original.UpdatedBy = User.Identity.Name;
                }                
                return RedirectToAction("Index");
            }
            return View(itemCost);
        }

        // GET: ItemCosts/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemCost itemCost = await Task.Run(() => itemsService.Get(id.Value));
            if (itemCost == null)
            {
                return HttpNotFound();
            }
            return View(itemCost);
        }

        // POST: ItemCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ItemCost itemCost = await Task.Run(() => itemsService.Get(id));
            await Task.Run(() => itemsService.Delete(itemCost));
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                itemsService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
