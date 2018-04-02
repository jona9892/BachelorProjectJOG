using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SKY_INTRA_MVCV2.Security
{
    public class SessionPersister
    {
        static string nameSessionvar = "name";
        static string jobtitleSessionvar = "jobtitle";
        static string sexSessionvar = "sex";
        static string emailSessionvar = "email";
        static string birthdaySessionvar = "birthday";
        //static string userIdSessionvar = "userId";
        //static string userRoleSessionvar = "userRole";

        public static string name
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[nameSessionvar];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[nameSessionvar] = value;
            }
        }

        public static string jobtitle
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[jobtitleSessionvar];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[jobtitleSessionvar] = value;
            }
        }

        public static string sex
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[sexSessionvar];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[sexSessionvar] = value;
            }
        }

        public static string email
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[emailSessionvar];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[emailSessionvar] = value;
            }
        }

        public static string birthday
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                var sessionVar = HttpContext.Current.Session[birthdaySessionvar];
                if (sessionVar != null)
                {
                    return sessionVar as string;
                }
                return null;
            }
            set
            {
                HttpContext.Current.Session[birthdaySessionvar] = value;
            }
        }

        //public static string UserId
        //{
        //    get
        //    {
        //        if (HttpContext.Current == null)
        //        {
        //            return string.Empty;
        //        }
        //        var sessionVar = HttpContext.Current.Session[userIdSessionvar];
        //        if (sessionVar != null)
        //        {
        //            return sessionVar as string;
        //        }
        //        return null;
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[userIdSessionvar] = value;
        //    }
        //}

        //public static string UserRole
        //{
        //    get
        //    {
        //        if (HttpContext.Current == null)
        //        {
        //            return string.Empty;
        //        }
        //        var sessionVar = HttpContext.Current.Session[userRoleSessionvar];
        //        if (sessionVar != null)
        //        {
        //            return sessionVar as string;
        //        }
        //        return null;
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[userRoleSessionvar] = value;
        //    }
        //}
    }
}