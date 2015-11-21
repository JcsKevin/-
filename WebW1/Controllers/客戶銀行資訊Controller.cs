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

    public class 客戶銀行資訊Controller : Controller
    {
		客戶銀行資訊Repository BankRepository = RepositoryHelper.Get客戶銀行資訊Repository();
		客戶資料Repository ClientRepository = RepositoryHelper.Get客戶資料Repository();

        // GET: 客戶銀行資訊
		public ActionResult Index(string search1)
        {
			var data = BankRepository.All(false).Include(客 => 客.客戶資料);
			if (!String.IsNullOrEmpty(search1))
			{
				data = data.Where(p => p.客戶資料.客戶名稱.Contains(search1) || p.銀行名稱.Contains(search1) || p.銀行代碼.ToString().Contains(search1) || p.分行代碼.ToString().Contains(search1) || p.帳戶名稱.Contains(search1) || p.帳戶號碼.Contains(search1));
			}

			return View(data);
        }


        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var data = BankRepository.GetByID(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
			ViewBag.客戶Id = new SelectList(ClientRepository.All(false), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
				BankRepository.Add(客戶銀行資訊);
				BankRepository.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

			ViewBag.客戶Id = new SelectList(ClientRepository.All(false), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var data = BankRepository.GetByID(id);
            if (data == null)
            {
                return HttpNotFound();
            }
			ViewBag.客戶Id = new SelectList(ClientRepository.All(false), "Id", "客戶名稱", data.客戶Id);
            return View(data);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼,是否已刪除")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
				var data = BankRepository.GetByID(客戶銀行資訊.Id);
				var includeBind = "銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼".Split(',');

				if (TryUpdateModel<客戶銀行資訊>(data, includeBind))
				{
					BankRepository.UnitOfWork.Commit();
					return RedirectToAction("Index");
				}

            }
            ViewBag.客戶Id = new SelectList(ClientRepository.All(false), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			var data = BankRepository.GetByID(id);
			if (data == null)
            {
                return HttpNotFound();
            }
			return View(data);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			if (ModelState.IsValid)
			{
				var data = BankRepository.GetByID(id);
				data.是否已刪除 = true;

				if (TryUpdateModel<客戶銀行資訊>(data))
				{
					BankRepository.UnitOfWork.Commit();
				}
			}
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				((客戶資料Entities)BankRepository.UnitOfWork.Context).Dispose();
				((客戶資料Entities)ClientRepository.UnitOfWork.Context).Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
