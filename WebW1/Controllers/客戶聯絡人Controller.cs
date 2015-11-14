using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebW1.Models;

namespace WebW1.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶聯絡人
		public ActionResult Index(string search1, string search2, string search3, string search4, string search5, string search6)
        {
			var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料).AsQueryable();
			客戶聯絡人 = 客戶聯絡人.Where(p => p.是否已刪除 == false);
			if (!String.IsNullOrEmpty(search1))
			{
				客戶聯絡人 = 客戶聯絡人.Where(p => p.客戶資料.客戶名稱.Contains(search1));
			}
			if (!String.IsNullOrEmpty(search2))
			{
				客戶聯絡人 = 客戶聯絡人.Where(p => p.職稱.Contains(search2));
			}
			if (!String.IsNullOrEmpty(search3))
			{
				客戶聯絡人 = 客戶聯絡人.Where(p => p.姓名.Contains(search3));
			}
			if (!String.IsNullOrEmpty(search4))
			{
				客戶聯絡人 = 客戶聯絡人.Where(p => p.Email.Contains(search4));
			}
			if (!String.IsNullOrEmpty(search5))
			{
				客戶聯絡人 = 客戶聯絡人.Where(p => p.手機.Contains(search5));
			}
			if (!String.IsNullOrEmpty(search6))
			{
				客戶聯絡人 = 客戶聯絡人.Where(p => p.電話.Contains(search6));
			}

			return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
				var num = db.客戶聯絡人.Where(p => p.客戶Id == 客戶聯絡人.客戶Id && p.Email == 客戶聯絡人.Email).ToList().Count();
				if (num != 1) {			
					db.客戶聯絡人.Add(客戶聯絡人);
					db.SaveChanges();
					return RedirectToAction("Index");
				}
				else
				{
					throw new Exception("同一個客戶下的聯絡人，其 Email 不能重複");
				}
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話,是否已刪除")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
			客戶聯絡人.是否已刪除 = true;
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
