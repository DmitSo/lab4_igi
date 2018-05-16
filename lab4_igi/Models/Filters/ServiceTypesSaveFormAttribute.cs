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
    public class ServiceTypesSaveFormAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var id = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "ServiceTypeId").Value.ToString();
            var name = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "Name").Value.ToString();
            var cost = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "Cost").Value.ToString();
            var description = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "Description").Value.ToString();
            
            var entityStr = JsonConvert.SerializeObject(new ServiceType()
            {                
                //ServiceTypeId = Int32.Parse(id),
                Name = name,
                Cost = double.Parse(cost),
                Description = description,
            });
            context.HttpContext.Session.SetString("ServiceType", entityStr);
        }
    }
}
