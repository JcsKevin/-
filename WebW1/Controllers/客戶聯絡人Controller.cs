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
		客戶聯絡人Repository ContactRepository = RepositoryHelper.Get客戶聯絡人Repository();
		客戶資料Repository ClientRepository = RepositoryHelper.Get客戶資料Repository();

        // GET: 客戶聯絡人
		public ActionResult Index(string search1)
        {
			var data = ContactRepository.All(false).Include(客 => 客.客戶資料);
			if (!String.IsNullOrEmpty(search1))
			{
				data = data.Where(p => p.客戶資料.客戶名稱.Contains(search1) || p.職稱.Contains(search1) || p.姓名.ToString().Contains(search1) || p.Email.ToString().Contains(search1) || p.手機.Contains(search1) || p.電話.Contains(search1));
			}

			return View(data);
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var data = ContactRepository.GetByID(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
			ViewBag.客戶Id = new SelectList(ClientRepository.All(false), "Id", "客戶名稱");
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
				ContactRepository.Add(客戶聯絡人);
				ContactRepository.UnitOfWork.Commit();
				return RedirectToAction("Index");
            }

			ViewBag.客戶Id = new SelectList(ClientRepository.All(false), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var data = ContactRepository.GetByID(id);
			if (data == null)
            {
                return HttpNotFound();
            }
			ViewBag.客戶Id = new SelectList(ClientRepository.All(false), "Id", "客戶名稱", data.客戶Id);
			return View(data);
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
				var data = ContactRepository.GetByID(客戶聯絡人.Id);
				var includeBind = "職稱,姓名,Email,手機,電話".Split(',');

				if (TryUpdateModel<客戶聯絡人>(data, includeBind))
				{
					ContactRepository.UnitOfWork.Commit();
					return RedirectToAction("Index");
				}
            }
			ViewBag.客戶Id = new SelectList(ClientRepository.All(false), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var data = ContactRepository.GetByID(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			var data = ContactRepository.GetByID(id);
			data.是否已刪除 = true;

			if (TryUpdateModel<客戶聯絡人>(data))
			{
				ContactRepository.UnitOfWork.Commit();
			}
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				((客戶資料Entities)ContactRepository.UnitOfWork.Context).Dispose();
				((客戶資料Entities)ClientRepository.UnitOfWork.Context).Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
