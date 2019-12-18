namespace Atm.Domain
{
    public class LegalTender
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public double Nominal { get; set; }

        public int Amount { get; set; }

        public string TenderType { get; set; }
    }
}
