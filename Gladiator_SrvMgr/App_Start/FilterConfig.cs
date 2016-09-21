using System.Web;
using System.Web.Mvc;

namespace Gladiator_SrvMgr
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}