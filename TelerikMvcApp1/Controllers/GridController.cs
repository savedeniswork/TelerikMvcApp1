using System;
﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using TelerikMvcApp1.Models;

namespace TelerikMvcApp1.Controllers
{
	public partial class GridController : Controller
    {
		public ActionResult Orders_Read([DataSourceRequest]DataSourceRequest request)
		{
			var result1 = Enumerable.Range(0, 50).Select(i => new OrderViewModel
			{
				OrderID = i,
				Freight = i * 10,
				OrderDate = DateTime.Now.AddDays(i),
				ShipName = "ShipName " + i,
				ShipCity = "ShipCity " + i
			});
            var result2 = Enumerable.Range(0, 10).Select(i => new OrderViewModel
            {
                OrderID = 123567,
                Freight = i * 10,
                OrderDate = DateTime.Now.AddDays(i),
                ShipName = "PHNjcmlwdD5hbGVydCgxMjMpPC9zY3JpcHQ+",
                ShipCity = "LC4vOydbXS09" + i
            });

            result1 = result1.Concat(result2);


            return Json(result1.ToDataSourceResult(request));
		}
	}
}
