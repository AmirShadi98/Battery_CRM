using Battery_CRM.Core.Domain.Models.ViewModels.Branch;
using Battery_CRM.Framework.Extensions;

namespace Battery_CRM.Core.Domain.Abstract;

public interface IBranchService
{
    Task<List<BranchesViewModel>> GetAll();

    Task<BranchViewModel> Get(int id);

    Task<Result> Add(CreateBranchViewModel model);

    Task<Result> Update(UpdateBranchViewModel model);

    Task<Result> Delete(int id);
}
