using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Categoria
    {
        public static Dictionary<string, object> GetById(byte idCategoria)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { {"Resultado", false}, {"Excepcion", ""}, {"Categoria", null} };

            try
            {
                using (DL.ArodriguezMoviesContext context = new DL.ArodriguezMoviesContext())
                {
                    var registro = (from categoria in context.Categoria
                                    where categoria.IdCategoria == idCategoria
                                    select new
                                    {
                                        IdCategoria = categoria.IdCategoria,
                                        Nombre = categoria.Nombre
                                    }).SingleOrDefault();

                    if(registro != null)
                    {
                        ML.Categoria categor = new ML.Categoria();
                        categor.IdCategoria = registro.IdCategoria;
                        categor.Nombre = registro.Nombre;

                        diccionario["Resultado"] = true;
                        diccionario["Categoria"] = categor;
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

        public static Dictionary<string, object> GetAll()
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", "" }, { "Categoria", null } };

            try
            {
                using (DL.ArodriguezMoviesContext context = new DL.ArodriguezMoviesContext())
                {
                    var registros = (from categoria in context.Categoria
                                    select new
                                    {
                                        IdCategoria = categoria.IdCategoria,
                                        Nombre = categoria.Nombre
                                    }).ToList();

                    if (registros != null)
                    {
                        ML.Categoria categor = new ML.Categoria();
                        categor.Categorias = new List<object>();
                        foreach(var registro in registros)
                        {
                            ML.Categoria cate = new ML.Categoria();
                            cate.IdCategoria = registro.IdCategoria;
                            cate.Nombre = registro.Nombre;

                            categor.Categorias.Add(cate);
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Categoria"] = categor;
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
