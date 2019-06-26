using CampusManagement.Business.Generics;
using CampusManagement.Business.Stage.Models;
using System.Threading.Tasks;

namespace CampusManagement.Business.Stage
{
    public interface IStageService : IDetailsService<StageDetailsModel>,
        ICreateService<StageCreateModel>
    {
        Task<StageDetailsModel> GetLastStage(params string[] includes);
    }
}
