namespace Atm.Application.LeastNumberRequest
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.DataTransferObjects;
    using Errors;
    using global::AutoMapper;
    using Interfaces;
    using MediatR;

    public class LeastItemsRequestHandler: IRequestHandler<LeastItemsRequest, LeastItemsDto>
    {
        private readonly ICashService cashService;
        private readonly IMapper mapper;

        public LeastItemsRequestHandler(ICashService cashService, IMapper mapper)
        {
            this.cashService = cashService;
            this.mapper = mapper;
        }

        public async Task<LeastItemsDto> Handle(LeastItemsRequest request, CancellationToken cancellationToken)
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

            var leastAmountTenders = await this.cashService.GetLeastAmountItems(2);

            return new LeastItemsDto
            {
                WithdrawnTenders = (List<LegalTenderDto>) this.mapper.Map<IList<LegalTenderDto>>(withdrawalTendersResult.LegalTenders),
                Balance = Math.Round(await this.cashService.TotalBalance(), 2),
                LeastAmountTenders = (List<LegalTenderDto>) this.mapper.Map<IList<LegalTenderDto>>(leastAmountTenders),
            };
        }
    }
}
 