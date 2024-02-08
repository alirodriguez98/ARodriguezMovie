using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace PL.Controllers
{
	public class LoginController : Controller
	{
		public ActionResult Login()
		{
			return View();
		}
		
		[HttpPost]
		public ActionResult Login(string email, string password)
		{
			Dictionary<string, object> result = BL.Usuario.GetByEmail(email);
			bool resultado = (bool)result["Resultado"];
			if (resultado)
			{
				ML.Usuario usuario = (ML.Usuario)result["Usuario"];
				string contraCadena = Encoding.UTF8.GetString(usuario.Password);

                if (contraCadena == password)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ViewBag.Mensaje = "Credenciales incorrectas";
					return PartialView("Modal");
				}
			}
			else
			{
				ViewBag.Mensaje = "Credenciales incorrectas";
				return PartialView("Modal");
			}
		}
	}
}
