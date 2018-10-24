using Google.Cloud.Datastore.V1;
using System;

namespace MitigatingCircumstances.Repositories
{
    public class StudentTicketRepository : ITicketRepository
    {
        private const string Kind = "Ticket";
        private readonly KeyFactory _keyFactory;
        private readonly DatastoreDb _db;

        public StudentTicketRepository(DatastoreDb db)
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
                ["email"] = "tea&milk@gmail.com",
                ["method"] = true,
                ["student"] = _db.AllocateId(_keyFactory.CreateIncompleteKey()),
                ["time_stamp"] = DateTime.UtcNow
            };

            _db.Insert(entity);
        }

        public Entity GetStudentRequest()
        {
            //var key = _keyFactory.CreateKey(5645015573331968);

            //return _db.Lookup(key);
            return _db.RunQuery(new Query(Kind)
            {

            }).Entities[0];
        }

        public Entity GetLog(Key key)
        {
            return _db.Lookup(key);
        }
    }
}
