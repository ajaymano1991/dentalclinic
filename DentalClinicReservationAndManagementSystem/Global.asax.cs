using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace DentalClinicReservationAndManagementSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static HashSet<string> ActiveUsers = new HashSet<string>(); // Stores active user sessions

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start()
        {
            string sessionId = Session.SessionID;
            lock (ActiveUsers)
            {
                ActiveUsers.Add(sessionId); // Add user session
            }
            using (var db = new DentalClinicEntities())
            {
                var stat = db.SiteStats.OrderByDescending(s => s.Id).FirstOrDefault();
                if (stat != null)
                {
                    stat.TotalVisitors += 1;
                    db.SaveChanges();
                }
            }
        }
        

        protected void Session_End()
        {
            string sessionId = Session.SessionID;
            lock (ActiveUsers)
            {
                ActiveUsers.Remove(sessionId); // Remove user session when expired
            }
        }

        public static int GetActiveUserCount()
        {
            return ActiveUsers.Count;
        }
    }
}
