using TEALINK.GARDEN.Model.Mobile.Models.Worker;
using TEALINK.Models.Worker;

namespace TEALINK.GARDEN.Interface.Mobile
{
    public interface IWorkerRepo
    {
        public WorkerRegistrationResponseDetails Registration(WorkerRegistrationRequest workerRegistrationRequest);
        public List<WorkerFetchResponseDetails> GetWorker(WorkerFetchRequest workerFetchRequest);
        public List<dynamic> GetBookByWorker(int BookId);
        public List<dynamic> GetWorkerListDynamic(WorkerFetchRequest workerFetchRequest);
        public List<dynamic> WorkerImageManipulation(WorkerImageRequest request);
        public List<dynamic> FetchWorkerImage(int bookId);
    }
}
