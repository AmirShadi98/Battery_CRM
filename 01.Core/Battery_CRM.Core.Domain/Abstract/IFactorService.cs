using Battery_CRM.Core.Domain.Models.ViewModels.Factor;
using Battery_CRM.Framework.Extensions;

namespace Battery_CRM.Core.Domain.Abstract;

public interface IFactorService
{
    Task<List<FactorsViewModel>> GetAll();

    Task<FactorViewModel> Get(int id);

    Task<Result> Add(CreateFactorViewModel model);
}
