namespace Atm.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using Domain;
    using Errors;
    using Interfaces;

    public class CashService: ICashService
    {
        private readonly ILegalTenderRepo legalTenderRepo;

        public CashService(ILegalTenderRepo legalTenderRepo)
        {
            this.legalTenderRepo = legalTenderRepo;
        }

        public async Task<double> TotalBalance()
        {
            var legalTenders = await this.legalTenderRepo.GetAll();

            return legalTenders.Sum(legalTender => legalTender.Amount * legalTender.Nominal);
        }

        public async Task<LegalTenderResult> CalculateWithdrawalTenders(double withdrawAmount)
        {
            var legalTenders = await this.legalTenderRepo.GetAll();

            var withdrawalTenders = new List<LegalTender>();

            legalTenders = legalTenders.OrderByDescending(x => x.Nominal).ToList();

            foreach (var legalTender in legalTenders)
            {
                var amountNeeded = (int) (withdrawAmount / legalTender.Nominal);
                var amountAvailable = legalTender.Amount;
                var amountWithdrawn = 0;

                if (amountAvailable == 0)
                {
                    continue;
                }

                if (amountAvailable >= amountNeeded)
                {
                    amountWithdrawn = (int) (withdrawAmount / legalTender.Nominal);
                    withdrawAmount = Math.Round(withdrawAmount % legalTender.Nominal, 2, MidpointRounding.AwayFromZero);
                }

                if (amountAvailable <= amountNeeded)
                {
                    amountWithdrawn = amountAvailable;
                    withdrawAmount = Math.Round(withdrawAmount - (amountAvailable * legalTender.Nominal), 2, MidpointRounding.AwayFromZero);
                }

                if (amountWithdrawn == 0)
                {
                    continue;
                }

                var withdrawalTender = new LegalTender
                {
                    Id = legalTender.Id,
                    Title = legalTender.Title,
                    Amount = amountWithdrawn,
                    Nominal = legalTender.Nominal,
                    TenderType = legalTender.TenderType
                };

                withdrawalTenders.Add(withdrawalTender);
            }

            if (Math.Abs(withdrawAmount) > 0.001)
            {
                return new LegalTenderResult
                {
                    Succeeded = false,
                    Error = "ATM does not have enough cash."
                };
            }

            return new LegalTenderResult
            {
                Succeeded = true,
                LegalTenders = withdrawalTenders
            };
        }

        public async Task<bool> UpdateTendersAmount(List<LegalTender> tendersToUpdate)
        {
            var legalTenders = await this.legalTenderRepo.GetAll();

            foreach (var legalTender in legalTenders)
            {
                foreach (var tender in tendersToUpdate.Where(tender => tender.Id == legalTender.Id))
                {
                    if (legalTender.Amount < tender.Amount)
                    {
                        throw new RestException(HttpStatusCode.BadRequest, new {title = "Not enough cash in ATM"});
                    }

                    legalTender.Amount -= tender.Amount;
                }
            }

            await this.legalTenderRepo.SaveAll();

            return true;
        }

        public async Task<IEnumerable<LegalTender>> GetLeastAmountItems(int amounts)
        {
            var legalTenders = await this.legalTenderRepo.GetAll();
            legalTenders = legalTenders.OrderBy(x => x.Amount).ToList();

            return legalTenders.Take(amounts);
        }

        public async Task<int> CountNotesByNominal(double nominal)
        {
            var legalTenders = await this.legalTenderRepo.GetAll();

            return legalTenders.Count(x => Math.Abs(x.Nominal - nominal) < 0.001);
        }
    }
}
