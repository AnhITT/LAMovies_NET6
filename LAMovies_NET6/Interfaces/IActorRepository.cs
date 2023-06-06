using LAMovies_NET6.Models;

namespace LAMovies_NET6.Interfaces
{
    public interface IActorRepository
    {
        bool Add(Actor model);
        bool Update(Actor model);
        Actor GetById(int id);
        bool Delete(int id);
        List<Actor> GetActorsList();
        ICollection<Movie> GetMoviesByActor(int idActor);
        IQueryable<Actor> List();
        Actor GetActorByName(string name);
    }
}
