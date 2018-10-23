using Google.Cloud.Datastore.V1;

namespace MitigatingCircumstances.Repositories
{
    public class StudentRequestRepository : IStudentRequestRepository
    {
        private const string Kind = "StudentRequest";
        private readonly KeyFactory _keyFactory;
        private readonly DatastoreDb _db;

        public StudentRequestRepository(DatastoreDb db)
        {
            _db = db;
            _keyFactory = db.CreateKeyFactory(Kind);
        }

        public void CreateMitigatingRequest()
        {
            Key key = _keyFactory.CreateIncompleteKey();
            var entity = new Entity()
            {
                Key = key,
                ["email"] = "test@gmail.com",
                ["method"] = false
            };

            _db.Insert(entity);
        }

        public Entity GetStudentRequest()
        {
            var key = _keyFactory.CreateKey(5645015573331968);

            return _db.Lookup(key);
        }

        public Entity GetLog(Key key)
        {
            return _db.Lookup(key);
        }
    }
}
