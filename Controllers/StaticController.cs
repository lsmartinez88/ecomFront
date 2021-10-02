using Microsoft.AspNetCore.Mvc;

namespace ecomFront.Controllers
{

    public class StaticController : Controller
    {
        public StaticController()
        {
            
        }

        [Route("")]
        [Route("Home")]
        public IActionResult HomeStatic()
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect("/Home/Index");
            }

            return View();
        }
    }
}
