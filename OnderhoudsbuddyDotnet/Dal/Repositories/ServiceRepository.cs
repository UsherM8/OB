using Dal.Entities;
using Domain.Dtos;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace Dal.Repositories
{
    public class ServiceRepository : IService
    {
        private readonly OnderhoudsbuddyDbContext _context;

        public ServiceRepository(OnderhoudsbuddyDbContext context)
        {
            _context = context;
        }
        public async Task CreateServiceAsync(ServiceDto service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service), "Service cannot be null");
            }
            var newService = new Service
            {
                CarId = service.CarId,
                GarageId = service.GarageId,
                ServiceType = service.ServiceType,
                Status = service.Status,
                ServiceDate = service.ServiceDate,
                NextServiceDate = service.NextServiceDate,
                Description = service.Description
            };
            await _context.Services.AddAsync(newService);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ServiceDto>> GetAllServicesAsync(int CarId)
        {
              List<Service> services = await _context.Services
                .Where(s => s.CarId == CarId).ToListAsync();
            if (services == null || !services.Any())
            {
                return Enumerable.Empty<ServiceDto>();
            }
            var ServiceDto = services.Select(Services => new ServiceDto
            {
                ServiceId = Services.ServiceId,
                CarId = Services.CarId,
                GarageId = Services.GarageId,
                Description = Services.Description,
                NextServiceDate = Services.NextServiceDate,
                ServiceDate = Services.ServiceDate,
                ServiceType = Services.ServiceType,
                Status = Services.Status
            });

            return ServiceDto;
        }


        public async Task<ServiceDto> GetServiceByIdAsync(int id)
        {
            var service = await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == id);

            if (service == null)
            {
                return new ServiceDto();
            }

            return new ServiceDto
            {
                ServiceId = service.ServiceId,
                CarId = service.CarId,
                GarageId = service.GarageId,
                Description = service.Description,
                NextServiceDate = service.NextServiceDate,
                ServiceDate = service.ServiceDate,
                ServiceType = service.ServiceType,
                Status = service.Status
            };
        }
    }
}
