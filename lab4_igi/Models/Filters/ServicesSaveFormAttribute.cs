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
    public class ServicesSaveFormAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var clientId = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "ClientId").Value.ToString();
            var employeeId = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "EmployeeId").Value.ToString();
            var serviceTypeId = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "ServiceTypeId").Value.ToString();

            var entityStr = JsonConvert.SerializeObject(new Service()
            {
                ClientId = Int32.Parse(clientId),
                EmployeeId = Int32.Parse(employeeId),
                ServiceTypeId = Int32.Parse(serviceTypeId)
            });
            context.HttpContext.Session.SetString("Service", entityStr);
        }
    }
}
