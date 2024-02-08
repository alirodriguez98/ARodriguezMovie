using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        public static Dictionary<string, object> Add(ML.Producto producto)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""} };

            try
            {
                using (DL.ArodriguezMoviesContext context = new DL.ArodriguezMoviesContext())
                {
                    var filasAfectadas = context.Database.ExecuteSqlRaw($"ProductoAdd '{producto.Nombre}'," +
                        $"{producto.Precio}, @Imagen, {producto.Categoria.IdCategoria}", new SqlParameter("@Imagen",producto.Imagen));

                    if (filasAfectadas > 0)
                    {
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

        public static Dictionary<string, object> Update(ML.Producto producto)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Resultado", false }, { "Excepcion", "" } };

            try
            {
                using (DL.ArodriguezMoviesContext context = new DL.ArodriguezMoviesContext())
                {
                    var filasAfectadas = context.Database.ExecuteSqlRaw($"ProductoUpdate '{producto.Nombre}'," +
                        $"{producto.Precio}, @Imagen, {producto.Categoria.IdCategoria}, {producto.IdProducto}", 
                        new SqlParameter("@Imagen", producto.Imagen));

                    if (filasAfectadas > 0)
                    {
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

        public static Dictionary<string, object> Delete(byte idProducto)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Resultado", false }, { "Excepcion", "" } };

            try
            {
                using (DL.ArodriguezMoviesContext context = new DL.ArodriguezMoviesContext())
                {
                    var filasAfectadas = context.Database.ExecuteSqlRaw($"ProductoDelete {idProducto}");

                    if (filasAfectadas > 0)
                    {
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

        public static Dictionary<string, object> GetById(byte idProducto)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", "" }, { "Producto", null } };

            try
            {
                using (DL.ArodriguezMoviesContext context = new DL.ArodriguezMoviesContext())
                {
                    var registro = (from producto in context.Productos
                                    join categoria in context.Categoria on producto.IdCategoria equals categoria.IdCategoria
                                    where producto.IdProducto == idProducto
                                    select new
                                    {
                                        IdProducto = producto.IdProducto,
                                        Nombre = producto.Nombre,
                                        Precio = producto.Precio,
                                        Imagen = producto.Imagen,
                                        IdCategoria = categoria.IdCategoria,
                                        NombreCategoria = categoria.Nombre
                                    }).SingleOrDefault();

                    if (registro != null)
                    {
                        ML.Producto item = new ML.Producto();
                        item.IdProducto = registro.IdProducto;
                        item.Nombre = registro.Nombre;
                        item.Precio = registro.Precio;
                        item.Imagen = registro.Imagen;
                        item.Categoria = new ML.Categoria();
                        item.Categoria.IdCategoria = registro.IdCategoria;
                        item.Categoria.Nombre = registro.NombreCategoria;

                        diccionario["Resultado"] = true;
                        diccionario["Producto"] = item;
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

        public static Dictionary<string, object> GetAll(byte idCategoria)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object> { { "Resultado", false }, { "Excepcion", "" }, { "Producto", null } };

            try
            {
                using (DL.ArodriguezMoviesContext context = new DL.ArodriguezMoviesContext())
                {
                    List<T> registros;
                    if(idCategoria == 0)
                    {
                        registros = (from producto in context.Productos
                                         join categoria in context.Categoria on producto.IdCategoria equals categoria.IdCategoria
                                         select new
                                         {
                                             IdProducto = producto.IdProducto,
                                             Nombre = producto.Nombre,
                                             Precio = producto.Precio,
                                             Imagen = producto.Imagen,
                                             IdCategoria = categoria.IdCategoria,
                                             NombreCategoria = categoria.Nombre
                                         }).ToList();
                    }
                    else
                    {
                        registros = (from producto in context.Productos
                                         join categoria in context.Categoria on producto.IdCategoria equals categoria.IdCategoria
                                         where producto.IdCategoria == idCategoria
                                         select new
                                         {
                                             IdProducto = producto.IdProducto,
                                             Nombre = producto.Nombre,
                                             Precio = producto.Precio,
                                             Imagen = producto.Imagen,
                                             IdCategoria = categoria.IdCategoria,
                                             NombreCategoria = categoria.Nombre
                                         }).ToList();
                    }

                    if (registros != null)
                    {
                        ML.Producto item = new ML.Producto();
                        foreach (var registro in registros)
                        {
                            item.Productos = new List<object>();

                            ML.Producto pro = new ML.Producto();

                            pro.IdProducto = registro.IdProducto;
                            pro.Nombre = registro.Nombre;
                            pro.Precio = registro.Precio;
                            pro.Imagen = registro.Imagen;
                            pro.Categoria = new ML.Categoria();
                            pro.Categoria.IdCategoria = registro.IdCategoria;
                            pro.Categoria.Nombre = registro.NombreCategoria;

                            item.Productos.Add(pro);
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Producto"] = item;
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
