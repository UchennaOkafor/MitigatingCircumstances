using Google.Cloud.Datastore.V1;

namespace MitigatingCircumstances.Repositories
{
    public class Repository
    {
        private readonly DatastoreDb _db;

        public Repository(DatastoreDb db)
        {
            _db = db;
        }

        public void Add(Entity entity)
        {
            _db.Insert(entity);
        }

        public Entity GetById(Key key)
        {
            return _db.Lookup(key);
        }

        public DatastoreQueryResults Search(Query query)
        {
            return _db.RunQueryLazily(query).GetAllResults();
        }

        public void Update(Entity entity)
        {
            _db.Update(entity);
        }

        public void Delete(Entity entity)
        {
            _db.Delete(entity);
        }

        public void Delete(Key key)
        {
            _db.Delete(key);
        }
    }
}
