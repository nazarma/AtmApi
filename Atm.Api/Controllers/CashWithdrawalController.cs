namespace Atm.Api.Controllers
{
    using System.Threading.Tasks;
    using Application.HighestNumberNotes;
    using Application.LeastNumberRequest;
    using Domain.DataTransferObjects;
    using Microsoft.AspNetCore.Mvc;

    public class CashWithdrawalController: BaseController
    {
        [HttpGet("algorithm1/{amount}")]
        public async Task<LeastItemsDto> AlgorithmOne(double amount)
        {
            return await Mediator.Send(new LeastItemsRequest {Amount = amount});
        }

        [HttpGet("algorithm2/{amount}")]
        public async Task<HighestNumberDto> AlgorithmTwo(double amount)
        {
            return await Mediator.Send(new HighestNumberRequest {Amount = amount});
        }
    }
}
