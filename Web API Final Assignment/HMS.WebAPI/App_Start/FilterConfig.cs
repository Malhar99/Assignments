﻿using HMS.WebApi;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace WebApiAssignment
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
