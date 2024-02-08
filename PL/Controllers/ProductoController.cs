using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
    }
}
