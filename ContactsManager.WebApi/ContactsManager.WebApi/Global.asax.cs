using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using ContactsManager.WebApi.App_Start;
using FluentValidation.Mvc;

namespace ContactsManager.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FluentValidationModelValidatorProvider.Configure();
        }
    }
}
