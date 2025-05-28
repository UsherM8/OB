
using Business.Classes;

namespace Business.ContainerInterfaces
{
    public interface IServiceContainer
    {
        Task<IEnumerable<Service>> GetAllServicesAsync(int CarId);
        Task<Service> GetServiceByIdAsync(int id);
        Task CreateServiceAsync(Service service);
    }
}
