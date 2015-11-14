using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WebW1.Models
{
	public class 手機號碼檢查_Attribute : DataTypeAttribute
	{
		public 手機號碼檢查_Attribute()
			: base(DataType.Text)
		{
			this.ErrorMessage = "請輸入正確的手機號碼格式!";
		}

		public override bool IsValid(object value)
		{
			if (value == null)
				return true;
			if (value is String)
				return Regex.IsMatch(value.ToString(), "^09[0-9]{2}-[0-9]{6}");
			else
				return true;
		}
	}
}