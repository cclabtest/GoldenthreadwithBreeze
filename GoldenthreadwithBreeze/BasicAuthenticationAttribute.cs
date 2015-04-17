using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Web;
using GoldenthreadwithBreeze.Models;

namespace GoldenthreadwithBreeze
{
    public class BasicAuthenticationAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private const string InvalidToken = "Invalid Authorization-Token";
        private const string MissingToken = "Missing Authorization-Token";
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (HttpContext.Current.Request.Headers["AuthToken"] == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                actionContext.Response.Content = new StringContent("Missing Authorization-Token");
            }
            else
            {
                string Values = HttpContext.Current.Request.Headers["AuthToken"];
                GoldenThreadEntities db = new GoldenThreadEntities();
                Guid appKey = Guid.Parse(Values);
                int APIKey = db.Applications.Where(a => a.APIKey == appKey).Count();
                if (APIKey > 0)
                {
                    try
                    {
                        base.OnActionExecuting(actionContext);
                        
                    }
                    catch( Exception ex) {
                        throw ex;
                    }
                }
                else
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    actionContext.Response.Content = new StringContent("Invalid Authorization-Token");
                }
            }
        }
    }
}