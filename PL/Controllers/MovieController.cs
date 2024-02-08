using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;

namespace PL.Controllers
{
    public class MovieController : Controller
    {
        [HttpGet]
        public IActionResult GetPopulares()
        {
            Models.Movie movie = new Models.Movie();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                var responseTask = client.GetAsync("movie/popular?api_key=20acf2bb051d52df9c3e3df86bcb595b");
                responseTask.Wait();
                var respuesta = responseTask.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsStringAsync();
                    readTask.Wait();
                    movie.Movies = new List<object>();

                    dynamic jsonObj = JObject.Parse(readTask.Result);

                    foreach (var registro in jsonObj.results)
                    {
                        Models.Movie peli = new Models.Movie();
                        peli.IdPelicula = registro.id;
                        peli.Titulo = registro.title;
                        peli.Poster = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/" + registro.poster_path;
                        peli.FechaEstreno = registro.release_date;

                        movie.Movies.Add(peli);
                    }

                    return View(movie);
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpGet]
		public IActionResult GetFavoritas()
		{
			Models.Movie movie = new Models.Movie();
			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
				var responseTask = client.GetAsync("account/20961216/favorite/movies?api_key=20acf2bb051d52df9c3e3df86bcb595b&session_id=3b90661128b6d21c3210106b79c45e7b4c13f194");
				responseTask.Wait();
				var respuesta = responseTask.Result;

				if (respuesta.IsSuccessStatusCode)
				{
					var readTask = respuesta.Content.ReadAsStringAsync();
					readTask.Wait();
					movie.Movies = new List<object>();

					dynamic jsonObj = JObject.Parse(readTask.Result);

					foreach (var registro in jsonObj.results)
					{
						Models.Movie peli = new Models.Movie();
						peli.IdPelicula = registro.id;
						peli.Titulo = registro.title;
						peli.Poster = "https://image.tmdb.org/t/p/w600_and_h900_bestv2/" + registro.poster_path;
						peli.FechaEstreno = registro.release_date;

						movie.Movies.Add(peli);
					}

					return View(movie);
				}
				else
				{
					return View();
				}
			}
		}

        [HttpPost]
		public IActionResult AgregarFavorito(int idPelicula)
        {
            using (HttpClient client = new HttpClient())
            {
                string media_type = "";
                int media_id = 0;
                string favorite = "";
                var objetoAnonimo = new { media_type = "movie", media_id = idPelicula, favorite = true };

                client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                var responseTask = client.PostAsJsonAsync("account/20961216/favorite?api_key=20acf2bb051d52df9c3e3df86bcb595b&session_id=3b90661128b6d21c3210106b79c45e7b4c13f194", objetoAnonimo);
                responseTask.Wait();

                var respuesta = responseTask.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                    readTask.Wait();
                    Dictionary<string, object> result = readTask.Result;

                    bool resultado = (bool)result["success"];

                    if (resultado)
                    {
                        ViewBag.Mensaje = "Se ha agregado esta pelicula a favoritos";
                        return PartialView("Modal");
                    }
                    else
                    {
						ViewBag.Mensaje = "Ocurrio un error!";
						return PartialView("Modal");
					}
                }
                else
                {
                    ViewBag.Mensaje = "Ha ocurrido un error";
                    return PartialView("Modal");
                }
            }
        }

		[HttpPost]
		public IActionResult EliminarFavorito(int idPelicula)
		{
			using (HttpClient client = new HttpClient())
			{
				string media_type = "";
				int media_id = 0;
				string favorite = "";
				var objetoAnonimo = new { media_type = "movie", media_id = idPelicula, favorite = false };

				client.BaseAddress = new Uri("https://api.themoviedb.org/3/");
				var responseTask = client.PostAsJsonAsync("account/20961216/favorite?api_key=20acf2bb051d52df9c3e3df86bcb595b&session_id=3b90661128b6d21c3210106b79c45e7b4c13f194", objetoAnonimo);
				responseTask.Wait();

				var respuesta = responseTask.Result;

				if (respuesta.IsSuccessStatusCode)
				{
					var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
					readTask.Wait();
					Dictionary<string, object> result = readTask.Result;

					bool resultado = (bool)result["success"];

					if (resultado)
					{
						ViewBag.Mensaje = "Se ha quitado esta pelicula de favoritos";
						return PartialView("Modal");
					}
					else
					{
						ViewBag.Mensaje = "Ocurrio un error!";
						return PartialView("Modal");
					}
				}
				else
				{
					ViewBag.Mensaje = "Ha ocurrido un error";
					return PartialView("Modal");
				}
			}
		}
	}
}
