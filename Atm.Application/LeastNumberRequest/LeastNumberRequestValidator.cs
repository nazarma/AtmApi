namespace Atm.Application.LeastNumberRequest
{
    using FluentValidation;

    public class LeastNumberRequestValidator: AbstractValidator<LeastItemsRequest>
    {
        public LeastNumberRequestValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0).Equal(562);
        }
    }
}
