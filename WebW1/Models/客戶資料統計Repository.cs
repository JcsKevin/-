using System;
using System.Linq;
using System.Collections.Generic;
	
namespace WebW1.Models
{   
	public  class 客戶資料統計Repository : EFRepository<客戶資料統計>, I客戶資料統計Repository
	{

	}

	public  interface I客戶資料統計Repository : IRepository<客戶資料統計>
	{

	}
}