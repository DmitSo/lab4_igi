using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using lab1_ef;

namespace Lab4.Models.Filters
{
    public class ClientsSaveFormAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var name = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "Name").Value.ToString();
            var roomId = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "RoomId").Value.ToString();
            var passport = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "Passport").Value.ToString();
            
            var entityStr = JsonConvert.SerializeObject(new Client()
            {                
                Name = name,
                RoomId = Int32.Parse(roomId),
                Passport = passport
            });
            context.HttpContext.Session.SetString("Client", entityStr);
        }
    }
}
