using Business.Classes;
using Domain.Dtos;

namespace Business.Mappers
{
    public class ServiceMapper
    {
        public static ServiceDto ToModel(Service service)
        {
            return new ServiceDto
            {
                ServiceId = service.ServiceId,
                CarId = service.CarId,
                GarageId = service.GarageId,
                ServiceType = service.ServiceType,
                Status = service.Status,
                ServiceDate = service.ServiceDate,
                NextServiceDate = service.NextServiceDate,
                Description = service.Description
            };
        }
        public static Service ToEntity(ServiceDto serviceDto)
        {
            return new Service
            {
                ServiceId = serviceDto.ServiceId,
                CarId = serviceDto.CarId,
                GarageId = serviceDto.GarageId,
                ServiceType = serviceDto.ServiceType,
                Status = serviceDto.Status,
                ServiceDate = serviceDto.ServiceDate,
                NextServiceDate = serviceDto.NextServiceDate,
                Description = serviceDto.Description
            };
        }
    }
}
