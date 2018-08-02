using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using tst.Models;

namespace tst
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start() // start point initializing program's part
        {
            Database.SetInitializer(new UserDbInitializer()); // introduce new users to the database

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
