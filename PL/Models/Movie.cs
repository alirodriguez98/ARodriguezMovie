namespace PL.Models
{
    public class Movie
    {
        public int IdPelicula { get; set; }
        public string Titulo { get; set; }
        public string FechaEstreno { get; set; }
        public string Poster { get; set; }
        public List<object> Movies { get; set; }
    }
}
