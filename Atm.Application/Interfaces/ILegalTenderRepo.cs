namespace Atm.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    public interface ILegalTenderRepo: IBaseRepo
    {
        Task<List<LegalTender>> GetAll();
    }
}
