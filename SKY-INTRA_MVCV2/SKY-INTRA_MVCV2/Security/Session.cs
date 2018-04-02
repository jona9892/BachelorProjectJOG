using ServiceGateway.APIGateway.Implementation;
using ServiceGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SKY_INTRA_MVCV2.Security
{
    public class Session : ActionFilterAttribute
    {
        EmployeeGateway eg = new EmployeeGateway();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(SessionPersister.name == null)
            {
                Employee emp = eg.GetEmployee(HttpContext.Current.User.Identity.Name);

                SessionPersister.name = emp.name;
                SessionPersister.jobtitle = emp.jobtitle;
                SessionPersister.sex = emp.sex;
                SessionPersister.email = emp.email;
                SessionPersister.birthday = emp.birthday.ToShortDateString();
            }            
        }
    }
}