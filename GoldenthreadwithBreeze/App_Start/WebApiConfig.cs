using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
namespace GoldenthreadwithBreeze
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            
      config.EnableCors();

      //start application controller        
      config.Routes.MapHttpRoute(
           name: "GetApp",
           routeTemplate: "api/{controller}/{action}",
           defaults: new { action = "Get", controller = "Application" });

      config.Routes.MapHttpRoute(
           name: "PostApp",
           routeTemplate: "api/{controller}/{action}",
           defaults: new { action = "Add", controller = "Application" });

      config.Routes.MapHttpRoute(
          name: "StatusAPI",
          routeTemplate: "api/{controller}/{action}/{id}",
          defaults: new { action = "GetVoicesByStatus", controller = "Application" });

      config.Routes.MapHttpRoute(
          name: "DelApp",
          routeTemplate: "api/{controller}/{action}/{id}",
          defaults: new { action = "Remove", controller = "Application" });

      //start chapter controller
      config.Routes.MapHttpRoute(
         name: "GetChapter",
         routeTemplate: "api/{controller}/{action}",
         defaults: new { action = "Get", controller = "Chapter" });

      config.Routes.MapHttpRoute(
        name: "EditChapter",
        routeTemplate: "api/{controller}/{action}/{id}",
        defaults: new { action = "Edit", controller = "Chapter" });

      config.Routes.MapHttpRoute(
         name: "DelChapter",
         routeTemplate: "api/{controller}/{action}",
         defaults: new { action = "Remove", controller = "Chapter" });

      config.Routes.MapHttpRoute(
       name: "AddChapter",
       routeTemplate: "api/{controller}/{action}",
       defaults: new { action = "Add", controller = "Chapter" });

      config.Routes.MapHttpRoute(
       name: "GetVoicesChapter",
       routeTemplate: "api/{controller}/{action}",
       defaults: new { action = "GetVoices", controller = "Chapter" });

      //Start voice controller

      config.Routes.MapHttpRoute(
          name: "GetVoc",
          routeTemplate: "api/{controller}/{action}",
          defaults: new { action = "Get", controller = "Voice" });

      config.Routes.MapHttpRoute(
          name: "EditVoice",
          routeTemplate: "api/{controller}/{action}/{id}",
          defaults: new { action = "Edit", controller = "Voice" });

      config.Routes.MapHttpRoute(
         name: "AddVoice",
         routeTemplate: "api/{controller}/{action}/{id}",
         defaults: new { action = "Add", controller = "Voice" });

      config.Routes.MapHttpRoute(
        name: "DelVoice",
        routeTemplate: "api/{controller}/{action}/{id}",
        defaults: new { action = "Remove", controller = "Voice" });

      config.Routes.MapHttpRoute(
          name: "GetAuthor",
          routeTemplate: "api/{controller}/{action}",
          defaults: new { action = "GetAuthor", controller = "Voice" });

      config.Routes.MapHttpRoute(
        name: "TagAPI",
        routeTemplate: "api/{controller}/{action}",
        defaults: new { action = "GetTags", controller = "Voice" });

      config.Routes.MapHttpRoute(
       name: "voicegetbystatus",
       routeTemplate: "api/{controller}/{action}/{id}",
       defaults: new { action = "GetVoicesByStatus", controller = "Voice" });

      //start Author controller
      config.Routes.MapHttpRoute(
       name: "AuthorVoiceAPI",
       routeTemplate: "api/{controller}/{action}/{id}",
       defaults: new { action = "GetVoice", controller = "Author" });


      config.Routes.MapHttpRoute(
         name: "DefaultApi",
         routeTemplate: "api/{controller}/{id}",
         defaults: new { id = RouteParameter.Optional });

      config.Routes.MapHttpRoute(
          name: "ActionApi",
          routeTemplate: "api/{controller}/{action}/{id}",
          defaults: new { id = RouteParameter.Optional });
    
        }
    }
}
