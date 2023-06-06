using LAMovies_NET6.Data;
using LAMovies_NET6.Interfaces;
using LAMovies_NET6.Models;
using System.Xml.Linq;

namespace LAMovies_NET6.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly ApplicationDbContext _data;
        public ActorRepository(ApplicationDbContext data)
        {
            _data = data;
        }
        public bool Add(Actor model)
        {
            try
            {
                _data.Actors.Add(model);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = _data.Actors.Find(id);
                if (data == null)
                    return false;
                _data.Actors.Remove(data);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Actor GetById(int id)
        {
            return _data.Actors.Find(id);
        }

        public List<Actor> GetActorsList()
        {
            return _data.Actors.ToList();
        }

        public ICollection<Movie> GetMoviesByActor(int idActor)
        {
            return _data.MovieActors.Where(g => g.idActor == idActor).Select(m => m.Movie).ToList();
        }
        public Actor GetActorByName(string name)
        {
            return _data.Actors.FirstOrDefault(m => m.nameActor == name);
        }
        public IQueryable<Actor> List()
        {
            var data = _data.Actors.AsQueryable();
            return data;
        }

        public bool Update(Actor model)
        {
            try
            {
                _data.Actors.Update(model);
                _data.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
