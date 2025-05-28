using Domain.Dtos;

namespace Domain.RepositoryInterfaces
{
    public interface IService
    {
        Task<IEnumerable<ServiceDto>> GetAllServicesAsync(int CarId);
        Task<ServiceDto> GetServiceByIdAsync(int id);
        Task CreateServiceAsync(ServiceDto service);
    }
}
