using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static Dictionary<string, object> GetByEmail(string email)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""}, {"Usuario", null} };
            ML.Usuario user = new ML.Usuario();

            try
            {
                using (DL.ArodriguezMoviesContext context = new DL.ArodriguezMoviesContext())
                {
                    var resultado = (from usuario in context.Usuarios
                                    where usuario.Email == email
                                    select new
                                    {
                                        Email = usuario.Email,
                                        Password = usuario.Password
                                    }).SingleOrDefault();

                    if(resultado != null)
                    {
                        user.Email = resultado.Email;
                        user.Password = resultado.Password;

                        diccionario["Usuario"] = user;
                        diccionario["Resultado"] = true;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
                    }
                }
            }
            catch (Exception ex)
            {
                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }

            return diccionario;
        }
    }
}
