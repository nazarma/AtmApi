namespace Atm.Application.Repos
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Persistence;

    public class LegalTenderRepo: ILegalTenderRepo
    {
        private readonly DataContext ctx;

        public LegalTenderRepo(DataContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<List<LegalTender>> GetAll()
        {
            return await this.ctx.LegalTender.ToListAsync();
        }

        public async Task<bool> SaveAll()
        {
            return await this.ctx.SaveChangesAsync() > 0;
        }
    }
}
