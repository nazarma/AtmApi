namespace Atm.Application.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;

    public interface ICashService
    {
        Task<double> TotalBalance();

        Task<LegalTenderResult> CalculateWithdrawalTenders(double withdrawAmount);

        Task<bool> UpdateTendersAmount(List<LegalTender> tendersToUpdate);

        Task<IEnumerable<LegalTender>> GetLeastAmountItems(int amounts);

        Task<int> CountNotesByNominal(double nominal);
    }
}
