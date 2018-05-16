using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Lab4.Models.Filters
{
    public class JournalKeeperAttribute : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var serializedJournal = context.HttpContext.Session.GetString("Journal");
            var journal = serializedJournal == null ? 
                new List<string>() : 
                JsonConvert.DeserializeObject<List<string>>(serializedJournal);

            var path = context.HttpContext.Request.Path.Value;
            if (path == "/")
            {
                journal.Add("User visited: /Home/Index");
            }
            else
            {
                // TODO
                if(new List<string>() { "/ServiceTypes", "/Services", "/TypeOfServices" }.Contains(path))
                {
                    journal.Add("User visited: " + path + "/Index");
                }
                else
                {
                    journal.Add("User visited: " + path);
                }
            }
            serializedJournal = JsonConvert.SerializeObject(journal);
            context.HttpContext.Session.SetString("Journal", serializedJournal);
        }
    }
}
