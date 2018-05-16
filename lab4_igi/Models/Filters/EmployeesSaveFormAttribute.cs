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
    public class EmployeesSaveFormAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var empName = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "Name").Value.ToString();

            var entityStr = JsonConvert.SerializeObject(new Employee()
            {
                Name = empName
            });
            context.HttpContext.Session.SetString("Employee", entityStr);
        }
    }
}
