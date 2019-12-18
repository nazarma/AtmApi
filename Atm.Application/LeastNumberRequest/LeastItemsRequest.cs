namespace Atm.Application.LeastNumberRequest
{
    using Domain.DataTransferObjects;
    using MediatR;

    public class LeastItemsRequest: IRequest<LeastItemsDto>
    {
        public double Amount { get; set; }
    }
}
