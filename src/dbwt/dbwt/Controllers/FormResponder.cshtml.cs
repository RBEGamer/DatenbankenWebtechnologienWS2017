using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dbwt.Views.Home
{
    [ViewComponent(Name = "FormResponder")]
    public class FormResponder : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return Content(DB_KV_ACCESS.Instance.ToString());
        }


        public IViewComponentResult Write()
        {
            return Content("penis");
        }
 


    }
}
