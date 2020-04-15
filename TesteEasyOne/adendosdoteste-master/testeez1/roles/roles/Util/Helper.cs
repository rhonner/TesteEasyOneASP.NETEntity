using Newtonsoft.Json;
using roles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace roles.Util {
    public class Helper {

        /// <summary>
        /// Load user's data from current cookie
        /// </summary>
        /// <returns></returns>
        public static LogOnModel GetUserId() {
            try {
                //Get cookie from Context
                if (HttpContext.Current != null) {
                    HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                    if (authCookie != null) {
                        FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                        //Deserialize user's data
                        return JsonConvert.DeserializeObject<LogOnModel>(ticket.UserData);
                    }
                }
                return null;
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
        }
    }
}