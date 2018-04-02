using SKY_INTRA_MVCV2.Security;
using System.Web;
using System.Web.Mvc;

namespace SKY_INTRA_MVCV2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

                filters.Add(new Session());
            
        }
    }
}
