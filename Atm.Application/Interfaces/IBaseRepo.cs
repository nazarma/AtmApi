namespace Atm.Application.Interfaces
{
    using System.Threading.Tasks;

    public interface IBaseRepo
    {
        Task<bool> SaveAll();
    }
}
