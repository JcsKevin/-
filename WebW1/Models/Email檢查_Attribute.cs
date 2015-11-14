using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebW1.Models
{
	public class Email檢查_Attribute : DataTypeAttribute
	{
		public Email檢查_Attribute()
			: base(DataType.Text)
		{
			this.ErrorMessage = "請輸入正確的電子郵件位址!";
		}

		public override bool IsValid(object value)
		{
			var str = value.ToString();
			bool bln;
			try
			{
				System.Net.Mail.MailAddress mail = new System.Net.Mail.MailAddress(str);
				bln = true;
			}
			catch
			{
				bln = false;
			}

			return bln;
		}
	}
}