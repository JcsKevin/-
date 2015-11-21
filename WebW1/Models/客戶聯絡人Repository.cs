using System;
using System.Linq;
using System.Collections.Generic;
	
namespace WebW1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
		public override IQueryable<客戶聯絡人> All()
		{
			return base.All().Where(p => p.是否已刪除 == false);
		}

		public IQueryable<客戶聯絡人> All(bool isGetAll)
		{
			if (isGetAll)
			{
				return base.All();
			}
			else
			{
				return this.All();
			}
		}

		public 客戶聯絡人 GetByID(int? id)
		{
			return this.All().FirstOrDefault(p => p.Id == id.Value);
		}

		public override void Delete(客戶聯絡人 entity)
		{
			this.GetByID(entity.Id).是否已刪除 = true;
			//base.Delete(entity);
		}
	}

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}