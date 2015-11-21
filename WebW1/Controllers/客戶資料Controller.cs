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
    public class 客戶資料Controller : Controller
    {
		客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();

        // GET: 客戶資料
		public ActionResult Index(string search1)
        {
			var data = repo.All(false);
			if (!String.IsNullOrEmpty(search1))
			{
				data = data.Where(p => p.客戶名稱.Contains(search1) || p.統一編號.Contains(search1) || p.電話.Contains(search1) || p.傳真.Contains(search1) || p.地址.Contains(search1) || p.Email.Contains(search1));
			}

			return View(data);
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

			var data = repo.GetByID(id);
			if (data == null)
            {
                return HttpNotFound();
            }
			return View(data);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,是否已刪除")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
				repo.Add(客戶資料);
				repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

			return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var data = repo.GetByID(id);
			if (data == null)
            {
                return HttpNotFound();
            }
			return View(data);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,是否已刪除")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
				var data = repo.GetByID(客戶資料.Id);
				//設定要取得哪些欄位 //於延遲驗證時僅會傳入有對應到的欄位
				var includeBind = "客戶名稱,統一編號,電話,傳真,地址,Email".Split(',');

				if (TryUpdateModel<客戶資料>(data, includeBind))
				{
					repo.UnitOfWork.Commit();
					return RedirectToAction("Index");
				}
            }
			return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var data = repo.GetByID(id);
			if (data == null)
			{
				return HttpNotFound();
			}
			return View(data);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			if (ModelState.IsValid)
			{
				var data = repo.GetByID(id);
				data.是否已刪除 = true;
				//設定要取得哪些欄位 //於延遲驗證時僅會傳入有對應到的欄位
				//var includeBind = "客戶名稱,統一編號,電話,傳真,地址,Email".Split(',');

				if (TryUpdateModel<客戶資料>(data))
				{
					repo.UnitOfWork.Commit();
				}
			}
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				((客戶資料Entities)repo.UnitOfWork.Context).Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
