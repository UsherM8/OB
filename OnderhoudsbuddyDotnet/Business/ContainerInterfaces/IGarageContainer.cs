

using Business.Classes;

namespace Business.ContainerInterfaces
{
    public interface IGarageContainer
    {
        Task<IEnumerable<Garage>> GetAllGaragesAsync();
        Task<Garage> GetGarageByIdAsync(int id);
        Task CreateGarageAsync(Garage garage);
    }
}
