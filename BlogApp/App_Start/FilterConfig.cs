using System.Web;
using System.Web.Mvc;
using BlogApp.Filter;

namespace BlogApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new NotificationFilter());
        }
    }
}
