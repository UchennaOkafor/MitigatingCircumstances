using Google.Cloud.Datastore.V1;

namespace MitigatingCircumstances.Repositories
{
    public interface ITicketRepository
    {
        void CreateMitigatingRequest();

        Entity GetStudentRequest();

        Entity GetLog(Key key);
    }
}
