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
    public class RoomsSaveFormAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var roomNo = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "RoomNo").Value.ToString();
            var cost = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "Cost").Value.ToString();
            var roomType = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "RoomTypeId").Value.ToString();
            var capacity = context.HttpContext.Request.Form.FirstOrDefault(p => p.Key == "Capacity").Value.ToString();
            
            var entityStr = JsonConvert.SerializeObject(new Room()
            {
                RoomNo = roomNo,
                Cost = double.Parse(cost),
                RoomTypeId = Int32.Parse(roomType),
                Capacity = Int32.Parse(capacity)
            });
            context.HttpContext.Session.SetString("Room", entityStr);
        }
    }
}
