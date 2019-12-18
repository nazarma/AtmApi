namespace Atm.Application.HighestNumberNotes
{
    using Domain.DataTransferObjects;
    using MediatR;

    public class HighestNumberRequest: IRequest<HighestNumberDto>
    {
        public double Amount { get; set; }
    }
}
