using MitigatingCircumstances.Models;
using MitigatingCircumstances.Repositories.Interface;

namespace MitigatingCircumstances.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly ApplicationDbContext Context;

        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
