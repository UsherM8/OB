
using Business.Classes;
using Business.Mappers;
using Domain.RepositoryInterfaces;

namespace Business.Containers
{
    public class ServiceContainer
    {
        private readonly IService _serviceRepository;
        public ServiceContainer(IService serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<IEnumerable<Service>> GetAllServicesAsync(int carId)
        {
            var serviceDtos = await _serviceRepository.GetAllServicesAsync(carId);
            return serviceDtos.Select(ServiceMapper.ToEntity).ToList();
        }
        public async Task<Service> GetServiceByIdAsync(int id)
        {
            var serviceDto = await _serviceRepository.GetServiceByIdAsync(id);
            if (serviceDto == null)
            {
                return new Service();
            }
            return ServiceMapper.ToEntity(serviceDto);
        }
        public async Task CreateServiceAsync(Service service)
        {
            var serviceDto = ServiceMapper.ToModel(service);
            await _serviceRepository.CreateServiceAsync(serviceDto);
        }
    }
}
