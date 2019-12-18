namespace Atm.Application.HighestNumberNotes
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.DataTransferObjects;
    using Errors;
    using Interfaces;
    using MediatR;

    public class HighestNumberRequestHandler: IRequestHandler<HighestNumberRequest, HighestNumberDto>
    {
        private readonly ICashService cashService;

        public HighestNumberRequestHandler(ICashService cashService)
        {
            this.cashService = cashService;
        }

        public async Task<HighestNumberDto> Handle(HighestNumberRequest request, CancellationToken cancellationToken)
        {
            var currentBalance = await this.cashService.TotalBalance();
            if (currentBalance < request.Amount)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { title = "Not enough cash in ATM" });
            }

            var withdrawalTendersResult = await this.cashService.CalculateWithdrawalTenders(request.Amount);
            if (!withdrawalTendersResult.Succeeded)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { title = "Not enough cash in ATM" });
            }

            var updateResult = await this.cashService.UpdateTendersAmount(withdrawalTendersResult.LegalTenders);
            if (!updateResult)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { title = "Cannot update the database" });
            }

            return new HighestNumberDto
            {
                Balance = Math.Round(await this.cashService.TotalBalance(), 2),
                TwentyPoundsQuantity = await this.cashService.CountNotesByNominal(20)
            };
        }
    }
}
