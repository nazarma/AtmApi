namespace Atm.Persistence.SeedData
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using Domain.Const;

    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            if (!context.LegalTender.Any())
            {
                var legalTenders = new List<LegalTender>
                {
                    new LegalTender
                    {
                        Title = "1 p",
                        Nominal = 0.01,
                        Amount = 100, 
                        TenderType = TenderType.Coin
                    },
                    new LegalTender
                    {
                        Title = "2 p",
                        Nominal = 0.02,
                        Amount = 100,
                        TenderType = TenderType.Coin
                    },
                    new LegalTender
                    {
                        Title = "5 p",
                        Nominal = 0.05,
                        Amount = 100,
                        TenderType = TenderType.Coin
                    },
                    new LegalTender
                    {
                        Title = "10 p",
                        Nominal = 0.1,
                        Amount = 100,
                        TenderType = TenderType.Coin
                    },
                    new LegalTender
                    {
                        Title = "20 p",
                        Nominal = 0.2,
                        Amount = 100,
                        TenderType = TenderType.Coin
                    },
                    new LegalTender
                    {
                        Title = "50 p",
                        Nominal = 0.5,
                        Amount = 100,
                        TenderType = TenderType.Coin
                    },


                    new LegalTender
                    {
                        Title = "£1",
                        Nominal = 1,
                        Amount = 100,
                        TenderType = TenderType.Banknote
                    },
                    new LegalTender
                    {
                        Title = "£2",
                        Nominal = 2,
                        Amount = 100,
                        TenderType = TenderType.Banknote
                    },
                    new LegalTender
                    {
                        Title = "£5",
                        Nominal = 5,
                        Amount = 50,
                        TenderType = TenderType.Banknote
                    },
                    new LegalTender
                    {
                        Title = "£10",
                        Nominal = 10,
                        Amount = 50,
                        TenderType = TenderType.Banknote
                    },
                    new LegalTender
                    {
                        Title = "£20",
                        Nominal = 20,
                        Amount = 50,
                        TenderType = TenderType.Banknote
                    },
                    new LegalTender
                    {
                        Title = "£50",
                        Nominal = 50,
                        Amount = 50,
                        TenderType = TenderType.Banknote
                    },
                };

                context.LegalTender.AddRange(legalTenders);
                context.SaveChanges();
            }
        }
    }
}
