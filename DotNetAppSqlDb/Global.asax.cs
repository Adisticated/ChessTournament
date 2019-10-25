using DotNetAppSqlDb.Helpers.SendgridEmailServiceHelper;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DotNetAppSqlDb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static ServiceProvider serviceProvider;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IServiceCollection services = new ServiceCollection();
            SendGridClient client = new SendGridClient("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");
            serviceProvider = services.AddSingleton<ISendGridClient>(client)
                                              .BuildServiceProvider();
        }

    }
}
