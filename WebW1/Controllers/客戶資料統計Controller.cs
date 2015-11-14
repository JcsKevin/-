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
    public class 客戶資料統計Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶資料統計
		public ActionResult Index(string search1, string search2, string search3)
        {
			var data = db.客戶資料統計.AsQueryable();
			if (!String.IsNullOrEmpty(search1))
			{
				data = data.Where(p => p.客戶名稱.Contains(search1));
			}
			if (isNumberic(search2))
			{
				data = data.Where(p => p.客戶聯絡人數量.ToString() == search2);
			}
			if (isNumberic(search3))
			{
				data = data.Where(p => p.客戶銀行資訊數量.ToString() == search3);
			}
			return View(data);
        }
		

		private bool isNumberic(string _string)
		{
			if (string.IsNullOrEmpty(_string))
				return false;
			foreach (char c in _string)
			{
				if (!char.IsDigit(c))
					return false;
			}
			return true;
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
