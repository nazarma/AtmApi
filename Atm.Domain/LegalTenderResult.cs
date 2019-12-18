namespace Atm.Domain
{
    using System.Collections.Generic;

    public class LegalTenderResult
    {
        public bool Succeeded { get; set; }

        public string Error { get; set; }

        public List<LegalTender> LegalTenders { get; set; }
    }
}
