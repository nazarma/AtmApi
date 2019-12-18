namespace Atm.Domain.DataTransferObjects
{
    using System.Collections.Generic;

    public class LeastItemsDto
    {
        public List<LegalTenderDto> WithdrawnTenders { get; set; }

        public List<LegalTenderDto> LeastAmountTenders { get; set; }

        public double Balance { get; set; }
    }
}
